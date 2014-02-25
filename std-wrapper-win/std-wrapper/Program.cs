using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace std_wrapper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var str = OpenStandardStreamIn();
            var obj = JsonConvert.DeserializeAnonymousType(str, new {Cmd = string.Empty, Arguments = string.Empty});
            Process.Start(obj.Cmd, obj.Arguments);
        }

        private static string OpenStandardStreamIn()
        {
            //// We need to read first 4 bytes for length information
            var stdin = Console.OpenStandardInput();
            var bytes = new byte[4];
            stdin.Read(bytes, 0, 4);
            var length = BitConverter.ToInt32(bytes, 0);

            var stringBuilder = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                stringBuilder.Append((char) stdin.ReadByte());
            }
            return stringBuilder.ToString();
        }

        private static void OpenStandardStreamOut(string stringData)
        {
            //// We need to send the 4 btyes of length information
            var dataLength = stringData.Length;
            var stdout = Console.OpenStandardOutput();
            stdout.WriteByte((byte) ((dataLength >> 0) & 0xFF));
            stdout.WriteByte((byte) ((dataLength >> 8) & 0xFF));
            stdout.WriteByte((byte) ((dataLength >> 16) & 0xFF));
            stdout.WriteByte((byte) ((dataLength >> 24) & 0xFF));
            //Available total length : 4,294,967,295 ( FF FF FF FF )

            Console.Write(stringData);
        }
    }
}
