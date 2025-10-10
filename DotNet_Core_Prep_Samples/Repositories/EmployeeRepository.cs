using DotNet_Core_Prep_Samples.Data;
using DotNet_Core_Prep_Samples.Entities;
using DotNet_Core_Prep_Samples.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Core_Prep_Samples.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly TrialsContext _dbContext;
        public EmployeeRepository(TrialsContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var employees = await (from emp in _dbContext.Employees select emp).ToListAsync();
            return employees;
        }
    }
}
