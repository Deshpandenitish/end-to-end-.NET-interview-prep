namespace DotNet_Core_Prep_Samples.Entities
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public decimal salary { get; set; }
        public int DeptId { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
    }
}
