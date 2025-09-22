using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopsTrials.Extensions
{
    internal class EmployeeFilter
    {
        public string EmployeeName { get; set; } = string.Empty;
        public decimal salary { get; set; }
        public int DeptId { get; set; }
        public int Age { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
