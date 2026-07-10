
namespace Domain.Entities;


public class Plant
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Code { get; set; } = default!;

    public ICollection<Equipment> Equipments { get; set; } = [];
}