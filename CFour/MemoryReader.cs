using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CFour
{
    public class MemoryReader
    {
        public static MemoryReader Instance { get; private set; }

        private IntPtr m_C4Pointer;

        [DllImport("kernel32.dll")]
        private static extern int ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In] [Out] byte[] lpBuffer, uint nSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In] [Out] byte[] lpBuffer, uint nSize, out IntPtr lpNumberOfBytesWritten);

        public MemoryReader()
        {
            Instance = this;
        }

        public bool Process_Handle(string processName)
        {
            bool result;
            try
            {
                var processesByName = Process.GetProcessesByName(processName);
                if (processesByName.Length == 0)
                {
                    result = false;
                }
                else
                {
                    m_C4Pointer = processesByName[0].Handle;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public void CFourHandle()
        {
            m_C4Pointer = (IntPtr)(-1);
        }

        private byte[] ReadBytesAt(int index, int bufferLength)
        {
            byte[] array = new byte[bufferLength];
            IntPtr zero = IntPtr.Zero;
            ReadProcessMemory(m_C4Pointer, (IntPtr)index, array, (uint)array.Length, out zero);
            return array;
        }

        private void WriteBytesAt(int index, int data)
        {
            byte[] bytes = BitConverter.GetBytes(data);
            IntPtr zero = IntPtr.Zero;
            WriteProcessMemory(m_C4Pointer, (IntPtr)index, bytes, (uint)bytes.Length, out zero);
        }

        public void Write(int index, int value)
        {
            WriteBytesAt(index, value);
        }

        public void Write(int index, string value)
        {
            var bytes = new ASCIIEncoding().GetBytes(value);
            WriteProcessMemory(m_C4Pointer, (IntPtr)index, bytes, (uint)bytes.Length, out var bytesWritten);
        }

        public void Write(int index, byte[] value)
        {
            WriteProcessMemory(m_C4Pointer, (IntPtr)index, value, (uint)value.Length, out var bytesWritten);
        }

        public void WriteNOP(int index)
        {
            var array = new byte[]
            {
                144,
                144,
                144,
                144,
                144
            };
            WriteProcessMemory(m_C4Pointer, (IntPtr)index, array, (uint)array.Length, out var bytesWritten);
        }

        public int ReadInteger(int index, int length = 4)
        {
            return BitConverter.ToInt32(ReadBytesAt(index, length), 0);
        }

        public string ReadString(int index, int length = 4)
        {
            return new ASCIIEncoding().GetString(ReadBytesAt(index, length));
        }

        public byte[] ReadBytes(int index, int length)
        {
            return ReadBytesAt(index, length);
        }
    }
}
