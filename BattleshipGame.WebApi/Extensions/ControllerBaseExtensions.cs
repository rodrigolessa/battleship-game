using Microsoft.AspNetCore.Mvc;

namespace BattleshipGame.WebApi.Extensions;

public static class ControllerBaseExtensions
{
    public static ObjectResult FeatureNotImplemented(this ControllerBase controller)
    {
        var validationDetails = new ValidationProblemDetails
        {
            Title = "Not yet implemented",
            Status = StatusCodes.Status501NotImplemented,
            Detail = "This feature is still under construction"
        };

        return new ObjectResult(validationDetails);
    }
}