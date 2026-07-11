 
public interface IEquipmentService
{
    Task<IEnumerable<EquipmentResponseDto>> GetAllAsync();
    Task<EquipmentResponseDto?> GetByIdAsync(Guid id);
    Task<EquipmentResponseDto> CreateAsync(EquipmentCreateDto dto);
    Task<EquipmentResponseDto?> UpdateAsync(Guid id, EquipmentUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}