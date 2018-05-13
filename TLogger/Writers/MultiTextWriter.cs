using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TLogger.Writers
{
    public class MultiTextWriter : TextWriter
    {
        private readonly IEnumerable<TextWriter> writers;

        public MultiTextWriter(IEnumerable<TextWriter> writers)
        {
            this.writers = writers.ToList();
        }

        public MultiTextWriter(params TextWriter[] writers)
        {
            //if (writers[0].GetType() == typeof(TLogger.Writers.LogWriter))
            //{
            //    //this.writers[0] = writers[0];
            //}

            //for (int x = 0; x < writers.Length; x++)
            //{
            //    if (x == 0)
            //    {
            //        writers.
            //    }
            //}

            this.writers = writers;
        }

        public override Encoding Encoding => Encoding.ASCII;

        public override void Write(char value)
        {
            foreach (var writer in writers)
                writer.Write(value);
        }

        public override void Write(string value)
        {
            foreach (var writer in writers)
                writer.Write(value);
        }

        public override void Flush()
        {
            foreach (var writer in writers)
                writer.Flush();
        }

        public override void Close()
        {
            foreach (var writer in writers)
                writer.Close();
        }
    }
}