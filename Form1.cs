using myModbusPollV1;
using System.Diagnostics;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Transactions;

namespace myModbusPolLV1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            write2LabelFromSubprocess(label4, "COMM STATUS: IDLE");
            write2LabelFromSubprocess(label5, "DATA STATUS: NO DATA");
            write2LabelFromSubprocess(label6, "ALARM: NONE");

            panel1.BackColor = Color.LightGray;
            panel2.BackColor = Color.LightGray;
            panel3.BackColor = Color.LightGray;
        }

        private void button2_Click(object sender, EventArgs e) // conecta a sensor de flujo
        {
            Thread subproces = new Thread(Connect2SensorFlow);
            subproces.Start();
        }

        private void button3_Click(object sender, EventArgs e) // Conecta a sensor de presión
        {
            Thread subprocess = new Thread(OpenPortAndHandleMessages_Pressure);
            subprocess.Start();
        }
        private void button4_Click(object sender, EventArgs e) // Conecta a sensor de potencia
        {
            Thread subproces = new Thread(Connect2SensorPower);
            subproces.Start();
        }
        private void button5_Click(object sender, EventArgs e) // Conecta a sensor / activación de bomba de agua
        {
            Thread subproces = new Thread(Connect2WaterPump);
            subproces.Start();
        }

        // UI handler functions
        bool UIavilable = true;
        private void clearTextboxFromSubprocess(RichTextBox textBox)
        {
            if (UIavilable)
            {
                Invoke(new MethodInvoker(() =>
                {
                    textBox.Text = "";
                }));
            }
        }
        private void write2TextboxFromSubprocess(RichTextBox textBox, string text)
        {
            if (UIavilable)
            {
                Invoke(new MethodInvoker(() =>
                {
                    if (UIavilable) textBox.Text += "\n" + text;
                }));
            }
        }

        private void write2LabelFromSubprocess(Label label, string text)
        {
            if (UIavilable)
            {
                Invoke(new MethodInvoker(() =>
                {
                    if (UIavilable) label.Text = text;
                }));
            }
        }

        private void enableButtonFromSubprocess(Button button)
        {
            if (UIavilable)
            {
                Invoke(new MethodInvoker(() =>
                {
                    button.Enabled = true;
                }));
            }
        }
        private void disableButtonFromSubprocess(Button button)
        {
            if (UIavilable)
            {
                Invoke(new MethodInvoker(() =>
                {
                    button.Enabled = false;
                }));
            }
        }

        private void setPanelColorFromSubprocess(Panel panel, Color color)
        {
            if (UIavilable)
            {
                Invoke(new MethodInvoker(() =>
                {
                    if (UIavilable) panel.BackColor = color;
                }));
            }
        }

        private void setCommOk()
        {
            lastValidResponseTime = DateTime.Now;
            dataIsValid = true;

            write2LabelFromSubprocess(label4, "COMM STATUS: CONNECTED");
            write2LabelFromSubprocess(label5, "DATA STATUS: VALID");
            write2LabelFromSubprocess(label6, "ALARM: NONE");

            setPanelColorFromSubprocess(panel1, Color.LimeGreen);
            setPanelColorFromSubprocess(panel2, Color.LimeGreen);
            setPanelColorFromSubprocess(panel3, Color.LimeGreen);
        }

        private void setWaitingResponse()
        {
            write2LabelFromSubprocess(label4, "COMM STATUS: CONNECTED");
            write2LabelFromSubprocess(label5, "DATA STATUS: WAITING RESPONSE");
            write2LabelFromSubprocess(label6, "ALARM: NONE");

            setPanelColorFromSubprocess(panel1, Color.LimeGreen);
            setPanelColorFromSubprocess(panel2, Color.Gold);
            setPanelColorFromSubprocess(panel3, Color.LimeGreen);
        }

        private void setCommLost(string message = "ALARM: COMMUNICATION LOST")
        {
            dataIsValid = false;

            write2LabelFromSubprocess(label4, "COMM STATUS: DISCONNECTED");
            write2LabelFromSubprocess(label5, "DATA STATUS: STALE");
            write2LabelFromSubprocess(label6, message);

            setPanelColorFromSubprocess(panel1, Color.Red);
            setPanelColorFromSubprocess(panel2, Color.Red);
            setPanelColorFromSubprocess(panel3, Color.Red);
        }

        private void setInvalidFrame(string message = "ALARM: INVALID MODBUS FRAME")
        {
            dataIsValid = false;

            write2LabelFromSubprocess(label4, "COMM STATUS: CONNECTED");
            write2LabelFromSubprocess(label5, "DATA STATUS: INVALID");
            write2LabelFromSubprocess(label6, message);

            setPanelColorFromSubprocess(panel1, Color.LimeGreen);
            setPanelColorFromSubprocess(panel2, Color.Orange);
            setPanelColorFromSubprocess(panel3, Color.Red);
        }

        private void invalidateDisplayedData()
        {
            write2LabelFromSubprocess(label1, "The temperature is ---");
            write2LabelFromSubprocess(label2, "The preassure is ---");
            write2LabelFromSubprocess(label3, "The flow is ---");
        }

        private void checkCommunicationTimeout()
        {
            if (!keepConnectionFlow && !keepConnectionPower && !keepConnectionWaterPump && !keepConnection)
                return;

            if (lastValidResponseTime == DateTime.MinValue)
            {
                setCommLost("ALARM: NO VALID RESPONSE YET");
                invalidateDisplayedData();
                return;
            }

            double elapsedMs = (DateTime.Now - lastValidResponseTime).TotalMilliseconds;

            if (elapsedMs > commTimeoutMs)
            {
                setCommLost("ALARM: SENSOR TIMEOUT");
                invalidateDisplayedData();
            }
        }



        //client functions
        TcpClient clientFlow = null;
        bool keepConnectionFlow = false;

        TcpClient clientPressure = null;
        bool keepConnectionPressure = false;

        TcpClient clientPower = null;
        bool keepConnectionPower = false;

        TcpClient clientWaterPump = null;
        bool keepConnectionWaterPump = false;

        TcpClient client = null;
        bool keepConnection = false;


        DateTime lastValidResponseTime = DateTime.MinValue;
        bool dataIsValid = false;
        int commTimeoutMs = 3000; // 3 seconds without valid response = communication lost

        private void AutomaticRequestFlow()
        {
            while (keepConnectionFlow)
            {
                setWaitingResponse();
                sendMessageTCPFlow();

                Thread.Sleep(1000);
                checkCommunicationTimeout();
            }
        }

        //private void AutomaticRequestFlow()
        //{
        //    while (keepConnectionFlow)
        //    {
        //        setWaitingResponse();
        //        sendMessageRTUoTCPFlow();
        //        //sendMessageFlow();

        //        Thread.Sleep(1000);
        //        checkCommunicationTimeout();
        //    }
        //}
        bool lastPowerRequestWasEnergy = false;
        private void AutomaticRequestPower()
        {
            bool requestingPower = true;

            while (keepConnectionPower)
            {
                setWaitingResponse();

                if (requestingPower)
                {
                    sendMessageRTUoTCPPower();
                    lastPowerRequestWasEnergy = false;
                }
                else
                {
                    sendMessageRTUoTCPEnergy();
                    lastPowerRequestWasEnergy = true;
                }

                requestingPower = !requestingPower;

                Thread.Sleep(1000);
                checkCommunicationTimeout();
            }
        }
        private void AutomaticRequestWaterPump()
        {
            while (keepConnectionWaterPump)
            {
                setWaitingResponse();
                //sendMessageRTUoTCP();
                sendMessageWaterPump();

                Thread.Sleep(1000);
                checkCommunicationTimeout();
            }
        }
        private bool processTCPresponseFlow(byte[] buffer)
        {
            bool result = false;

            if (buffer[2] == 0 && buffer[3] == 0)
            {
                write2TextboxFromSubprocess(richTextBox1, "ProtocolID for TCP ok");
                int TCPMessageLength = 3 + 2 * RegQty_Flow;

                if (buffer[4] * 256 + buffer[5] == TCPMessageLength)
                {
                    write2TextboxFromSubprocess(richTextBox1, "MessageLength Ok");

                    if (buffer[6] == ModbusID_Flow)
                    {
                        write2TextboxFromSubprocess(richTextBox1, "ModbusID Ok");

                        if (buffer[7] == Function_Flow)
                        {
                            write2TextboxFromSubprocess(richTextBox1, "Function Ok");

                            if (buffer[8] == 2 * RegQty_Flow)
                            {
                                result = true;
                                write2TextboxFromSubprocess(richTextBox1, "Data bytes length Ok");

                                byte[] newArray = new byte[4];
                                newArray[0] = buffer[12];          //
                                newArray[1] = buffer[11];         // Transformar Big Endian to Float
                                newArray[2] = buffer[10];        //
                                newArray[3] = buffer[9];       //
                                float instant_flow = BitConverter.ToSingle(newArray, 0);
                                write2LabelFromSubprocess(label1, $"The Instant Flow is {instant_flow} [lps]");


                                newArray[0] = buffer[16];          //
                                newArray[1] = buffer[15];         // Transformar Big Endian to Float 
                                newArray[2] = buffer[14];        //
                                newArray[3] = buffer[13];       //
                                float totalizer = BitConverter.ToSingle(newArray, 0);
                                write2LabelFromSubprocess(label7, $"The Totalizer is {totalizer} [m^3]");

                                setCommOk();
                                return true;
                            }
                            else
                            {
                                setInvalidFrame("ALARM: WRONG DATA LENGTH");
                            }
                        }
                        else
                        {
                            setInvalidFrame("ALARM: WRONG FUNCTION");
                        }
                    }
                    else
                    {
                        setInvalidFrame("ALARM: WRONG MODBUS ID");
                    }
                }
                else
                {
                    setInvalidFrame("ALARM: WRONG TCP LENGTH");
                }
            }
            else
            {
                setInvalidFrame("ALARM: WRONG PROTOCOL ID");
            }

            return result;
        }
        private bool processRTUoTCPresponsePower(byte[] buffer, int bytesReceived)
        {
            bool result = false;

            if (crcObject.ComputeCRC(buffer, bytesReceived) == 0)
            {
                write2TextboxFromSubprocess(richTextBox1, "CRC ok");

                if (buffer[0] == ModbusID_Power)
                {
                    write2TextboxFromSubprocess(richTextBox1, "ModbusID Ok");

                    if (buffer[1] == Function_Power)
                    {
                        write2TextboxFromSubprocess(richTextBox1, "Function Ok");

                        if (buffer[2] == 2 * RegQty_Power)
                        {
                            result = true;
                            write2TextboxFromSubprocess(richTextBox1, "Data bytes length Ok");

                            // Power: float little endian
                            // RTU response data starts at buffer[3]
                            float power = BitConverter.ToSingle(buffer, 3);

                            write2LabelFromSubprocess(label3, $"The Power is {power} [kW]");

                            setCommOk();
                            return true;
                        }
                        else
                        {
                            setInvalidFrame("ALARM: WRONG POWER DATA LENGTH");
                        }
                    }
                    else
                    {
                        setInvalidFrame("ALARM: WRONG POWER FUNCTION");
                    }
                }
                else
                {
                    setInvalidFrame("ALARM: WRONG POWER MODBUS ID");
                }
            }
            else
            {
                setInvalidFrame("ALARM: WRONG POWER CRC");
            }

            return result;
        }
        private bool processRTUoTCPresponseEnergy(byte[] buffer, int bytesReceived)
        {
            bool result = false;

            if (crcObject.ComputeCRC(buffer, bytesReceived) == 0)
            {
                write2TextboxFromSubprocess(richTextBox1, "CRC ok");

                if (buffer[0] == ModbusID_Power)
                {
                    write2TextboxFromSubprocess(richTextBox1, "ModbusID Ok");

                    if (buffer[1] == Function_Power)
                    {
                        write2TextboxFromSubprocess(richTextBox1, "Function Ok");

                        if (buffer[2] == 2 * RegQty_Energy)
                        {
                            result = true;
                            write2TextboxFromSubprocess(richTextBox1, "Data bytes length Ok");

                            // Energy: Int32 little endian
                            // RTU response data starts at buffer[3]
                            int energy = BitConverter.ToInt32(buffer, 3);

                            write2LabelFromSubprocess(label8, $"The Energy is {energy} [kWh]");

                            setCommOk();
                            return true;
                        }
                        else
                        {
                            setInvalidFrame("ALARM: WRONG ENERGY DATA LENGTH");
                        }
                    }
                    else
                    {
                        setInvalidFrame("ALARM: WRONG ENERGY FUNCTION");
                    }
                }
                else
                {
                    setInvalidFrame("ALARM: WRONG ENERGY MODBUS ID");
                }
            }
            else
            {
                setInvalidFrame("ALARM: WRONG ENERGY CRC");
            }

            return result;
        }
        private bool processRTUresponsePressure(byte[] buffer, int bytesReceived)
        {
            bool result = false;
            if (true) // NO protocol check needed 
            {
                if (crcObject.ComputeCRC(buffer, bytesReceived) == 0)
                {
                    {
                        write2TextboxFromSubprocess(richTextBox1, "CRC ok");

                        if (buffer[0] == ModbusID_Pressure)
                        {
                            write2TextboxFromSubprocess(richTextBox1, "ModbusID Ok");

                            if (buffer[1] == Function_Pressure)
                            {
                                write2TextboxFromSubprocess(richTextBox1, "Function Ok");

                                if (buffer[2] == 2 * RegQty_Pressure)
                                {
                                    result = true;
                                    write2TextboxFromSubprocess(richTextBox1, "Data bytes length Ok");
                                    byte[] newArray = new byte[4];
                                    newArray[0] = buffer[10];          //
                                    newArray[1] = buffer[9];         // Transformar Big Endian to Float
                                    newArray[2] = buffer[8];        //
                                    newArray[3] = buffer[7];       //
                                    float Preassure = BitConverter.ToSingle(newArray, 0);
                                    write2LabelFromSubprocess(label2, $"The preassure is {Preassure} [kg/cm²]");
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        private void Connect2SensorFlow()
        {
            try
            {
                disableButtonFromSubprocess(button2);

                write2LabelFromSubprocess(label4, "COMM STATUS: CONNECTING...");
                write2LabelFromSubprocess(label5, "DATA STATUS: NO DATA");
                write2LabelFromSubprocess(label6, "ALARM: NONE");

                panel1.BackColor = Color.Gold;
                panel2.BackColor = Color.LightGray;
                panel3.BackColor = Color.LightGray;

                clientFlow = new TcpClient("127.0.0.1", 502);

                if (clientFlow.Connected)
                {
                    keepConnectionFlow = true;
                    lastValidResponseTime = DateTime.MinValue;
                    dataIsValid = false;

                    write2LabelFromSubprocess(label4, "COMM STATUS: CONNECTED TO FLOW SENSOR");
                    setPanelColorFromSubprocess(panel1, Color.LimeGreen);

                    Thread pollingSubprocess = new Thread(AutomaticRequestFlow);
                    pollingSubprocess.Start();

                    NetworkStream stream = clientFlow.GetStream();
                    stream.ReadTimeout = 1000;

                    int bytesRead = 0;
                    byte[] buffer = new byte[1024];

                    while (keepConnectionFlow && clientFlow.Connected)
                    {
                        try
                        {
                            bytesRead = stream.Read(buffer, 0, buffer.Length);

                            if (bytesRead == 0)
                            {
                                setCommLost("ALARM: SERVER DISCONNECTED");
                                break;
                            }
                            else
                            {
                                write2TextboxFromSubprocess(richTextBox1, "New Message");
                                bool valid = processTCPresponseFlow(buffer);

                                if (!valid)
                                {
                                    checkCommunicationTimeout();
                                }
                            }
                        }
                        catch (IOException)
                        {
                            checkCommunicationTimeout();
                        }
                    }
                }
                else
                {
                    write2TextboxFromSubprocess(richTextBox1, "connection failed");
                    setCommLost("ALARM: CONNECTION FAILED");
                    invalidateDisplayedData();
                }
            }
            catch (Exception ex)
            {
                write2TextboxFromSubprocess(richTextBox1, ex.ToString());
                setCommLost("ALARM: EXCEPTION IN CLIENT");
                invalidateDisplayedData();
            }
            finally
            {
                keepConnectionFlow = false;

                if (clientFlow != null && clientFlow.Connected)
                {
                    NetworkStream stream = clientFlow.GetStream();
                    stream.Close();
                    clientFlow.Close();
                }

                enableButtonFromSubprocess(button2);
            }
        }

        private void Connect2SensorPower()
        {
            try
            {
                disableButtonFromSubprocess(button4);

                write2LabelFromSubprocess(label4, "COMM STATUS: CONNECTING...");
                write2LabelFromSubprocess(label5, "DATA STATUS: NO DATA");
                write2LabelFromSubprocess(label6, "ALARM: NONE");

                setPanelColorFromSubprocess(panel1, Color.Gold);
                setPanelColorFromSubprocess(panel2, Color.LightGray);
                setPanelColorFromSubprocess(panel3, Color.LightGray);

                clientPower = new TcpClient("127.0.0.1", 504);

                if (clientPower.Connected)
                {
                    keepConnectionPower = true;
                    lastValidResponseTime = DateTime.MinValue;
                    dataIsValid = false;

                    write2LabelFromSubprocess(label4, "COMM STATUS: CONNECTED TO POWER SENSOR");
                    setPanelColorFromSubprocess(panel1, Color.LimeGreen);

                    Thread pollingSubprocess = new Thread(AutomaticRequestPower);
                    pollingSubprocess.Start();

                    NetworkStream stream = clientPower.GetStream();
                    stream.ReadTimeout = 1000;

                    int bytesRead = 0;
                    byte[] buffer = new byte[1024];

                    while (keepConnectionPower && clientPower.Connected)
                    {
                        try
                        {
                            bytesRead = stream.Read(buffer, 0, buffer.Length);

                            if (bytesRead == 0)
                            {
                                setCommLost("ALARM: POWER SENSOR DISCONNECTED");
                                break;
                            }

                            write2TextboxFromSubprocess(richTextBox1, "New Power Meter Message");

                            bool valid;

                            if (lastPowerRequestWasEnergy)
                            {
                                valid = processRTUoTCPresponseEnergy(buffer, bytesRead);
                            }
                            else
                            {
                                valid = processRTUoTCPresponsePower(buffer, bytesRead);
                            }

                            if (!valid)
                            {
                                checkCommunicationTimeout();
                            }
                        }
                        catch (IOException)
                        {
                            checkCommunicationTimeout();
                        }
                    }
                }
                else
                {
                    write2TextboxFromSubprocess(richTextBox1, "Power sensor connection failed");
                    setCommLost("ALARM: POWER SENSOR CONNECTION FAILED");
                    invalidateDisplayedData();
                }
            }
            catch (Exception ex)
            {
                write2TextboxFromSubprocess(richTextBox1, ex.ToString());
                setCommLost("ALARM: EXCEPTION IN POWER CLIENT");
                invalidateDisplayedData();
            }
            finally
            {
                keepConnectionPower = false;

                if (clientPower != null && clientPower.Connected)
                {
                    NetworkStream stream = clientPower.GetStream();
                    stream.Close();
                    clientPower.Close();
                }

                enableButtonFromSubprocess(button4);
            }
        }

        private void Connect2WaterPump()
        {
            try
            {
                disableButtonFromSubprocess(button5);

                write2LabelFromSubprocess(label4, "COMM STATUS: CONNECTING...");
                write2LabelFromSubprocess(label5, "DATA STATUS: NO DATA");
                write2LabelFromSubprocess(label6, "ALARM: NONE");

                panel1.BackColor = Color.Gold;
                panel2.BackColor = Color.LightGray;
                panel3.BackColor = Color.LightGray;

                clientWaterPump = new TcpClient("127.0.0.1", 505);

                if (clientWaterPump.Connected)
                {
                    keepConnectionWaterPump = true;
                    lastValidResponseTime = DateTime.MinValue;
                    dataIsValid = false;

                    write2LabelFromSubprocess(label4, "COMM STATUS: CONNECTED TO WATER PUMP");
                    setPanelColorFromSubprocess(panel1, Color.LimeGreen);

                    Thread pollingSubprocess = new Thread(AutomaticRequestWaterPump);
                    pollingSubprocess.Start();

                    NetworkStream stream = clientWaterPump.GetStream();
                    stream.ReadTimeout = 1000;

                    int bytesRead = 0;
                    byte[] buffer = new byte[1024];

                    while (keepConnectionWaterPump && clientWaterPump.Connected)
                    {
                        try
                        {
                            bytesRead = stream.Read(buffer, 0, buffer.Length);

                            if (bytesRead == 0)
                            {
                                setCommLost("ALARM: SERVER DISCONNECTED");
                                break;
                            }
                            else
                            {
                                write2TextboxFromSubprocess(richTextBox1, "New Message");
                                bool valid = processRTUresponsePressure(buffer, bytesRead);

                                if (!valid)
                                {
                                    checkCommunicationTimeout();
                                }
                            }
                        }
                        catch (IOException)
                        {
                            checkCommunicationTimeout();
                        }
                    }
                }
                else
                {
                    write2TextboxFromSubprocess(richTextBox1, "connection failed");
                    setCommLost("ALARM: CONNECTION FAILED");
                    invalidateDisplayedData();
                }
            }
            catch (Exception ex)
            {
                write2TextboxFromSubprocess(richTextBox1, ex.ToString());
                setCommLost("ALARM: EXCEPTION IN CLIENT");
                invalidateDisplayedData();
            }
            finally
            {
                keepConnectionWaterPump = false;

                if (clientWaterPump != null && clientWaterPump.Connected)
                {
                    NetworkStream stream = clientWaterPump.GetStream();
                    stream.Close();
                    clientWaterPump.Close();
                }

                enableButtonFromSubprocess(button5);
            }
        }

        Int16 TransactionID = 0;
        Int16 ProtocolID = 0; // MODBUS-TCP = 0
        Int16 MessageLength = 6; // For functions 3 & 4

        // Flow: Inst.Flow, Totalizer (2 variables)
        byte ModbusID_Flow = 5; // ID in Modbus
        byte Function_Flow = 3; // Function in Modbus 
        Int16 Address_Flow = 1200; // Start adress to read
        Int16 RegQty_Flow = 4; // Ya que son 2 floats (hasta 1202 + 1203) ya que ocupan 2 registros cada uno. 

        // Pressure: pressure (una solo variable)
        byte ModbusID_Pressure = 1; // ID in Modbus
        byte Function_Pressure = 4; // Function in Modbus 
        Int16 Address_Pressure = 75; // Start adress to read
        Int16 RegQty_Pressure = 2;

        // Power: power, Energy (2 variables)   
        byte ModbusID_Power = 2; // ID in Modbus
        byte Function_Power = 4; // Function in Modbus 
        // Power (float little Endian):
        Int16 Address_Power = 8; // Start adress to read
        Int16 RegQty_Power = 2;
        // Energy (Int32 bits):
        Int16 Address_Energy = 200; // Start adress to read
        Int16 RegQty_Energy = 2;

        // Water Pump
        byte ModbusID_WaterPump = 17; // ID in Modbus
        byte Function_WaterPump = 4; // Function in Modbus 
        Int16 Address_WaterPump = 12; // Start adress to read
        Int16 RegQty_WaterPump = 5;


        myCRC crcObject = new myCRC();

        private void sendMessageTCPFlow()
        {
            if (UIavilable && clientFlow != null && clientFlow.Connected)
            {
                NetworkStream stream = clientFlow.GetStream();

                byte[] bytes2send = new byte[12];

                bytes2send[0] = (byte)(TransactionID >> 8);
                bytes2send[1] = (byte)(TransactionID);

                bytes2send[2] = (byte)(ProtocolID >> 8);
                bytes2send[3] = (byte)(ProtocolID);

                bytes2send[4] = (byte)(MessageLength >> 8);
                bytes2send[5] = (byte)(MessageLength);

                bytes2send[6] = ModbusID_Flow;
                bytes2send[7] = Function_Flow;

                bytes2send[8] = (byte)(Address_Flow >> 8);
                bytes2send[9] = (byte)(Address_Flow);

                bytes2send[10] = (byte)(RegQty_Flow >> 8);
                bytes2send[11] = (byte)(RegQty_Flow);

                stream.Write(bytes2send);
                TransactionID++;
            }
        }

        //private void sendMessageRTUoTCPFlow(/*RichTextBox textBox*/)
        //{
        //    if (UIavilable && client != null && client.Connected)
        //    {
        //        //string message = textBox.Text;
        //        //clearTextboxFromSubprocess(textBox);

        //        NetworkStream stream = clientFlow.GetStream();
        //        byte[] bytes2send = new byte[8];
        //        bytes2send[0] = ModbusID_Flow;
        //        bytes2send[1] = Function_Flow;
        //        bytes2send[2] = (byte)(Address_Flow >> 8);
        //        bytes2send[3] = (byte)(Address_Flow);
        //        bytes2send[4] = (byte)(RegQty_Flow >> 8);
        //        bytes2send[5] = (byte)(RegQty_Flow);
        //        Int16 CRC = crcObject.ComputeCRC(bytes2send, 6);
        //        bytes2send[6] = (byte)(CRC >> 8);
        //        bytes2send[7] = (byte)(CRC);
        //        //byte[] bytes2send = Encoding.UTF8.GetBytes(message);

        //        stream.Write(bytes2send);
        //    }
        //}
        private void sendMessageRTU_Pressure()
        {
            byte[] bytes2send = new byte[8];
            bytes2send[0] = ModbusID_Pressure;
            bytes2send[1] = Function_Pressure;
            bytes2send[2] = (byte)(Address_Pressure >> 8);
            bytes2send[3] = (byte)(Address_Pressure);
            bytes2send[4] = (byte)(RegQty_Pressure >> 8);
            bytes2send[5] = (byte)(RegQty_Pressure);
            Int16 messageCRC = crcObject.ComputeCRC(bytes2send, 6);
            bytes2send[6] = (byte)(messageCRC >> 8);
            bytes2send[7] = (byte)(messageCRC);
            serialPort1.Write(bytes2send, 0, 8);
            serialPort1.BaseStream.Flush();
        }
        private void sendMessageRTUoTCPPower(/*RichTextBox textBox*/)
        {
            if (UIavilable && clientPower != null && clientPower.Connected)
            {
                //string message = textBox.Text;
                //clearTextboxFromSubprocess(textBox);

                NetworkStream stream = clientPower.GetStream();
                byte[] bytes2send = new byte[8];
                bytes2send[0] = ModbusID_Power;
                bytes2send[1] = Function_Power;
                bytes2send[2] = (byte)(Address_Power >> 8);
                bytes2send[3] = (byte)(Address_Power);
                bytes2send[4] = (byte)(RegQty_Power >> 8);
                bytes2send[5] = (byte)(RegQty_Power);
                Int16 CRC = crcObject.ComputeCRC(bytes2send, 6);
                bytes2send[6] = (byte)(CRC >> 8);
                bytes2send[7] = (byte)(CRC);
                //byte[] bytes2send = Encoding.UTF8.GetBytes(message);

                stream.Write(bytes2send);
            }
        }
        private void sendMessageRTUoTCPEnergy()
        {
            if (UIavilable && clientPower != null && clientPower.Connected)
            {
                NetworkStream stream = clientPower.GetStream();

                byte[] bytes2send = new byte[8];
                bytes2send[0] = ModbusID_Power;
                bytes2send[1] = Function_Power;
                bytes2send[2] = (byte)(Address_Energy >> 8);
                bytes2send[3] = (byte)(Address_Energy);
                bytes2send[4] = (byte)(RegQty_Energy >> 8);
                bytes2send[5] = (byte)(RegQty_Energy);

                Int16 CRC = crcObject.ComputeCRC(bytes2send, 6);
                bytes2send[6] = (byte)(CRC >> 8);
                bytes2send[7] = (byte)(CRC);

                stream.Write(bytes2send);
            }
        }
        private void sendMessageWaterPump(/*RichTextBox textBox*/)
        {
            if (UIavilable && clientWaterPump != null && clientWaterPump.Connected)
            {
                //string message = textBox.Text;
                //clearTextboxFromSubprocess(textBox);

                NetworkStream stream = clientWaterPump.GetStream();
                byte[] bytes2send = new byte[12];
                bytes2send[0] = (byte)(TransactionID >> 8);
                bytes2send[1] = (byte)(TransactionID);
                bytes2send[2] = (byte)(ProtocolID >> 8);
                bytes2send[3] = (byte)(ProtocolID);
                bytes2send[4] = (byte)(MessageLength >> 8);
                bytes2send[5] = (byte)(MessageLength);
                bytes2send[6] = ModbusID_WaterPump;
                bytes2send[7] = Function_WaterPump;
                bytes2send[8] = (byte)(Address_WaterPump >> 8);
                bytes2send[9] = (byte)(Address_WaterPump);
                bytes2send[10] = (byte)(RegQty_WaterPump >> 8);
                bytes2send[11] = (byte)(RegQty_WaterPump);
                //byte[] bytes2send = Encoding.UTF8.GetBytes(message);
                stream.Write(bytes2send);
                TransactionID++;
            }
        }
        private void disconnectClient()
        {
            keepConnection = false;
            setCommLost("ALARM: CLIENT STOPPED");
            invalidateDisplayedData();
        }

        //server functions
        private TcpListener server = null;
        private List<TcpClient> clients = new List<TcpClient> { };
        bool keepConnections = true;
        private void ListenConnections()
        {
            try
            {
                server = new TcpListener(IPAddress.Any, 10001);
                server.Start();
                keepConnections = true;
                disableButtonFromSubprocess(button1);
                // Check if there's a pending connection before accepting
                while (keepConnections)
                {
                    if (server.Pending())
                    {
                        TcpClient client = server.AcceptTcpClient();
                        Thread clientTask = new Thread(new ParameterizedThreadStart(HandleMessages));
                        clients.Add(client);
                        clientTask.Start(client);
                    }
                    else
                    {
                        Thread.Sleep(100); // Sleep briefly to avoid high CPU usage
                    }
                }
            }
            catch (Exception ex)
            {
                write2TextboxFromSubprocess(richTextBox1, "Connection ERROR: " + ex.Message);
            }
            //finally
            try
            {
                Thread.Sleep(1000);//allow disconnections
                clients.Clear();
                if (server != null) server.Stop();
                enableButtonFromSubprocess(button1);
            }
            catch (Exception ex)
            {
                write2TextboxFromSubprocess(richTextBox1, "Disconnect ERROR:" + ex.Message);
            }
        }
        private void HandleMessages(object obj)
        {
            write2TextboxFromSubprocess(richTextBox1, "New client");
            // Cast object back to TcpClient
            TcpClient client = (TcpClient)obj;
            // Read data from the client
            NetworkStream stream = client.GetStream();
            try
            {
                while (keepConnections && client != null && client.Connected)  // Keep reading messages
                {
                    if (!stream.DataAvailable) // Avoid blocking if no data is present
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; //disconnected

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    byte[] bytes2send = Encoding.UTF8.GetBytes(message);
                    stream.Write(bytes2send);
                }
                if (client != null && client.Connected)
                {
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                write2TextboxFromSubprocess(richTextBox1, "Client ERROR: " + ex.Message);
            }

        }
        private void disconnectServer()
        {
            keepConnections = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            disconnectClient();
            UIavilable = false;

            if (clientFlow != null)
            {
                clientFlow.Close();
            }
            if (clientPressure != null)
            {
                clientPressure.Close();
            }
            if (clientPower != null)
            {
                clientPower.Close();
            }
            if (clientWaterPump != null)
            {
                clientWaterPump.Close();
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private SerialPort serialPort1;

        public void OpenPortAndHandleMessages_Pressure()
        {
            try
            {
                disableButtonFromSubprocess(button3);
                serialPort1 = new SerialPort("COM4", 9600) // change COM and serial port 
                {
                    Parity = Parity.None,
                    DataBits = 8,
                    StopBits = StopBits.One,
                    Handshake = Handshake.None,
                    ReadTimeout = 100,
                    WriteTimeout = 1000
                };
                serialPort1.Open();
                if (serialPort1.IsOpen)
                {
                    keepConnection = true;
                    byte[] temp_buffer = new byte[1024];
                    byte[] buffer = new byte[1024];
                    int _frameTimeoutMs = 5;//more than 3 bytes
                    while (keepConnection && serialPort1.IsOpen)
                    {
                        sendMessageRTU_Pressure();
                        int temp_bytesRead = 0;
                        int bytesRead = 0;
                        Stopwatch responseTimer = new Stopwatch();
                        responseTimer.Start();
                        Stopwatch silenceTimer = new Stopwatch();
                        silenceTimer.Start();
                        while (responseTimer.ElapsedMilliseconds < 1000)
                        {
                            try
                            {
                                temp_bytesRead = serialPort1.Read(temp_buffer, 0, temp_buffer.Length);

                                if (temp_bytesRead > 0)
                                {
                                    for (int i = 0; i < temp_bytesRead; i++)
                                    {
                                        buffer[bytesRead++] = temp_buffer[i];
                                    }
                                }
                                silenceTimer.Restart();
                            }
                            catch (TimeoutException)
                            {
                                if (bytesRead > 0 && silenceTimer.ElapsedMilliseconds > _frameTimeoutMs)
                                {
                                    byte[] frame = new byte[bytesRead];
                                    Array.Copy(buffer, frame, bytesRead);
                                    //process frame
                                    processRTUresponsePressure(frame, bytesRead);
                                    bytesRead = 0; // reset buffer
                                    break;
                                }
                            }
                        }
                        while (responseTimer.ElapsedMilliseconds < 1000)
                        {
                            Thread.Sleep(10);
                        }
                    }
                }
                else
                {
                    write2TextboxFromSubprocess(richTextBox1, "Port oppenning failed");
                }
            }
            catch (Exception ex)
            {
                write2TextboxFromSubprocess(richTextBox1, $"Error with port: {ex.Message}");
            }
            if (serialPort1 != null && serialPort1.IsOpen)
            {
                serialPort1.Close();
                serialPort1.Dispose();
                serialPort1 = null;
            }
            enableButtonFromSubprocess(button3);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
