using LearningDotnet.Data.config;
using Microsoft.EntityFrameworkCore;

namespace LearningDotnet.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Student> students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //This thing has 2 ways 
            //1.this is 
            //2. use configclass of dbset and set the property view this in studentConfig.cs
            //if you have multiple table 
            //table 1
            modelBuilder.ApplyConfiguration(new studentConfig());
            //table 2
            //modelBuilder.ApplyConfiguration(new studentConfig1());
            //table 3
            //modelBuilder.ApplyConfiguration(new studentConfig3());


            //modelBuilder.Entity<Student>().HasData(
            //    new List<Student>()
            //    {
            //        new Student
            //        {
            //            Id=1,
            //            StudentName="Rohit",
            //            Email="rohit@gmail.com",
            //            Address="Rajpura"
            //        },new Student
            //        {
            //            Id=2,
            //            StudentName="Mohit",
            //            Email="Mohit@gmail.com",
            //            Address="Ambala"
            //        },new Student
            //        {
            //            Id=3,
            //            StudentName="Rajat",
            //            Email="Rajat@gmail.com",
            //            Address="Bilaspur"
            //        }
            //    }
            //    );


            //modelBuilder.Entity<Student>(entity =>
            //{
            //    //entity.Property(n=>n.StudentName).IsRequired();
            //    //entity.Property(n=>n.StudentName).HasMaxLength(250);
            //    //entity.Property(n=>n.Address).IsRequired(false);
            //    //entity.Property(n=>n.Address).HasMaxLength(500);
            //    //entity.Property(n=>n.Email).IsRequired().HasMaxLength(250);
            //});
        }
    }
}
