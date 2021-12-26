using Microsoft.Extensions.Options;
using Products.Api.Configuration;
using Products.Api.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace Products.Api.Notifications
{
    public class Notifier : INotifier
    {
        protected IOptions<BusConfiguration> Configuration { get; set; }

        public Notifier(IOptions<BusConfiguration> configuration)
        {
            Configuration = configuration;
        }

        public void Notify(ProductChangeEvent productChangeEvent)
        {
            ConnectionFactory factory = new ConnectionFactory() {
                HostName = Configuration.Value.HostName,
                UserName = Configuration.Value.UserName,
                Password = Configuration.Value.Password,
                Port = Configuration.Value.Port,
                VirtualHost = Configuration.Value.VirtualHost,
            };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: Configuration.Value.QueueName,
                        exclusive: false,
                        autoDelete: false
                    );

                    string eventToDispatch = JsonSerializer.Serialize(productChangeEvent);
                    byte[] body = Encoding.UTF8.GetBytes(eventToDispatch);

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "", routingKey: Configuration.Value.QueueName, body: body);
                    channel.BasicAcks += AcknowledgementRecieved;
                }
            }
        }

        private void AcknowledgementRecieved(object sender, BasicAckEventArgs e)
        {
            Console.WriteLine("RabbitMQ received the message");
        }
    }
}
