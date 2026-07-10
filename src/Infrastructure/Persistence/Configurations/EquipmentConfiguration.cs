using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDataService.Infrastructure.Configurations
{
    public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            // 1. Таблица
            builder.ToTable("Equipments");

            // 2. Первичный ключ
            builder.HasKey(e => e.Id);

            // 3. Свойства
            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.PlantId)
                .HasColumnName("PlantId")
                .IsRequired();

            // 4. Связь с Plant (Многие к одному)
            builder.HasOne(e => e.Plant)
                .WithMany(p => p.Equipments) // если в Plant есть ICollection<Equipment>
                .HasForeignKey(e => e.PlantId)
                .OnDelete(DeleteBehavior.Restrict);

            // 5. Связь с Tags (Один ко многим)
            builder.HasMany(e => e.Tags)
                .WithOne(t => t.Equipment)
                .HasForeignKey(t => t.EquipmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // 6. Индекс для быстрого поиска по PlantId
            builder.HasIndex(e => e.PlantId)
                .HasDatabaseName("IX_Equipments_PlantId");

            // 7. Уникальный индекс (чтобы не было дублей Name в рамках одного Plant)
            builder.HasIndex(e => new { e.PlantId, e.Name })
                .IsUnique()
                .HasDatabaseName("IX_Equipments_PlantId_Name_Unique");
        }
    }
}