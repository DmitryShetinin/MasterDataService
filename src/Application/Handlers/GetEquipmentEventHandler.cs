using MediatR;
using Core.Events;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Application.Interfaces;

public class GetEquipmentEventHandler : INotificationHandler<GetEquipmentEvent>
{
    private readonly ILogger<GetEquipmentEventHandler> _logger;
    private const string Topic = "equipment-events";
    private readonly IMessageProducer _producer;
    public GetEquipmentEventHandler(ILogger<GetEquipmentEventHandler> logger, IMessageProducer producer)
    {
        _logger = logger;
        _producer = producer;
    }

    public async Task Handle(GetEquipmentEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            // Сериализуем событие в JSON
            var messageJson = JsonSerializer.Serialize(new
            {
                EventType = nameof(GetEquipmentEvent),
                notification.Id,
                notification.PlantId,
                notification.Name,
                Timestamp = DateTime.UtcNow
            });

            // Отправляем в Kafka. В качестве ключа можно использовать PlantId (чтобы шардировать по заводам)
            await _producer.ProduceAsync(
                topic: Topic,
                key: notification.PlantId.ToString(),
                message: messageJson,
                cancellationToken: cancellationToken
            );

            _logger.LogInformation("Kafka event sent for Equipment {EquipmentId}", notification.PlantId);
        }
        catch (Exception ex)
        {
            // Логируем ошибку, но не пробрасываем дальше, чтобы не уронить основной поток
            _logger.LogError(ex, "Failed to send Kafka event for Equipment {EquipmentId}", notification.PlantId);
        }
    }
}