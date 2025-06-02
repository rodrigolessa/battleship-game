namespace BattleshipGame.WebApi.BackgroundServices;

public class EndGameBackgroundService : BackgroundService // Events: https://www.youtube.com/watch?v=NmmpXcMxCjY
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("Game Status");
            await Task.Delay(1000, stoppingToken);
        }
    }
}