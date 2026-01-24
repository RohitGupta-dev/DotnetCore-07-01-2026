using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository_Pattern.Models;

namespace Repository_Pattern.Data
{
    public class studentConfig : IEntityTypeConfiguration<student>
    {
        public void Configure(EntityTypeBuilder<student> builder)
        {
            builder.ToTable("student");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(n => n.Name).IsRequired();
            builder.Property(n => n.Email).HasMaxLength(250);
            builder.Property(n => n.Address).IsRequired(false);
            builder.Property(n => n.Phone).IsRequired(false);
            builder.Property(n => n.Address).HasMaxLength(500);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);

            //add data 
            builder.HasData(new List<student>()
               {
                    new student
                    {
                        Id=1,
                        Name="Rohit",
                        Email="rohit@gmail.com",
                        Address="43243243432",
                        Phone="4324324234"
                    },new student
                    {
                        Id=2,
                        Name="Mohit",
                        Email="Mohit@gmail.com",
                       Address ="Ambala",
                       Phone="23424324324"
                    },new student
                    {
                        Id=3,
                        Name="Rajat",
                        Email="Rajat@gmail.com",
                        Address="Bilaspur",
                        Phone="345345345"
                    }
               });

            builder.HasOne(n => n.Department).
            WithMany(n => n.students)
            .HasForeignKey(n => n.DepratmentId)
            .HasConstraintName("Fk_student_Department");

        }
    }
}
