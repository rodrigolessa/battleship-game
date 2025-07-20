using BattleshipGame.Application.Configurations;
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

// builder.Services.AddLoki();

// Authorization

//builder.Services.AddAuthorizationServices(Configuration);
//builder.Services.AddAuthenticationServices(Configuration);

// TODO: Encapsulate DI in extensions methods

//builder.Services.AddSingleton<ICommandScheduler, CommandScheduler>();

// Validators
builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssemblyContaining(Assembly.Get.GetExecutingAssembly());
builder.Services.AddQueryValidators();

// CQRS - Queries
builder.Services.AddQueryHandlers();

// Message Broker

// builder.Services.AddRabbitMQ(Configuration);

// Storage

// builder.Services.AddStorage(Configuration);

// TODO: Uncomment to validate game status
// builder.Services.AddHostedService<EndGameBackgroundService>();

// Cache Service

//builder.Services.AddRedis(Configuration);

// Serialization

// builder.Services.AddSerializerSchema();

// API Endpoints

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

// app.UseRouting();

app.UseAuthorization();

// app.ConfigureCors(Configuration);

app.MapControllers();

// app.ConfigureHealthCheckEndpoints();
// app.UseHealthChecks();

// app.UseDocumentation();

app.Run();