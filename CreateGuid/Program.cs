using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CreateGuid
{
   class Program
   {
      [STAThread]
      static void Main(string[] args)
      {
         try {
            var options = CreateGuidOptions.Parse(args);
            var guids = CreateGuids(options);
            if (String.IsNullOrWhiteSpace(options.OutFile)) {
               CopyToClipboard(guids);
            }
            else {
               SendToFile(guids, options);
            }
         }
         catch (Exception ex) {
            Console.WriteLine(ex);
            Environment.Exit(1);
         }
      }

      private static string CreateGuids(CreateGuidOptions options)
      {
         using (var sw = new StringWriter()) {
            for (int ii = 0; ii < options.Count; ++ii) {
               string guid = Guid.NewGuid().ToString(options.Format.ToString()).ToUpper();
               if (options.Count == 1) {
                  sw.Write(guid);
               }
               else {
                  sw.WriteLine(guid);
               }
            }

            return sw.ToString();
         }
      }

      private static void CopyToClipboard(string guids)
      {
         Clipboard.SetDataObject(guids, true);
         Console.WriteLine(guids);
      }

      private static void SendToFile(string guids, CreateGuidOptions options)
      {
         File.WriteAllText(options.OutFile, guids);
      }
   }
}
