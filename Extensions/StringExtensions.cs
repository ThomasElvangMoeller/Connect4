using System.Collections.Generic;

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
    }
}