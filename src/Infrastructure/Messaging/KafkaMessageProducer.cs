using Application.Interfaces;
using Confluent.Kafka;

 

namespace Infrastructure.Messaging
{
    public class KafkaMessageProducer : IMessageProducer
    {
        private readonly IProducer<string, string> _producer;
        public KafkaMessageProducer(ProducerConfig config)
        {

            Console.WriteLine("KafkaMessageProducer constructor started");
            _producer = new ProducerBuilder<string, string>(config).Build();
            Console.WriteLine($"KafkaMessageProducer constructed {config}");
        }

        public async Task ProduceAsync(string topic, string key, 
            string message, CancellationToken cancellationToken)
        {
            await _producer.ProduceAsync(topic, 
                                         new Message<string, string> { Key = key, Value = message}, 
                                         cancellationToken); 
        }
    }
}
