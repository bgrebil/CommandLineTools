using System;
using System.Linq;
using System.Xml.XPath;

namespace XPathQuery
{
   class Program
   {
      static void Main(string[] args)
      {
         if (args.Length != 2 || args.Any(p => p == "/?" || p == "-?")) {
            ShowHelp();
         }

         try {
            var doc = new XPathDocument(args[0]);
            XPathNavigator nav = doc.CreateNavigator();

            var o = nav.Evaluate(args[1]);
            if (o is XPathNodeIterator) {
               var iter = o as XPathNodeIterator;
               while (iter.MoveNext()) {
                  Console.WriteLine(iter.Current.Value);
               }
            }
            else {
               Console.WriteLine(o);
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
xpq <input> <xpath>

Options:
--------
input    - the input xml file
xpath    - the xpath expression to query
";

         Console.Write(help);
         Environment.Exit(0);
      }
   }
}
