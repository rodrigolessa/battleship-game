using FluentValidation.Results;

namespace BattleshipGame.Infrastructure.Exceptions;

public class FrustratedCommandExecutionException : Exception
{
    private const string FrustratedCommandExecutionMessage = "Frustrated command execution";
    
    // TODO: Create a function to concatenate errors list into string 
    public FrustratedCommandExecutionException(IEnumerable<ValidationFailure> errors) : base(FrustratedCommandExecutionMessage)
    {
    }
}