using myModbusPollV1;
using System.Diagnostics;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
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

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread subproces = new Thread(Connect2Server);
            //Thread subprocess = new Thread(OpenPortAndHandleMessages);
            subproces.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {

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
        //client functions
        TcpClient client = null;
        bool keepConnection = false;
        private void AutomaticRequest()
        {
            while (keepConnection)
            {
                //sendMessageRTUoTCP();
                sendMessageTCP();
                Thread.Sleep(1000);
            }
        }
        private bool processTCPresponse(byte[] buffer)
        {
            bool result = false;
            if (buffer[2] == 0 && buffer[3] == 0)
            {
                write2TextboxFromSubprocess(richTextBox1, "ProtocolID for TCP ok");
                int TCPMessageLength = 3 + 2 * RegQty; // Calculate length 
                if (buffer[4] * 256 + buffer[5] == TCPMessageLength)
                {
                    write2TextboxFromSubprocess(richTextBox1, "MessageLength Ok");

                    if (buffer[6] == ModbusID)
                    {
                        write2TextboxFromSubprocess(richTextBox1, "ModbusID Ok");

                        if (buffer[7] == Function)
                        {
                            write2TextboxFromSubprocess(richTextBox1, "Function Ok");

                            if (buffer[8] == 2 * RegQty)
                            {
                                result = true;
                                write2TextboxFromSubprocess(richTextBox1, "Data bytes length Ok");
                                // byte[9] and byte[10] = reg-12 -- Temp Starts (Float Little Endian)
                                // byte[11] and byte[12] = reg-13 -- Temp continues 
                                // byte[13] and byte[14] = reg-14 -- 
                                // byte[15] and byte[16] = reg-15
                                // byte[17] and byte[18] = reg-16
                                float Temperature = BitConverter.ToSingle(buffer, 9);
                                write2LabelFromSubprocess(label1, $"The temperature is {Temperature}");
                                byte[] newArray = new byte[4];
                                newArray[0] = buffer[16];          //
                                newArray[1] = buffer[15];         // Transformar Big Endian to Little Endian 
                                newArray[2] = buffer[14];        //
                                newArray[3] = buffer[13];       //
                                float Preassure = BitConverter.ToSingle(newArray, 0);
                                write2LabelFromSubprocess(label2, $"The preassure is {Preassure}");
                                write2LabelFromSubprocess(label3, $"The flow is {buffer[17] * 256 + buffer[18]}");
                            }
                        }
                    }
                }
            }
            return result;
        }
        private bool processRTUresponse(byte[] buffer, int bytesReceived)
        {
            bool result = false;
            if (true) // NO protocol check needed 
            {
                if (crcObject.ComputeCRC(buffer, bytesReceived) == 0)
                {
                    {
                        write2TextboxFromSubprocess(richTextBox1, "CRC ok");

                        if (buffer[0] == ModbusID)
                        {
                            write2TextboxFromSubprocess(richTextBox1, "ModbusID Ok");

                            if (buffer[1] == Function)
                            {
                                write2TextboxFromSubprocess(richTextBox1, "Function Ok");

                                if (buffer[2] == 2 * RegQty)
                                {
                                    result = true;
                                    write2TextboxFromSubprocess(richTextBox1, "Data bytes length Ok");
                                    // byte[3] and byte[4] = reg-12 -- Temp Starts (Float Little Endian)
                                    // byte[5] and byte[6] = reg-13 -- Temp continues 
                                    // byte[7] and byte[8] = reg-14 -- 
                                    // byte[9] and byte[10] = reg-15
                                    // byte[11] and byte[12] = reg-16
                                    float Temperature = BitConverter.ToSingle(buffer, 9);
                                    write2LabelFromSubprocess(label1, $"The temperature is {Temperature}");
                                    byte[] newArray = new byte[4];
                                    newArray[0] = buffer[10];          //
                                    newArray[1] = buffer[9];         // Transformar Big Endian to Little Endian 
                                    newArray[2] = buffer[8];        //
                                    newArray[3] = buffer[7];       //
                                    float Preassure = BitConverter.ToSingle(newArray, 0);
                                    write2LabelFromSubprocess(label2, $"The preassure is {Preassure}");
                                    write2LabelFromSubprocess(label3, $"The flow is {buffer[11] * 256 + buffer[12]}");
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        private void Connect2Server()
        {
            try
            {
                disableButtonFromSubprocess(button1);
                client = new TcpClient("127.0.0.1", 502);
                if (client.Connected)
                {
                    keepConnection = true;
                    Thread pollingSubprocess = new Thread(AutomaticRequest);
                    pollingSubprocess.Start();
                    NetworkStream stream = client.GetStream();
                    int bytesRead = 0;
                    byte[] buffer = new byte[1024];
                    while (keepConnection && client.Connected)
                    {
                        if (!stream.DataAvailable)
                        {
                            bytesRead = 0;
                        }
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                        {
                            break;
                        }
                        else
                        {
                            // string text = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            write2TextboxFromSubprocess(richTextBox1, "New Message");
                            //processRTUresponse(buffer, bytesRead);
                            processTCPresponse(buffer);
                        }
                    }
                }
                else
                {
                    write2TextboxFromSubprocess(richTextBox1, "connection failed");
                }
            }
            catch (Exception ex)
            {
                write2TextboxFromSubprocess(richTextBox1, ex.ToString());
            }
            finally
            {
                if (client != null && client.Connected)
                {
                    NetworkStream stream = client.GetStream();
                    stream.Close();
                    client.Close();
                }
                enableButtonFromSubprocess(button1);
            }
        }

        Int16 TransactionID = 0;
        Int16 ProtocolID = 0; // MODBUS-TCP = 0
        Int16 MessageLength = 6; // For functions 3 & 4
        byte ModbusID = 17; // ID in Modbus
        byte Function = 4; // Function in Modbus 
        Int16 Address = 12; // Start adress to read
        Int16 RegQty = 5;

        myCRC crcObject = new myCRC();

        private void sendMessageRTUoTCP(/*RichTextBox textBox*/)
        {
            if (UIavilable && client != null && client.Connected)
            {
                //string message = textBox.Text;
                //clearTextboxFromSubprocess(textBox);

                NetworkStream stream = client.GetStream();
                byte[] bytes2send = new byte[8];
                bytes2send[0] = ModbusID;
                bytes2send[1] = Function;
                bytes2send[2] = (byte)(Address >> 8);
                bytes2send[3] = (byte)(Address);
                bytes2send[4] = (byte)(RegQty >> 8);
                bytes2send[5] = (byte)(RegQty);
                Int16 CRC = crcObject.ComputeCRC(bytes2send, 6);
                bytes2send[6] = (byte)(CRC >> 8);
                bytes2send[7] = (byte)(CRC);
                //byte[] bytes2send = Encoding.UTF8.GetBytes(message);

                stream.Write(bytes2send);
            }
        }
        private void sendMessageTCP(/*RichTextBox textBox*/)
        {
            if (UIavilable && client != null && client.Connected)
            {
                //string message = textBox.Text;
                //clearTextboxFromSubprocess(textBox);

                NetworkStream stream = client.GetStream();
                byte[] bytes2send = new byte[12];
                bytes2send[0] = (byte)(TransactionID >> 8);
                bytes2send[1] = (byte)(TransactionID);
                bytes2send[2] = (byte)(ProtocolID >> 8);
                bytes2send[3] = (byte)(ProtocolID);
                bytes2send[4] = (byte)(MessageLength >> 8);
                bytes2send[5] = (byte)(MessageLength);
                bytes2send[6] = ModbusID;
                bytes2send[7] = Function;
                bytes2send[8] = (byte)(Address >> 8);
                bytes2send[9] = (byte)(Address);
                bytes2send[10] = (byte)(RegQty >> 8);
                bytes2send[11] = (byte)(RegQty);
                //byte[] bytes2send = Encoding.UTF8.GetBytes(message);
                stream.Write(bytes2send);
                TransactionID++;
            }
        }
        private void disconnectClient()
        {
            keepConnection = false;
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
            if (client != null)
            {
                client.Close();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private SerialPort serialPort1;
        private void sendMessageRTU()
        {
            byte[] bytes2send = new byte[8];
            bytes2send[0] = ModbusID;
            bytes2send[1] = 0x04;//function 4
            bytes2send[2] = (byte)(Address >> 8);
            bytes2send[3] = (byte)(Address);
            bytes2send[4] = (byte)(RegQty >> 8);
            bytes2send[5] = (byte)(RegQty);
            Int16 messageCRC = crcObject.ComputeCRC(bytes2send, 6);
            bytes2send[6] = (byte)(messageCRC >> 8);
            bytes2send[7] = (byte)(messageCRC);
            serialPort1.Write(bytes2send, 0, 8);
            serialPort1.BaseStream.Flush();
        }

        public void OpenPortAndHandleMessages()
        {
            try
            {
                disableButtonFromSubprocess(button1);
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
                        sendMessageRTU();
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
                                    processRTUresponse(frame, bytesRead);
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
            enableButtonFromSubprocess(button1);
        }
    }
}
