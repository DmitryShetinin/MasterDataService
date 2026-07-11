using AutoMapper;
using Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Equipment, EquipmentResponseDto>();
        CreateMap<EquipmentCreateDto, Equipment>();
        CreateMap<EquipmentUpdateDto, Equipment>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}