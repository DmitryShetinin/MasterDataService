namespace Domain.Entities;


public class Equipment
{
    public Guid Id { get; set; }





   
    public Guid PlantId { get; set; }

    public string Name { get; set; } = default!;

    public Plant Plant { get; set; } = default!;

    public ICollection<Tag> Tags { get; set; } = [];
}