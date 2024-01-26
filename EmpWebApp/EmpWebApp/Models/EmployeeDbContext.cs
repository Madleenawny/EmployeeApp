using Microsoft.EntityFrameworkCore;

namespace EmpWebApp.Models
{
    public class EmployeeDbContext :DbContext
    { 
        public EmployeeDbContext() { }
        public EmployeeDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
