using System;
using System.Linq;
using System.Xml;
using System.Xml.Schema;

namespace XmlSchemaValidator
{
   class Program
   {
      static void Main(string[] args)
      {
         if (args.Length != 2 || args.Any(p => p == "/?" || p == "-?")) {
            ShowHelp();
         }

         ValidateXml(args[0], args[1]);
      }

      private static void ValidateXml(string input, string schema)
      {
         var schemas = new XmlSchemaSet();
         schemas.Add(null, XmlReader.Create(schema));

         var settings = new XmlReaderSettings { Schemas = schemas };
         settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
         settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
         settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
         settings.ValidationEventHandler += OnValidationError;

         var reader = XmlReader.Create(input, settings);
         while (reader.Read()) ;

      }

      static void OnValidationError(object sender, ValidationEventArgs e)
      {
         if (e.Severity == XmlSeverityType.Warning)
            Console.WriteLine("\tWarning: Matching schema not found. No validation occurred. " + e.Message);
         else
            Console.WriteLine("\tValidation error: " + e.Message);
      }

      private static void ShowHelp()
      {
         const string help = @"
xsv input.xml schema.xsd

Options:
--------
input    - The xml to validate
schema   - The schema to validate against
         ";

         Console.WriteLine(help);
         Environment.Exit(0);
      }
   }
}
