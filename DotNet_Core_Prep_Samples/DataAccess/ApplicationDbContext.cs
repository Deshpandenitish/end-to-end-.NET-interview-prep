using DotNet_Core_Prep_Samples.Entities;
using Microsoft.EntityFrameworkCore;
namespace DotNet_Core_Prep_Samples.DataAccess
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //DbSets
        DbSet<Employee> employees { get; set; }
        DbSet<Department> department { get; set; }
    }
}
