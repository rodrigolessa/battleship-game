using FluentValidation;
using FluentValidation.Results;

namespace BattleshipGame.Infrastructure.Exceptions;

[Serializable]
public class FrustratedQueryExecutionException : ValidationException
{
    private const string FrustratedQueryExecutionMessage = "Frustrated query execution";

    public FrustratedQueryExecutionException(string message) : base(message)
    {
    }

    public FrustratedQueryExecutionException(string message, IEnumerable<ValidationFailure> errors) : base(message,
        errors)
    {
    }

    public FrustratedQueryExecutionException(string message, IEnumerable<ValidationFailure> errors,
        bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
    {
    }

    public FrustratedQueryExecutionException(IEnumerable<ValidationFailure> errors) : base(
        FrustratedQueryExecutionMessage, errors)
    {
    }
}