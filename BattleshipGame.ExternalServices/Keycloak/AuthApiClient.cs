using BattleshipGame.Infrastructure.Authentication.Abstractions;
using BattleshipGame.Infrastructure.Authentication.Responses;

namespace BattleshipGame.ExternalServices.Keycloak;

public class AuthApiClient : IAuthApiClient
{
    public Task<AuthApiResponse> GetAccessToken()
    {
        throw new NotImplementedException();
    }
}