using System;

namespace myModbusPollV1
{
	public class myCRC
	{
        private const ushort polynomial = 0xA001;
        private readonly ushort[] table = new ushort[256];

        public myCRC()
        {
            Crc16init();
        }

        public Int16 ComputeCRC(byte[] bytes, int contbytes)
        {
        ushort crc = 0xFFFF;
            for (int i = 0; i < contbytes; ++i)
            {
                byte index = (byte)(crc ^ bytes[i]);
                crc = (ushort)((crc >> 8) ^ table[index]);
            }
        return (Int16)(crc >> 8 | (crc << 8));
        }

        public void Crc16init()
        {
            ushort value;
            ushort temp;
            for (ushort i = 0; i < table.Length; ++i)
            {
                value = 0;
                temp = i;
                for (byte j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (ushort)((value >> 1) ^ polynomial);
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                table[i] = value;
            }
        }
    }
}
