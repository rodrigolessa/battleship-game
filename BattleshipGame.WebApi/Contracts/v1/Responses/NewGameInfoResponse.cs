using BattleshipGame.Models;
using BattleshipGame.Models.Entities;

namespace BattleshipGame.WebApi.Contracts.v1.Responses;

public record NewGameInfoResponse(string IdempotencyKey, GameId GameId, PlayerId PlayerOneId, PlayerId PlayerTwoId)
    : CommandStatusResponse(IdempotencyKey, GameId);