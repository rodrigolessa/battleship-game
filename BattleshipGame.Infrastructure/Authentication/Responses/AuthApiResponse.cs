namespace BattleshipGame.Infrastructure.Authentication.Responses;

public sealed class AuthApiResponse
{
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
}