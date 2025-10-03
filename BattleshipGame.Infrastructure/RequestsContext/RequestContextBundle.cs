namespace BattleshipGame.Infrastructure.RequestsContext;

public class RequestContextBundle : IRequestContextBundle
{
    public string? ClientApplication { get; set; }
    public string? IpAddress { get; set; }

    public string? UserEmail { get; set; }

    public string? IdempotencyKey { get; set; }
    public string? CorrelationKey { get; set; }
    public string? SagaProcessKey { get; set; }
}