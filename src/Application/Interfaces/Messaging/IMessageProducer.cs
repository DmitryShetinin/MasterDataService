namespace Application.Interfaces
{
    public interface IMessageProducer
    {
        Task ProduceAsync(string topic, string key, 
                          string message, CancellationToken cancellationToken);
    }
} 
