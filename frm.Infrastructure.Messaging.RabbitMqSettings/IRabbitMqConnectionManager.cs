using RabbitMQ.Client;

namespace frm.Infrastructure.Messaging.RabbitMqSettings;

public interface IRabbitMqConnectionManager : IAsyncDisposable
{
    IConnection Connection { get; }
    IChannel Channel { get; }
    string Exchange { get; }
}