// Infrastructure/Persistence/AppDbContext.cs




using Domain.Entities;
using MasterDataService.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Outbox;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Equipment> Equipments => Set<Equipment>();

    public DbSet<MeasurementUnit> MeasurementUnits => Set<MeasurementUnit>();
    public DbSet<Plant> Plants => Set<Plant>();
    public DbSet<Tag> Tags => Set<Tag>();


    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new MeasurementUnitConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentConfiguration());

        modelBuilder.ApplyConfiguration(new PlantConfiguration());

    }
}
