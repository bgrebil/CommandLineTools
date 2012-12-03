using System;
using System.Collections.Generic;
using System.Linq;
using Toolbox;

namespace XmlFormatter
{
   public class XmlFormatOptions
   {
      public string InputFile { get; private set; }
      public string OutFile { get; private set; }
      public uint IndentLevel { get; private set; }
      public bool UseClipboard { get; private set; }

      public XmlFormatOptions()
      {
         OutFile = null;
         IndentLevel = 3;
         UseClipboard = false;
      }

      public static XmlFormatOptions Parse(IEnumerable<string> arguments)
      {
         var parms = new XmlFormatOptions();

         var options = new OptionSet()
            .Add("o=|outfile", o => parms.OutFile = o)
            .Add("t=|indent", t => parms.IndentLevel = Convert.ToUInt32(t))
            .Add("c|clip", c => parms.UseClipboard = true)
            .Add("?|help", p => ShowHelp());

         parms.InputFile = options.Parse(arguments).FirstOrDefault();

         if (String.IsNullOrWhiteSpace(parms.InputFile) && !parms.UseClipboard) {
            Console.WriteLine("Input file not specified!");
            ShowHelp();
         }

         return parms;
      }

      private static void ShowHelp()
      {
         const string help = @"
xf [-i=3] [-o=<output>] [-c] input.xml

Options:
--------
input    - The XML file to format
-o       - Specifies the output file, if not specified text is dumped to console
-i       - Specifies the spacing to use on each indent level. Default = 3.
-c       - Read the input from the clipboard instead of a file
         ";

         Console.WriteLine(help);
         Environment.Exit(0);
      }
   }
}
