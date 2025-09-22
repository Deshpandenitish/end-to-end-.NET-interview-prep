using OopsTrials.DTO;
using OopsTrials.Extensions;

namespace OopsTrials
{
    #region Abstraction
    /// <summary>
    /// The derived classes will implement their own version of abstract method
    /// It enforces the derived classes to have their own behavior
    /// </summary>
    public abstract class Account
    {
        public readonly string AccountNumber;
        public decimal Balance { get; set; }
        protected Account(string accountNumber, decimal balance)
        {
            AccountNumber = accountNumber;
            Balance += balance;
        }
        public abstract void WithDraw(decimal Amount);// should be overriden in Dervied classes
        public void Deposit(decimal Amount) => Balance += Amount;
    }

    /// <summary>
    /// WithDraw -- Add set of conditions for Savings Accounts
    /// </summary>
    public class SavingsAccount: Account
    {
        public decimal DailyTransactionLimit { get; set; } = 25000;
        public const decimal minimumBalance = 2000;
        public SavingsAccount(string accountNumer, decimal initialBalance) : base(accountNumer, initialBalance) { }
        public override void WithDraw(decimal Amount)
        {
            if (Amount < 0)
            {
                Console.WriteLine("Withdrawal Amount must be positive !!");
                return;
            }
            if (Balance - Amount < minimumBalance)
            {
                Console.WriteLine($"Can not go below {minimumBalance}");
                return;
            }
            Balance -= Amount;
        }
    }

    /// <summary>
    /// WithDraw -- Add set of conditions for FD Accounts
    /// </summary>
    public class FixedDepositAccount: Account
    {
        decimal FDBalance { get; set; } = 0;
        public FixedDepositAccount(string accountNumer, decimal initialBalance) : base(accountNumer, initialBalance) { }
        public override void WithDraw(decimal Amount) => FDBalance -= Amount;
    }

    /// <summary>
    /// WithDraw -- Add set of conditions for RD Accounts
    /// </summary>
    public class RecurringDepositAccount: Account
    {
        decimal RDBalance { get; set; } = 0;
        public RecurringDepositAccount(string accountNumber, decimal initialBalance) : base(accountNumber, initialBalance) { }

        public override void WithDraw(decimal Amount) => RDBalance -= Amount;
    }
    #endregion

    #region Encapsulation
    public class Employee
    {
        private decimal salary { get; set; }
        public decimal GetSalary() => salary;
        public void SetSalary(decimal amount) => salary += amount;
    }
    #endregion

    #region Polymorphism
    public class MethodOverload
    {
        public void Add(int a, int b) => a += b;
        public double Add(double a, double b) => a += b;
    }
    #region Method Overriding
    public class Animal
    {
        public virtual void Speak() => Console.WriteLine("Animal Speaks !!");
    }
    public class Cat: Animal
    {
        public override void Speak()
        {
            Console.WriteLine("Cat Speaks !!");//override base speak()
        }
    }
    #endregion
    #endregion

    #region SOLID Principles Practices

    #region Liskov Substitution Principle(LSP)
    public abstract class Bird { public string Name { get; set; } = string.Empty; }
    public interface IFlyable { void Fly(); }
    public class Sparrow: Bird, IFlyable
    {
        void IFlyable.Fly() { }
    }
    #endregion

    #region Open for Extension Closed for Modification Principle(OCP)

    #endregion
    #endregion

    #region Scalable and Efficient Linq Filter Using Expression Predicates

    #endregion

    #region Scalable and Efficient Linq Filter using Dictionary

    #endregion

    internal class Program
    {
        static void RunQueries(string syntax)
        {
            #region Linq Trials
            var Departments = new List<Department> {
                new Department { DeptId=1,DeptName="HR" },
                new Department { DeptId=2, DeptName="IT" },
                new Department { DeptId=3, DeptName="FINANCE"},
            };
            var lstEmployees = new List<Employees> {
                new Employees { EmpId = 101, EmployeeName = "Harish", DeptId = 1, salary = 100000, Skills = new List<string> { "Communication","Recruitment" } },
                new Employees { EmpId = 102, EmployeeName = "Nitish", DeptId = 2, salary = 200000, Skills = new List<string> { "C#","Azure" } },
                new Employees { EmpId = 103, EmployeeName = "Shreesh", DeptId = 2, salary = 50000, Skills = new List<string> { "JavaScript","React","C#"} },
                new Employees { EmpId = 104, EmployeeName = "Rutwik", DeptId = 3, salary = 100000, Skills = new List<string> { "Excel","SAP" } },
                new Employees { EmpId = 105, EmployeeName = "Hrishikesh", DeptId = 1, salary = 50000, Skills = new List<string> { "Policy Making" } },
            };

            #region Join
            Console.WriteLine("Join Employees with Department");
            Console.WriteLine("---------------------------------------------------");
            if (syntax.ToUpper() == "Y")
            {
                Console.WriteLine("Query Based Execution");
                Console.WriteLine("---------------------------");
                var empWithDepartments = from e in lstEmployees
                                         join d in Departments on e.DeptId equals d.DeptId
                                         select new { EmployeeName = e.EmployeeName, DeptName = d.DeptName, salary = e.salary };

                foreach (var details in empWithDepartments)
                    Console.WriteLine($"Name: {details.EmployeeName} works in {details.DeptName} having salary {details.salary}");
            }
            else
            {
                Console.WriteLine("Expression Based Execution");
                Console.WriteLine("---------------------------------");
                var empWithDepartments = lstEmployees.Join(Departments,
                    e => e.DeptId,
                    d => d.DeptId,
                    (e, d) => new
                    {
                        EmployeeName = e.EmployeeName,
                        DeptName = d.DeptName,
                        salary = e.salary,
                    });

                foreach (var details in empWithDepartments)
                    Console.WriteLine($"Name: {details.EmployeeName} works in {details.DeptName} having salary {details.salary}");
            }
            Console.WriteLine();
            #endregion

            #region Employees Who Share at Least One Skill(Self Join)
            Console.WriteLine("Employees Who Share at Least One Skill");
            Console.WriteLine("---------------------------------------------------");
            var lstEmpPairsShareCmnSkills = from e1 in lstEmployees
                                            from e2 in lstEmployees
                                            where e1.EmpId < e2.EmpId && e1.Skills.Intersect(e2.Skills).Any()
                                            select new
                                            {
                                                Employee1 = e1.EmployeeName,
                                                Employee2 = e2.EmployeeName,
                                                Commonskills = string.Join(",", e1.Skills)
                                            };
            foreach (var details in lstEmpPairsShareCmnSkills)
                Console.WriteLine($"{details.Employee1} and {details.Employee2} have {details.Commonskills} skills in common.");
            Console.WriteLine();
            #endregion

            #region Department with Highest Average Salary
            Console.WriteLine("Department with Highest Average Salary");
            Console.WriteLine("---------------------------------------------------");
            if (syntax.ToUpper() == "Y")
            {
                var topSalaryDeptWise = (from e in lstEmployees
                                         group e by e.DeptId into g
                                         select new
                                         {
                                             DepartmentName = Departments.Where(e => e.DeptId == g.Key).Select(s => s.DeptName).FirstOrDefault(),
                                             AvgSalary = g.Average(s => s.salary),
                                         }).MaxBy(e => e.AvgSalary);
                Console.WriteLine($"{topSalaryDeptWise?.DepartmentName} has the highest average salary :{topSalaryDeptWise?.AvgSalary}");
            }
            else
            {
                var topSalaryDeptWise = lstEmployees.GroupBy(e => e.DeptId).Select(g => new
                {
                    DepartmentName = Departments.Where(d => d.DeptId == g.Key).Select(s => s.DeptName).FirstOrDefault(),
                    AvgSalary = g.Average(a => a.salary),
                }).MaxBy(m => m.AvgSalary);
                Console.WriteLine($"{topSalaryDeptWise?.DepartmentName} has the highest average salary :{topSalaryDeptWise?.AvgSalary}");
            }
            Console.WriteLine();
            #endregion

            #region Employee having highest salary in Each Department
            Console.WriteLine("Employee having highest salary in Each Department");
            Console.WriteLine("-------------------------------------------------------");
            if (syntax.ToUpper() == "Y")
            {
                var empHighestSalaryDeptWise = from e in lstEmployees
                                               group e by e.DeptId into g
                                               select new
                                               {
                                                   EmployeeName = string.Join(",", g.Select(e => e.EmployeeName)),
                                                   Department = Departments.Where(d => d.DeptId == g.Key).Select(s => s.DeptName).FirstOrDefault(),
                                                   HighestSalary = g.Max(h => h.salary),
                                               };
                foreach (var details in empHighestSalaryDeptWise)
                    Console.WriteLine($"{details.EmployeeName} has the highest salary of {details.HighestSalary} in {details.Department} department");
            }
            else
            {
                var empHighestSalaryDeptWise = lstEmployees.GroupBy(e => e.DeptId).Select(g => new
                {
                    EmployeeName = string.Join(",", g.Select(e => e.EmployeeName)),
                    Department = Departments.Where(d => d.DeptId == g.Key).Select(s => s.DeptName).FirstOrDefault(),
                    HighestSalary = g.Max(h => h.salary),
                });
                foreach (var details in empHighestSalaryDeptWise)
                    Console.WriteLine($"{details.EmployeeName} has the highest salary of {details.HighestSalary} in {details.Department} department");
            }
            Console.WriteLine();
            #endregion

            #region Get All Employees in each Department
            Console.WriteLine("Get All Employees in each Department");
            Console.WriteLine("-------------------------------------------------------");
            if (syntax.ToUpper() == "Y")
            {
                var deptWiseEmployees = from e in lstEmployees
                                        group e by e.DeptId into g
                                        select new
                                        {
                                            Department = Departments.Where(d => d.DeptId == g.Key).Select(s => s.DeptName).FirstOrDefault(),
                                            Employees = g.Select(e => e.EmployeeName).ToList(),
                                        };
                foreach (var details in deptWiseEmployees)
                    Console.WriteLine($"{string.Join(",", details.Employees)} are in {details.Department}.");
            }
            else
            {
                var deptWiseEmployees = lstEmployees.GroupBy(e => e.DeptId)
                    .Select(g =>
                    new
                    {
                        Department = Departments.Where(d => d.DeptId == g.Key).Select(s => s.DeptName).FirstOrDefault(),
                        Employees = g.Select(e => e.EmployeeName).ToList(),
                    });
                foreach (var details in deptWiseEmployees)
                    Console.WriteLine($"{string.Join(",", details.Employees)} are in {details.Department}.");
            }
            Console.WriteLine();
            #endregion

            #region Get Employees with more than one skill
            Console.WriteLine("Get All Employees in each Department");
            Console.WriteLine("-------------------------------------------------------");
            if (syntax.ToUpper() == "Y")
            {
                var empsHavingMultipleSkills = (from e in lstEmployees where e.Skills.Count() > 1 select e).ToList();
                foreach (var emp in empsHavingMultipleSkills)
                    Console.WriteLine($"{emp.EmployeeName} has these skills :{string.Join(",", emp.Skills)}");
            }
            else
            {
                var empsHavingMultipleSkills = lstEmployees.Where(e => e.Skills.Count > 1).ToList();
                foreach (var emp in empsHavingMultipleSkills)
                    Console.WriteLine($"{emp.EmployeeName} has these skills :{string.Join(",", emp.Skills)}");
            }
            Console.WriteLine();
            #endregion

            #region Highest Paid Employee Overall
            Console.WriteLine("Highest Paid Employee Overall");
            Console.WriteLine("-------------------------------------------------------");
            if (syntax.ToUpper() == "Y")
            {
                var highestPaidEmployee = (from e in lstEmployees orderby e.salary descending select e).First();
                Console.WriteLine($"{highestPaidEmployee.EmployeeName} has the highest salary of {highestPaidEmployee.salary}");
            }
            else
            {
                var highestPaidEmployee = lstEmployees.MaxBy(e => e.salary);
                Console.WriteLine($"{highestPaidEmployee?.EmployeeName} has the highest salary of {highestPaidEmployee?.salary}");
            }
            Console.WriteLine();
            #endregion

            #region Get Department Wise EMployees List(Group Join)
            Console.WriteLine("Get Department Wise EMployees List");
            Console.WriteLine("-------------------------------------------------------");
            if (syntax.ToUpper() == "Y")
            {
                var deptWiseEMployees = from d in Departments
                                        join e in lstEmployees on d.DeptId equals e.DeptId into empGroup
                                        select new
                                        {
                                            Department = d.DeptName,
                                            Employees = empGroup.Select(e => e.EmployeeName).ToList(),
                                        };
                foreach (var emp in deptWiseEMployees)
                    Console.WriteLine($"{emp.Department} :[{string.Join(",", emp.Employees)}]");
            }
            else
            {
                var deptWiseEMployees = Departments.GroupJoin(lstEmployees,
                    d => d.DeptId,
                    e => e.DeptId,
                    (d, empGroup) => new
                    {
                        Department = d.DeptName,
                        Employees = empGroup.Select(g => g.EmployeeName).ToList(),
                    });
                foreach (var emp in deptWiseEMployees)
                    Console.WriteLine($"{emp.Department} :[{string.Join(",", emp.Employees)}]");
            }
            Console.WriteLine();
            #endregion

            #region Employees Without Any Department (Left Join)
            Console.WriteLine("Employees Without Any Department (Left Join)");
            Console.WriteLine("-------------------------------------------------------");
            if (syntax.ToUpper() == "Y")
            {
                var empWithoutDepts = from e in lstEmployees
                                      join d in Departments on e.DeptId equals d.DeptId into empGroup
                                      from d in empGroup.DefaultIfEmpty()
                                      where d == null
                                      select e;
                foreach (var emp in empWithoutDepts)
                    Console.WriteLine($"{emp} does not belong to any department.");
            }
            else
            {
                var empWithoutDepts = from e in lstEmployees
                                      join d in Departments on e.DeptId equals d.DeptId into ed
                                      from d in ed.DefaultIfEmpty()
                                      where d is null
                                      select e;

                foreach (var emp in empWithoutDepts)
                    Console.WriteLine($"{emp} does not belong to any department.");
            }

            Console.WriteLine();
            #endregion
            #endregion
        }

        static void Main(string[] args)
        {
            Account Savings = new SavingsAccount("SAV123", 100000);
            Savings.Deposit(1000m);
            Savings.WithDraw(200000m);
            Console.WriteLine("----------------------------------");
            Animal cat = new Cat();
            cat.Speak();
            Console.WriteLine();

            Console.Write("Run Linq on Query Syntax?(Y/N) :");
            var syntax = Console.ReadLine();
            RunQueries(syntax ?? "N");

            Console.Write("Run Prdicate Query Filter(Y/N) :");
            var lstEmployees = new List<Employees> {
                new Employees { EmpId = 101, EmployeeName = "Harish", DeptId = 1, salary = 100000, Skills = new List<string> { "Communication","Recruitment" } },
                new Employees { EmpId = 102, EmployeeName = "Nitish", DeptId = 2, salary = 200000, Skills = new List<string> { "C#","Azure" } },
                new Employees { EmpId = 103, EmployeeName = "Shreesh", DeptId = 2, salary = 50000, Skills = new List<string> { "JavaScript","React","C#"} },
                new Employees { EmpId = 104, EmployeeName = "Rutwik", DeptId = 3, salary = 100000, Skills = new List<string> { "Excel","SAP" } },
                new Employees { EmpId = 105, EmployeeName = "Hrishikesh", DeptId = 1, salary = 50000, Skills = new List<string> { "Policy Making" } },
            };
            var filt = Console.ReadLine();
            var EmployeeFilter = new EmployeeFilter()
            {
                salary = 100000m,
            };
            var filteredEmployees = lstEmployees.AsQueryable().ApplyFiltersPredicate(EmployeeFilter);
            foreach (var employee in filteredEmployees)
            {
                Console.WriteLine($"Name :{employee.EmployeeName}, Salary :{employee.salary}, " +
                    $"Skills :{string.Join(",", employee.Skills)}");
            }
        }
    }
}
