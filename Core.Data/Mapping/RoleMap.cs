﻿using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Data.Mapping
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder.Property(c => c.Id)
                 .ValueGeneratedOnAdd();                 

            builder.Property(c => c.Name)
                .HasColumnType("varchar(60)")
                .HasMaxLength(100)
                .IsRequired();

        }

    }
}
