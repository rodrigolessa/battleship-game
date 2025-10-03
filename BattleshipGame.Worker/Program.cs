using BattleshipGame.Infrastructure;
using BattleshipGame.Infrastructure.Abstractions;
using BattleshipGame.Infrastructure.IoC.Configurations;
using BattleshipGame.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IClock, MyClock>();

// TODO: Mapping commands to its handlers

// Message Broker
// Create all resources: exchange, queues and binds
builder.Services.SetMessageBrokerSettings(builder.Configuration);
builder.Services.CreateAllNecessaryRabbitMqInfrastructure(builder.Configuration);

//builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();