using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data
{
    public class EmployeeMap: IEntityTypeConfiguration<Employee>
    {

        public void Configure(EntityTypeBuilder<Employee> builder)
        {            
            builder.ToTable("Employee");

            builder.Property(c => c.Id)
                 .HasColumnName("Id");

            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Email)                
                .HasColumnType("varchar(150)")
                .HasMaxLength(150);


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
