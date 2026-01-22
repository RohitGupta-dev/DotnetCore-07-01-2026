using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LearningDotnet.Data.config
{
    public class studentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");
            builder.HasKey(x=>x.Id);

            builder.Property(x=>x.Id).UseIdentityColumn();
            builder.Property(n => n.StudentName).IsRequired();
            builder.Property(n => n.StudentName).HasMaxLength(250);
            builder.Property(n => n.Address).IsRequired(false);
            builder.Property(n => n.Address).HasMaxLength(500);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);

            //add data 
            builder.HasData(new List<Student>()
               {
                    new Student
                    {
                        Id=1,
                        StudentName="Rohit",
                        Email="rohit@gmail.com",
                        Address="Rajpura"
                    },new Student
                    {
                        Id=2,
                        StudentName="Mohit",
                        Email="Mohit@gmail.com",
                        Address="Ambala"
                    },new Student
                    {
                        Id=3,
                        StudentName="Rajat",
                        Email="Rajat@gmail.com",
                        Address="Bilaspur"
                    }
               });

        }
    }
}
