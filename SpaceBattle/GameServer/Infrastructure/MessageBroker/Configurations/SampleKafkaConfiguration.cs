using Microsoft.Extensions.Options;

namespace GameServer.Infrastructure.MessageBroker.Configurations
{
    public class SampleKafkaConfiguration : IOptions<KafkaConfiguration>
    {
        public KafkaConfiguration Value => new KafkaConfiguration
        {
            Brokers = "GameBrokers",
            Topic = "GameObjectTopic",
            Key = "Testkey",
            ConsumerGroup = "Game1"
        };
    }
}
