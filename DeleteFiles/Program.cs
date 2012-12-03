using System;
using System.Linq;

namespace DeleteFiles
{
   class Program
   {
      static void Main(string[] args)
      {
         try {
            var options = DeleteFilesOptions.Parse(args);

            var processor = new DeleteFilesProcessor();
            processor.ProcessFiles(options);

         }
         catch (Exception ex) {
            Console.WriteLine(ex);
            Environment.Exit(1);
         }
      }
   }
}
