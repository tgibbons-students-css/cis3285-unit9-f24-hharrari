using System;

using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple
{
    public class ConsoleLogger : ILogger
    {
        public void LogWarning(string message, params object[] args)
        {
            Console.WriteLine(string.Concat("WARN: ", message), args);
        }

        public void LogInfo(string message, params object[] args)
        {
            Console.WriteLine(string.Concat("INFO: ", message), args);
        }
       

        private void LogMessage(string type, string message, params object[] args)
        {
            //formatting the message first so that args will have the actual values in both the console and the file
            String formattedMessage = string.Format(message, args);
            Console.WriteLine($"{type}: {formattedMessage}");
            
            using (StreamWriter logfile = File.AppendText("log.xml"))
            {
                logfile.WriteLine($"<log><type>{type}</type><message>{formattedMessage}</message></log>");
            }
            
        }
    }
}
