using Domain.AuxModels;
using Domain.Enums;
using Domain.Interfaces.Services.Auxs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Services.Services.Auxs
{
    public class QueueManager : IQueueManager
    {
        private readonly ConnectionFactory _connectionFactory;

        public QueueManager(IOptions<RabbitMqSettings> rabbitmqSettings)
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = rabbitmqSettings.Value.Host,
                Port = rabbitmqSettings.Value.Port,
                UserName = rabbitmqSettings.Value.Username,
                Password = rabbitmqSettings.Value.Password
            };
        }

        public void SendMessage<T>(T content, QueueName qeueName)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: qeueName.ToString(), durable: true, exclusive: false, autoDelete: false, arguments: null);
            var serialized = JsonSerializer.Serialize(content);
            var body = Encoding.UTF8.GetBytes(serialized);

            channel.BasicPublish(exchange: "", routingKey: qeueName.ToString(), basicProperties: null, body: body);
        }
    }
}
