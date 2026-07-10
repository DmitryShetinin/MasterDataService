namespace Domain.Entities;


public class MeasurementUnit
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Symbol { get; set; } = default!;

    public ICollection<Tag> Tags { get; set; } = [];

}