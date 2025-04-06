using Confluent.Kafka;
using GameServer.Infrastructure.MessageBroker.Commands;
using GameServer.Infrastructure.MessageBroker.Configurations;
using GameServer.Infrastructure.MessageBroker.Models;
using LZ4;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace GameServer.Infrastructure.MessageBroker
{
    public class KafkaConsumerService : IHostedService, IDisposable
    {
        private readonly KafkaConfiguration _kafkaConfiguration;
        private IConsumer<string, string> _consumer;

        public KafkaConsumerService(IOptions<KafkaConfiguration> kafkaConfigurationOptions)
        {
            _kafkaConfiguration = kafkaConfigurationOptions?.Value ?? throw new ArgumentException(nameof(kafkaConfigurationOptions));

            Init();
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {

                    _consumer.Subscribe(new List<string>() { _kafkaConfiguration.Topic });

                    await Consume(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {

            _consumer.Close();

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _consumer.Dispose();
        }

        private void Init()
        {
            var config = new ConsumerConfig()
            {
                BootstrapServers = _kafkaConfiguration.Brokers,
                GroupId = _kafkaConfiguration.ConsumerGroup,
                SecurityProtocol = SecurityProtocol.Plaintext,
                EnableAutoCommit = false,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true
            };

            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }


        private async Task Consume(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(cancellationToken);

                    if (consumeResult?.Message == null) continue;

                    if (consumeResult.Topic.Equals(_kafkaConfiguration.Topic))
                    {
                        await Task.Run(() =>
                        {
                            var json = Encoding.UTF8.GetString(LZ4Codec.Unwrap(Convert.FromBase64String(consumeResult.Message.Value)));
                            var message = JsonSerializer.Deserialize<GameMessage>(json);
                            var cmd = new Interpret(message);
                            cmd.Execute();
                        }, cancellationToken).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}