
namespace Domain.Entities;


public class Formula
{
    public Guid Id { get; set; }

    public string Expression { get; private set; } = default!;

    public int Version { get; set; }

    public bool IsCurrent { get; set; }
    public ICollection<Tag> Tags { get; set; } = [];

}