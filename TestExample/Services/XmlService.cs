using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using TestExample.Model;

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
                File.WriteAllText(outputFile, result, Encoding.UTF8);
                return result;
            }
        }

        public static void AddTotalAmountAttribute(string xmlText)
        {
            XDocument doc = XDocument.Parse(xmlText);

            foreach (var employee in doc.Descendants("Employee"))
            {
                double total = 0;
                foreach (var salary in employee.Elements("salary"))
                {
                    string amountStr = salary.Attribute("amount")?.Value;
                    if (!string.IsNullOrEmpty(amountStr))
                    {
                        amountStr = amountStr.Replace('.', ',');
                        if (double.TryParse(amountStr, out double amount))
                        {
                            total += amount;
                        }
                    }
                }
                employee.SetAttributeValue("totalAmount", total.ToString("F2"));
            }
            doc.Save(@"Resources\Output\Employee.xml");
        }

        public static void AddTotalAmountToData1Xml()
        {
            if (!File.Exists(@"Resources\Input\Data1.xml"))
                return;

            XDocument doc = XDocument.Load(@"Resources\Input\Data1.xml");
            double total = 0;

            foreach (var item in doc.Descendants("item"))
            {
                string amountStr = item.Attribute("amount")?.Value;
                if (!string.IsNullOrEmpty(amountStr))
                {
                    amountStr = amountStr.Replace('.', ',');
                    if (double.TryParse(amountStr, out double amount))
                    {
                        total += amount;
                    }
                }
            }
            doc.Root.SetAttributeValue("totalAmount", total.ToString("F2"));
            doc.Save(@"Resources\Input\Data1.xml");
        }

        public static void AddRecordToData1Xml(string name, string surname, string amount, string month)
        {
            if (!File.Exists(@"Resources\Input\Data1.xml"))
                return;

            XDocument doc = XDocument.Load(@"Resources\Input\Data1.xml");

            XElement newItem = new XElement("item",
                new XAttribute("name", name),
                new XAttribute("surname", surname),
                new XAttribute("amount", amount),
                new XAttribute("mount", month)
            );

            doc.Root.Add(newItem);
            doc.Save(@"Resources\Input\Data1.xml");
        }

    }

    public enum InputFileEnum
    {
        DataOne,
        DataTwo
    }
}
