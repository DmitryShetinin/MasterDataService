using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDataService.Infrastructure.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            // 1. Таблица
            builder.ToTable("Tags");

            // 2. Первичный ключ
            builder.HasKey(t => t.Id);

            // 3. Свойства
            builder.Property(t => t.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Code)
                .HasColumnName("Code")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Formula)
                .HasColumnName("Formula")
                .IsRequired()
                .HasMaxLength(1000); // Длинная строка для формулы

            // Внешние ключи
            builder.Property(t => t.EquipmentId)
                .HasColumnName("EquipmentId")
                .IsRequired();

            builder.Property(t => t.MeasurementUnitId)
                .HasColumnName("MeasurementUnitId")
                .IsRequired();

            // 4. Связи (Relationships)
            // Tag -> Equipment (Многие к одному)
            builder.HasOne(t => t.Equipment)
                .WithMany(e => e.Tags) // если в Equipment есть ICollection<Tag>
                .HasForeignKey(t => t.EquipmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tag -> MeasurementUnit (Многие к одному)
            builder.HasOne(t => t.Unit)
                .WithMany(u => u.Tags) // если в MeasurementUnit есть ICollection<Tag>
                .HasForeignKey(t => t.MeasurementUnitId)
                .OnDelete(DeleteBehavior.Restrict);

            // 5. Уникальный индекс (чтобы не было дублей по Equipment + MeasurementUnit + Code)
            builder.HasIndex(t => new { t.EquipmentId, t.MeasurementUnitId, t.Code })
                .IsUnique()
                .HasDatabaseName("IX_Tags_Unique_Combination");

            // 6. Индекс для быстрого поиска по Code
            builder.HasIndex(t => t.Code)
                .HasDatabaseName("IX_Tags_Code");
        }
    }
}