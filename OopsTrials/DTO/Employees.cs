namespace OopsTrials.DTO
{
    internal class Employees
    {
        public int EmpId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public decimal salary { get; set; }
        public int DeptId { get; set; }
        public int Age { get; set; }
        public string Location { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = new List<string>();
    }
}
