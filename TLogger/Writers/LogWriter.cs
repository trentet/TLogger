using System.IO;
using System.Text;
using TLogger.Controller;

namespace TLogger.Writers
{
    public class LogWriter : TextWriter
    {
        private string line;
        private LogHandler logHandler;

        public LogWriter(ref LogHandler logHandler)
        {
            NewLine = "/r/n";
            this.logHandler = logHandler;
        }

        public override Encoding Encoding => Encoding.ASCII;

        public override void Write(char value)
        {
            line += value;
            if (value == '\r')
            {
                //Console.WriteLine();
            }

            if (value == '\n')
            {
                logHandler.Logs.Add(line);
                line = "";
            }
        }

        public override void Write(string value)
        {
            logHandler.Logs.Add(value);
        }
    }
}