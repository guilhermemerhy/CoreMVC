using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Data.Mapping
{
    public class DependentMap : IEntityTypeConfiguration<Dependent>
    {

        public void Configure(EntityTypeBuilder<Dependent> builder)
        {
            builder.ToTable("Dependent");

            builder.Property(c => c.Id)
                 .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

        }

    }
}
