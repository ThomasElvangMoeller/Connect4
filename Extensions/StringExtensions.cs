using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4.Extensions
{
    public static class StringExtensions
    {
        public static List<int> ParseAll(this string[] sInts)
        {
            List<int> newInts = new List<int>();
            foreach (var item in sInts)
            {
                if (int.TryParse(item, out int oInt))
                {
                    newInts.Add(oInt);
                }
            }

            return newInts;
        }

        public static string ToHexString(this string str)
        {
            StringBuilder sb = new StringBuilder();

            byte[] bytes = Encoding.Unicode.GetBytes(str);
            foreach (byte t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString(); // returns: "48656C6C6F20776F726C64" for "Hello world"
        }

        public static string FromHexString(this string hexString)
        {
            byte[] bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.Unicode.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
        }
    }
}