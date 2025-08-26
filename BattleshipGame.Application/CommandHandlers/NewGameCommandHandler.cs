using BattleshipGame.Application.Commands.NewGame;
using MediatR;

namespace BattleshipGame.Application.CommandHandlers;

public sealed class NewGameCommandHandler : IRequestHandler<NewGameCommand>
{
    public Task Handle(NewGameCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}