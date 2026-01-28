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
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
    }
}