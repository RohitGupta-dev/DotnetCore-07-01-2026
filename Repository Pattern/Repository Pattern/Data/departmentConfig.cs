using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository_Pattern.Models;

namespace Repository_Pattern.Data
{
    internal class departmentConfig : IEntityTypeConfiguration<Department>
    {

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department");
            builder.HasKey(x=>x.id);

            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.departmanetName).IsRequired();
            builder.Property(x => x.departmanetName).HasMaxLength(250);

            builder.HasData(new List<Department>()
            {
                new Department
                {
                    id=1,
                    departmanetName="ESC",
                    departmaneDesc="Esc Departmetn"

                },
                 new Department
                {
                    id=2,
                    departmanetName="CSE",
                    departmaneDesc="CSE Departmetn"
                }
            });
        }
    }
}