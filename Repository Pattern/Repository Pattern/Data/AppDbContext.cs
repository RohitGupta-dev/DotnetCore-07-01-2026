using Microsoft.EntityFrameworkCore;
using Repository_Pattern.Models;
using System.Data;

namespace Repository_Pattern.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
                
        }
        public DbSet<Employee> employee { get; set; }
        public DbSet<student> students { get; set; }
        public DbSet<Department> departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new studentConfig());
            modelBuilder.ApplyConfiguration(new departmentConfig());
            
        }
    }
}
