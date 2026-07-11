

using Core.Events;
using MediatR;


public record CreateEquipmentEvent(Guid PlantId) : INotification;
public record GetEquipmentEvent(Guid Id, Guid PlantId, string Name) : INotification;

public record GetEquipmentByIdEvent(Guid Id, Guid PlantId, string Name) : INotification;

public record UpdateEquipmentEvent(Guid Id, Guid PlantId, string Name) : INotification;

public record DeleteEquipmentEvent(Guid Id, Guid PlantId, string Name) : INotification;

