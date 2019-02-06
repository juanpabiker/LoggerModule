using Logger.Core;
using System;
using System.IO;

namespace Logger.File
{
    public class Logger : IPlugin
    {
        public static string Formatting = "{0,-20} - {1,-10} - {2,-20}";

        public void Log(string type, string message)
        {
            var messegeFinal = string.Format(Formatting, new string[] { string.Format("{0:G}", DateTime.Now), type, message });
            using (StreamWriter writetext = new StreamWriter("log.txt"))
            {
                writetext.WriteLine(messegeFinal);
            }
        }
    }
}
