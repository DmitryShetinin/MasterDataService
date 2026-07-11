// Application/Services/EquipmentService.cs
using AutoMapper;
using Domain.Entities;

public class EquipmentService : IEquipmentService
{
    private readonly IRepository<Equipment> _equipmentRepo;
    private readonly IRepository<Plant> _plantRepo; // чтобы проверить существование Plant
    private readonly IMapper _mapper;

    public EquipmentService(
        IRepository<Equipment> equipmentRepo,
        IRepository<Plant> plantRepo,
        IMapper mapper)
    {
        _equipmentRepo = equipmentRepo;
        _plantRepo = plantRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EquipmentResponseDto>> GetAllAsync()
    {
        var equipments = await _equipmentRepo.GetAllAsync();
        return _mapper.Map<IEnumerable<EquipmentResponseDto>>(equipments);
    }

    public async Task<EquipmentResponseDto?> GetByIdAsync(Guid id)
    {
        var equipment = await _equipmentRepo.GetByIdAsync(id);
        return equipment == null ? null : _mapper.Map<EquipmentResponseDto>(equipment);
    }

    public async Task<EquipmentResponseDto> CreateAsync(EquipmentCreateDto dto)
    {
        // Проверяем, что Plant с таким Id существует
        var plantExists = await _plantRepo.GetByIdAsync(dto.PlantId) != null;
        if (!plantExists)
            throw new ArgumentException($"Plant with id {dto.PlantId} not found.");

        var equipment = _mapper.Map<Equipment>(dto);
        equipment.Id = Guid.NewGuid();
        await _equipmentRepo.AddAsync(equipment);
        await _equipmentRepo.SaveChangesAsync();

        return _mapper.Map<EquipmentResponseDto>(equipment);
    }

    public async Task<EquipmentResponseDto?> UpdateAsync(Guid id, EquipmentUpdateDto dto)
    {
        var equipment = await _equipmentRepo.GetByIdAsync(id);
        if (equipment == null)
            return null;

        // Обновляем только Name (PlantId не меняем, можно разрешить отдельно)
        _mapper.Map(dto, equipment); // если AutoMapper настроен на обновление
        _equipmentRepo.Update(equipment);
        await _equipmentRepo.SaveChangesAsync();

        return _mapper.Map<EquipmentResponseDto>(equipment);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var equipment = await _equipmentRepo.GetByIdAsync(id);
        if (equipment == null)
            return false;

        _equipmentRepo.Delete(equipment);
        await _equipmentRepo.SaveChangesAsync();
        return true;
    }
}