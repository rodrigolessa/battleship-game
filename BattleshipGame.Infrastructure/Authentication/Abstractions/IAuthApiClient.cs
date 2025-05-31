using BattleshipGame.Infrastructure.Authentication.Responses;

namespace BattleshipGame.Infrastructure.Authentication.Abstractions;

public interface IAuthApiClient
{
    Task<AuthApiResponse> GetAccessToken();
}