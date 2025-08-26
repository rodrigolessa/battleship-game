namespace BattleshipGame.Application.Configurations;

public class MessageBrokerConstants
{
    public const string Exchange = "battleship.game";
    public const string NewGameRoute = "battle.new";
    public const string ShootingRoute = "battle.shoot";
    public const string EventRoute = "";
}