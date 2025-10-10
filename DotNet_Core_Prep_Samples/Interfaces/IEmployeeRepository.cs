using DotNet_Core_Prep_Samples.Entities;

namespace DotNet_Core_Prep_Samples.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee>> GetAllEmployeesAsync();
    }
}
