using GitApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Domain
{
    public static class UtilityServices 
    {
        public static int GetAsciiValueFirstChar(string word)
        {
            byte[] asciiVal = Encoding.ASCII.GetBytes(word);
            return asciiVal[0];
        }

        public static int GetAsciiValue(string word)
        {
            byte[] asciiVal = Encoding.ASCII.GetBytes(word);
            var total = 0;
            foreach(var count in asciiVal)
            {
                total += count;
            }
            return total;
        }

        public static string RemoveEscapeChars(string statement)
        {
            return statement.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace(",","").Replace(".","");
        }
    }
}
