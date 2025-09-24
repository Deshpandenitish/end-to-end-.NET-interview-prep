using OopsTrials.DTO;

namespace OopsTrials.Extensions
{
    internal static class QueryableExtensions
    {
        public static IQueryable<Employees> ApplyFiltersPredicate(this IQueryable<Employees> Source, EmployeeFilter employeeFilter)
        {
            var predicate = PredicateBuilder.True<Employees>();
            if (!string.IsNullOrEmpty(employeeFilter.EmployeeName))
                predicate = predicate.And(e => e.EmployeeName == employeeFilter.EmployeeName);

            predicate = predicate.And(e => e.salary > employeeFilter.salary);

            return Source.Where(predicate);
        }
    }
}
