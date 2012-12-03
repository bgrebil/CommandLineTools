using System;
using System.Collections.Generic;
using System.Linq;
using Toolbox;

namespace DeleteFiles
{
   public class DeleteFilesOptions
   {
      public DeleteFilesOptions()
      {
         Recursive = false;
         RemoveEmptyDirectories = false;
         UseRecycleBin = false;
         Days = -1;
         Seconds = -1;
      }

      public string Path { get; private set; }
      public string FileSpec { get; private set; }

      public bool Recursive { get; private set; }
      public bool RemoveEmptyDirectories { get; private set; }
      public bool UseRecycleBin { get; private set; }
      public int Days { get; private set; }
      public int Seconds { get; private set; }

      public static DeleteFilesOptions Parse(IEnumerable<string> arguments)
      {
         var parms = new DeleteFilesOptions();

         var options = new OptionSet()
            .Add("r", p => parms.Recursive = true)
            .Add("f", p => parms.RemoveEmptyDirectories = true)
            .Add("y", p => parms.UseRecycleBin = true)
            .Add("d=", d => parms.Days = Convert.ToInt32(d))
            .Add("s=", s => parms.Seconds = Convert.ToInt32(s))
            .Add("help|?", p => ShowHelp());

         var fullpath = options.Parse(arguments);

         if (!fullpath.Any()) {
            Console.WriteLine("pathspec not specified!");
            ShowHelp();
         }

         parms.Path = System.IO.Path.GetDirectoryName(fullpath[0]);
         parms.FileSpec = System.IO.Path.GetFileName(fullpath[0]);

         return parms;
      }

      private static void ShowHelp()
      {
         const string help = @"
delfiles -r -f -y -d=# -s=# <pathspec>

Options:
--------
pathSpec    Path and File Spec. Make sure to add a filespec
-r          Delete files [R]ecursively     
-f          Remove empty [F]olders
-y          Delete to Rec[Y]le Bin (can be slow!)
-d=XX       Number of [D]ays before the current date to delete            
-s=XX       Number of [S]econds before the current time to delete

-?          This help display

Seconds override days
If neither -d or -s no date filter is applied

Examples:
---------
delfiles -r -f c:\temp\*.*         - deletes all files in temp folder recursively 
                                     and deletes empty folders
delfiles -r -f -d=10 c:\temp\*.*    - delete files 10 days or older 
delfiles -r -f -s=3600 c:\temp\*.*  - delete files older than an hour
delfiles -r ""c:\My Files\*.*""      - deletes all files in temp folder recursively
";

         Console.WriteLine(help);
         Environment.Exit(0);
      }
   }
}
