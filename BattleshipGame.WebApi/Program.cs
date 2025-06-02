using System.Reflection;
using BattleshipGame.WebApi.Contracts.v1.Requests.InitGame;
using BattleshipGame.WebApi.RequestProcessor;
using FluentValidation;
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

//builder.Services.AddSingleton<ICommandScheduler, CommandScheduler>();

builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssemblyContaining(Assembly.Get.GetExecutingAssembly());

// TODO: uncomment to validate game status
// builder.Services.AddHostedService<EndGameBackgroundService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Build minimal API for event retrieval

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();