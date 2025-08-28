using BattleshipGame.Infrastructure.IoC.Configurations;
using BattleshipGame.Infrastructure.RequestsContext;
using BattleshipGame.WebApi.Contracts.v1.Requests.InitGame;
using BattleshipGame.WebApi.RequestProcessor;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// enable api versioning and return the headers
// "api-supported-versions" and "api-deprecated-versions"
builder.Services.AddApiVersioning( options => options.ReportApiVersions = true );

// Add services to the container.
builder.Services.AddScoped<IRequestHandler<InitGameRequest, ObjectResult>, InitGameRequestHandler>();
builder.Services.AddScoped<IRequestProcessor, RequestProcessor>();
builder.Services.AddScoped<IMediator, Mediator>();

// Logging
//builder.Services.AddLoki();

// Authorization
//builder.Services.AddAuthorizationServices(Configuration);
//builder.Services.AddAuthenticationServices(Configuration);
//builder.Services.AddKeyCloak();

// TODO: Encapsulate DI in extensions methods

//builder.Services.AddSingleton<ICommandScheduler, CommandScheduler>();

// Validators
builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssemblyContaining(Assembly.Get.GetExecutingAssembly());
builder.Services.AddQueryValidators();

// CQRS - Queries
builder.Services.AddQueryHandlers();

// CQRS - Request Context Bundle
builder.Services.AddRequestContextHandlers();

// Message Broker Settings
// Create the exchange needed for API send the messages
builder.Services.SetMessageBrokerSettings(builder.Configuration);
builder.Services.ConnectOrCreateRabbitMqExchangeForPublishMessages(builder.Configuration);

// Persistent Storage for Event Sourcing and Read Model
// builder.Services.AddStorage(Configuration);

// TODO: Uncomment to validate game status
// builder.Services.AddHostedService<EndGameBackgroundService>();

// Caching layer to improve performance
// TODO: What strategy fit for our project? 
// TODO: Is it necessary implement a Bloom Filter Algorithm?
//builder.Services.AddRedis(Configuration);

// Serialization
// builder.Services.AddSerializerSchema();

// API Endpoints
// TODO: Implement a Leaky Bucket Algorithm and use for rate limiting
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDocumentation(Configuration, DocumentationConfiguration.OpenApiOptions);

// Health checks
//builder.Services.AddAllHealthChecks<ReadModelContext>(Configuration);

var app = builder.Build();

// Build minimal API for event retrieval

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseLogInterceptor();

// app.UseMiddleware<TimeElapsedHeaderMiddleware>();
app.UseMiddleware<RequestContextMiddleware>();

// app.UseRouting();

// Authorization and Authentication using Keycloak
app.UseAuthorization();

// app.ConfigureCors(Configuration);

app.MapControllers();

// app.ConfigureHealthCheckEndpoints();
// app.UseHealthChecks();

// app.UseDocumentation();

app.Run();