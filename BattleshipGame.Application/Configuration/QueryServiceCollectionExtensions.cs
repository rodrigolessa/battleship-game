using System.Diagnostics.CodeAnalysis;
using BattleshipGame.Application.Queries.SearchVessels;
using BattleshipGame.Infrastructure.PipelineBehaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BattleshipGame.Application.Configuration;

[ExcludeFromCodeCoverage]
public static class QueryServiceCollectionExtensions
{
    private const string SubNamespace = "BattleshipGame.Application.Queries";

    public static void AddQueryValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<SearchVesselsQuery>(filter: f => f.ValidatorType.Namespace.StartsWith(SubNamespace));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
    }

    public static void AddQueryHandlers(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.TypeEvaluator = (x) => x.Namespace?.StartsWith(SubNamespace) == true;
            cfg.RegisterServicesFromAssembly(typeof(SearchVesselsQuery).Assembly);
        });
    }
}