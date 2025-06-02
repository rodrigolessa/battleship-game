using BattleshipGame.Infrastructure.Cqrs.Commands;
using BattleshipGame.Models;

namespace BattleshipGame.WebApi.Contracts.v1.Responses;

// TODO: Replace GUID to the specífic type of each property
public record CommandStatusResponse(string IdempotencyKey, GameId GameId);