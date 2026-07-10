using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDataService.Infrastructure.Configurations
{
    public class MeasurementUnitConfiguration : IEntityTypeConfiguration<MeasurementUnit>
    {
        public void Configure(EntityTypeBuilder<MeasurementUnit> builder)
        {
            builder.ToTable("MeasurementUnits");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(m => m.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Symbol)
                .HasColumnName("Symbol")
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}