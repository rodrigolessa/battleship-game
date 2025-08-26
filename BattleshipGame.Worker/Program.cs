using BattleshipGame.Infrastructure;
using BattleshipGame.Infrastructure.Abstractions;
using BattleshipGame.Infrastructure.IoC.Configurations;
using BattleshipGame.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IClock, MyClock>();
builder.Services.AddHostedService<Worker>();

// Message Broker
// Create all resources: exchange, queues and binds
builder.Services.AddRabbitMq(builder.Configuration);

var host = builder.Build();
host.Run();