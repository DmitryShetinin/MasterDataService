 
namespace Domain.Entities;

public class Tag
{
    public Guid Id { get; set; }

    public Guid EquipmentId { get; set; }

    public string Code { get; set; } = default!;

    public Guid FormulaId { get; set; }

    public Guid MeasurementUnitId { get; set; }

    public Equipment Equipment { get; set; } = default!;

    public string Formula { get; set; } = default!;

    public MeasurementUnit Unit { get; set; } = default!;
}