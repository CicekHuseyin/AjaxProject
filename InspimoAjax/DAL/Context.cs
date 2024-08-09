using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace InspimoAjax.DAL
{
    public class Context : DbContext
    {
        private readonly IConfiguration _configuration;
        public Context(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
