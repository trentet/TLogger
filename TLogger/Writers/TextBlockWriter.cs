using System.IO;
using System.Text;
using System.Windows.Controls;

namespace TLogger.Writers
{
    public class TextBlockWriter : TextWriter
    {
        private readonly TextBlock textbox;

        public TextBlockWriter(TextBlock textblock)
        {
            textbox = textblock;
        }

        public override Encoding Encoding => Encoding.ASCII;

        public override void Write(char value)
        {
            textbox.Text += value;
        }

        public override void Write(string value)
        {
            textbox.Text += value;
        }
    }
}