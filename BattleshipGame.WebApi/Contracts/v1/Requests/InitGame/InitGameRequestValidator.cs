using BattleshipGame.Models.Entities;
using FluentValidation;

namespace BattleshipGame.WebApi.Contracts.v1.Requests.InitGame;

public class InitGameRequestValidator : AbstractValidator<InitGameRequest>
{
    public InitGameRequestValidator()
    {
        RuleFor(x => x.Player1)
            .NotEmpty()
            .MaximumLength(Player.MaxLengthOfPlayerName);
        
        RuleFor(x => x.Player2)
            .NotEmpty()
            .MaximumLength(Player.MaxLengthOfPlayerName);
    }
}