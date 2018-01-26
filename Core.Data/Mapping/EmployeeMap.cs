using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Data
{
    public class EmployeeMap: IEntityTypeConfiguration<Employee>
    {

        public void Configure(EntityTypeBuilder<Employee> builder)
        {            
            builder.ToTable("Employee");

            builder.Property(c => c.Id)
                 .HasColumnName("Id")
                 .ValueGeneratedNever();

            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();


            builder.OwnsOne(c => c.Email, email =>
            {
                email.Property(c => c.Address)
                   .HasColumnType("varchar(150)")
                   .HasColumnName("Email")
                   .HasMaxLength(150);
            });

            builder.Property(c => c.Birth)
              .HasColumnType("Date");


            builder.Property(c => c.Genre)
             .HasColumnType("Bit");

            builder.Property(x => x.RoleId).IsRequired();


            builder.HasOne(p => p.Role)
               .WithMany(p => p.Employees)
               .HasForeignKey(p => p.RoleId);

        }

    }
}
