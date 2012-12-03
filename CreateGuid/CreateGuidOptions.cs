using System;
using System.Collections.Generic;
using System.Linq;
using Toolbox;

namespace CreateGuid
{
   // Matches .NET formats for GUIDS
   public enum GuidFormat { N, D, B, P };

   public class CreateGuidOptions
   {
      public GuidFormat Format { get; private set; }
      public string OutFile { get; private set; }
      public uint Count { get; private set; }

      public CreateGuidOptions()
      {
         Format = GuidFormat.B;
         OutFile = null;
         Count = 1;
      }

      public static CreateGuidOptions Parse(IEnumerable<string> arguments)
      {
         var parms = new CreateGuidOptions();

         var options = new OptionSet()
            .Add("f=|format", f => parms.Format = (GuidFormat)Enum.Parse(typeof(GuidFormat), f))
            .Add("o=|outfile", o => parms.OutFile = o)
            .Add("n=|num", n => parms.Count = Convert.ToUInt32(n))
            .Add("?|help", p=> ShowHelp());

         options.Parse(arguments);

         return parms;
      }

      private static void ShowHelp()
      {
         const string help = @"
cguid [-f=<format>] [-o=<file>] [-n=#]

Options:
--------
-f    GUID string format. Values match .NET format specifiers (N, D, B, P). Default = B.
-o    Write output to named file. When not specified, output is copied to clipboard.
-n    Create n GUIDs.

-?    This help display.

Examples:
---------
cguid                      - Creates a single GUID copied to the clipboard
cguid -n=5                 - Creates 5 GUIDs copied to the clipboard
cguid -n=20 -o=output.txt  - Creates 20 GUIDs written to the output.txt file
";

         Console.WriteLine(help);
         Environment.Exit(0);
      }
   }
}
