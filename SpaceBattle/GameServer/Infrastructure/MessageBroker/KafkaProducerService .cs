using Confluent.Kafka;
using GameServer.Infrastructure.MessageBroker.Configurations;
using GameServer.Infrastructure.MessageBroker.Enums;
using GameServer.Infrastructure.MessageBroker.Models;
using LZ4;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace GameServer.Infrastructure.MessageBroker
{
    public class KafkaProducerService : IHostedService, IDisposable
    {
        private IProducer<string, string> _producer;
        private readonly KafkaConfiguration _kafkaConfiguration;

        public KafkaProducerService(IOptions<KafkaConfiguration> kafkaConfigurationOptions)
        {
            _kafkaConfiguration = kafkaConfigurationOptions?.Value ?? throw new ArgumentException(nameof(kafkaConfigurationOptions));

            Init();
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await Produce(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _producer.Flush(cancellationToken);

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _producer.Dispose();
        }

        private void Init()
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = _kafkaConfiguration.Brokers,
                ClientId = "Kafka.Dotnet.Sample",
                SecurityProtocol = SecurityProtocol.Plaintext,
                EnableDeliveryReports = false,
                QueueBufferingMaxMessages = 10000000,
                QueueBufferingMaxKbytes = 100000000,
                BatchNumMessages = 500,
                Acks = Acks.None,
                DeliveryReportFields = "none"
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        private async Task Produce(CancellationToken cancellationToken)
        {
            try
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    var messageData = "{x: 1, y: 0}";
                    var sampleMessage = new GameMessage("Game1", new Guid(), OperationType.MoveAction, JsonSerializer.Deserialize<JsonObject>(messageData));
                    var json = sampleMessage.ToString();

                    var msg = new Message<string, string>
                    {
                        Key = _kafkaConfiguration.Key,
                        Value = Convert.ToBase64String(LZ4Codec.Wrap(Encoding.UTF8.GetBytes(json)))
                    };

                    await _producer.ProduceAsync(_kafkaConfiguration.Topic, msg, cancellationToken).ConfigureAwait(false);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
