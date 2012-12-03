using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XmlFormatter
{
   public class XmlFormatter
   {
      private readonly XmlFormatOptions _options;

      public XmlFormatter(XmlFormatOptions options)
      {
         _options = options;
      }

      public void Process()
      {
         TextReader input = LoadXml();
         TextWriter output = GetWriter();

         FormatXml(input, output);
      }

      private TextReader LoadXml()
      {
         if (_options.UseClipboard) {
            return LoadFromClipboard();
         }

         return LoadFromFile();
      }

      private TextReader LoadFromClipboard()
      {
         IDataObject clip = Clipboard.GetDataObject();
         if (clip.GetDataPresent(DataFormats.Text)) {
            return new StringReader((string)clip.GetData(DataFormats.Text));
         }

         throw new Exception("No data found on the clipboard!");
      }

      private TextReader LoadFromFile()
      {
         return new StreamReader(_options.InputFile);
      }

      private TextWriter GetWriter()
      {
         if (String.IsNullOrWhiteSpace(_options.OutFile)) {
            return Console.Out;
         }

         return new StreamWriter(_options.OutFile, false);
      }

      private void FormatXml(TextReader input, TextWriter output)
      {
         
         var reader = XmlReader.Create(input, new XmlReaderSettings { IgnoreWhitespace = true });
         var writer = XmlWriter.Create(output, new XmlWriterSettings {
            Indent = true,
            IndentChars = new string(' ', (int)_options.IndentLevel),
            CloseOutput = true
         });

         try {
            writer.WriteNode(reader, true);
         }
         finally {
            if (reader != null) { reader.Close(); }
            if (writer != null) { writer.Close(); }
         }
      }
   }
}
