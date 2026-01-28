using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExample.Model
{
    public class Employee
    {
        public string Name { get; set; }
        public ObservableCollection<SalaryRecord> SalaryRecords { get; set; } = new ObservableCollection<SalaryRecord>();
    }

    public class SalaryRecord
    {
        public string Mount { get; set; }
        public double Amount { get; set; }
        public string InfoSalary => Mount + " " + Convert.ToString(Amount);
    }
}
