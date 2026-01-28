using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace TestExample.Services
{
    public class XmlService
    {

        public static string XsltTransform(string xsltFile, string inputFile)
        {
            string outputFile = @"Resources\Output\Employee.xml";

            if (!File.Exists(@"Resources\Input\Data1.xml") || !File.Exists(@"Resources\Input\Data1.xml"))
                return string.Empty;

            if (inputFile == string.Empty)
                return string.Empty;

            XslCompiledTransform xslt = new XslCompiledTransform();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Ignore;

            using (XmlReader reader = XmlReader.Create(xsltFile, settings))
            {
                xslt.Load(reader);
            }

            using (StringWriter sw = new StringWriter())
            {
                xslt.Transform(inputFile, null, sw);
                string result = sw.ToString();
                File.WriteAllText(outputFile, result);
                return result;
            }
        }

    }

    public enum InputFileEnum
    {
        DataOne,
        DataTwo
    }
}
