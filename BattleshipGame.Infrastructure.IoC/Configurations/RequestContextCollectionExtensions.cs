using BattleshipGame.Infrastructure.PipelineBehaviors;
using BattleshipGame.Infrastructure.RequestsContext;
using frm.Infrastructure.Cqrs.Requests;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BattleshipGame.Infrastructure.IoC.Configurations;

public static class RequestContextCollectionExtensions
{
    private const string CqrsNamespace = "frm.Infrastructure.Cqrs";

    public static void AddRequestContextHandlers(this IServiceCollection services)
    {
        services.AddScoped<RequestContextBundle>();
        services.AddScoped<IRequestContextBundle>(sp => sp.GetRequiredService<RequestContextBundle>());
        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestContextBehavior<,>));
    }
}