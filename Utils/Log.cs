using System;
using System.IO;

namespace AFD.Utils
{
    public static class Log
    {
        public static void LogMessage(string message)
        {
            using (StreamWriter w = File.AppendText($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/log.txt"))
            {
                w.WriteLine("-------------------------------");
                w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                w.WriteLine($":{message}");
                w.WriteLine("-------------------------------");
            }
        }
    }
}