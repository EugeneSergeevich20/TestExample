using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using TestExample.Model;
using TestExample.Services;

namespace TestExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            fileEnum = InputFileEnum.DataOne;
            inputFile = @"Resources\Input\Data1.xml";
            xsltFile = @"Resources\Data1Converter.xslt";
        }

        private string employeeXml = string.Empty;
        private InputFileEnum fileEnum;

        string xsltFile = string.Empty;
        string inputFile = string.Empty;

        private void XmlTransform_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                employeeXml = XmlService.XsltTransform(xsltFile, inputFile);

                XmlService.AddTotalAmountAttribute(employeeXml);

                XmlService.AddTotalAmountToData1Xml();

                EmployeeDataTable();
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EmployeeDataTable()
        {
            if (!File.Exists(@"Resources\Output\Employee.xml"))
                return;

            XDocument doc = XDocument.Load(@"Resources\Output\Employee.xml");
            List<Employee> employees = new List<Employee>();

            foreach (var employee in doc.Descendants("Employee"))
            {
                Employee emp = new Employee();
                emp.Name = $"{employee.Attribute("name")?.Value} {employee.Attribute("surname")?.Value}";
                string totalAmount = employee.Attribute("totalAmount")?.Value;

                foreach (var salary in employee.Elements("salary"))
                {
                    SalaryRecord salaryRecord = new SalaryRecord();
                    salaryRecord.Mount = salary.Attribute("mount")?.Value;
                    string amountStr = salary.Attribute("amount")?.Value;

                    if (!string.IsNullOrEmpty(amountStr))
                    {
                        amountStr = amountStr.Replace('.', ',');
                        if (double.TryParse(amountStr, out double amount))
                        {
                            salaryRecord.Amount = amount;
                        }
                    }
                    emp.SalaryRecords.Add(salaryRecord);
                }
                employees.Add(emp);
            }
            employeeTreeView.ItemsSource = employees;
        }

        private void rbOne_Checked(object sender, RoutedEventArgs e)
        {
            fileEnum = InputFileEnum.DataOne;

            inputFile = @"Resources\Input\Data1.xml";
            xsltFile = @"Resources\Data1Converter.xslt";
        }

        private void rbTwo_Checked(object sender, RoutedEventArgs e)
        {
            fileEnum = InputFileEnum.DataTwo;

            inputFile = @"Resources\Input\Data2.xml";
            xsltFile = @"Resources\Data2Converter.xslt";
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            AddRecordWindow addRecordWindow = new AddRecordWindow();
            if (addRecordWindow.ShowDialog() == true)
            {
                XmlService.AddRecordToData1Xml(addRecordWindow.name.Text, addRecordWindow.surname.Text, addRecordWindow.amount.Text, addRecordWindow.mount.Text);
                XmlTransform_Click(sender, e);
            }
        }
    }
}