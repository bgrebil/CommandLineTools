using System;
using System.IO;
using System.Linq;

namespace Which
{
   class Program
   {
      static void Main(string[] args)
      {
         try {
            if (!args.Any() || args.Any(p => p == "/?" || p == "-?")) {
               ShowHelp();
            }

            var paths = Environment.GetEnvironmentVariable("PATH").Split(';');
            foreach (string exe in args) {
               foreach (string path in paths) {
                  string candidatePath = Path.Combine(path, exe);
                  if (File.Exists(candidatePath)) {
                     Console.WriteLine(candidatePath);
                     break;
                  }
               }
            }
         }
         catch (Exception ex) {
            Console.WriteLine(ex);
            Environment.Exit(1);
         }
      }

      private static void ShowHelp()
      {
         const string help = @"
which <file.exe>[, <file2.exe>, ...]

Options:
--------
file  - executable to search for in the PATH environment variable

-?    - This help display
         ";

         Console.WriteLine(help);
         Environment.Exit(0);
      }
   }
}
