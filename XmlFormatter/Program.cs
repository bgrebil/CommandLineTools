using System;
using System.Linq;

namespace XmlFormatter
{
   class Program
   {
      [STAThread]
      static void Main(string[] args)
      {
         try {
            var options = XmlFormatOptions.Parse(args);

            var formatter = new XmlFormatter(options);
            formatter.Process();

         }
         catch (Exception ex) {
            Console.WriteLine(ex);
            Environment.Exit(1);
         }
      }
   }
}
