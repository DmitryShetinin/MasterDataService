public class EquipmentResponseDto
{
    public Guid Id { get; set; }
    public Guid PlantId { get; set; }
    public string Name { get; set; } = default!;
    // Опционально: можно включить список Tags, но для простоты не будем
}