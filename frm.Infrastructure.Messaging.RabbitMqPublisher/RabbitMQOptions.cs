namespace frm.Infrastructure.Messaging.RabbitMqPublisher;

public class RabbitMQOptions
{
    public string HostName { get; set; }
    public int Port { get; set; } = 5672;
    public string UserName { get; set; }
    public string Password { get; set; }
}