# Battleship Game Project

Battleship Game engine with Event Sourcing, CQRS and C#

## Dependencies

### Packages

- [StronglyTypedIds](https://github.com/andrewlock/StronglyTypedId) - The DDD-Style **strongly typed ids*** to avoid primitive obsession, [Andrew Lock](https://andrewlock.net/series/using-strongly-typed-entity-ids-to-avoid-primitive-obsession/);
- Sqids - Obfuscate Numbers;
- Mapperly - .NET source generator for generating object mappings;

### Servers

- Seq - is a centralized structured logging and observability server
- Redis
- RabbitMQ
- MS SQL Server

## Architecture

### Patterns

- [CQRS pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- Event Source
- Outbox Pattern
- [Transactional outbox] (https://microservices.io/patterns/data/transactional-outbox.html)
- Mediator (with MediatR)
  - Organize code around use cases with requests and handlers
  - Cross-cutting concerns with pipeline behaviors
  - Thin API endpoints (avoiding fat endpoints)
  - Design highly-testable handlers

### Most Common filters
Mechanism to intercept and modify the execution of a request pipeline;

- IAuthorizationFilter (Authorization - Run early to allow/deny access)
- IResourceFilter (Resource - Wrap around the entire request)
- IActionFilter (Action - Run before/after controller actions)
- IExceptionFilter (Exception - Handle exceptions globally)
- IResultFilter (Result - Before/after the result is executed)

Useful for concerns like:

- Logging
- Authorization
- Exception handling
- Validation
- Caching

## Message Brokers

### RabbitMQ (with plugins)

- For Scheduled Delivery (not native). Needs Delayed Message Plugin
- Ordered Queues / Sessions. FIFO within queue; limited session-like behavior via x-group-id or custom logic
- Simulate RabbitMQ behavior with https://tryrabbitmq.com/

#### Start RabbitMQ Locally

Use Docker:
´´´
docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=MyStrongP@ssword rabbitmq:3-management
´´´

#### Install RabbitMQ Client in .NET

Add the required package:
´´´
dotnet add package RabbitMQ.Client

´´´

#### NATS.Client.Core features

- Async/Await Support -> Full support (async-first design);
- Performance -> Faster, more optimized;
- Dependency Overhead -> Lightweight;
- Ease of Use -> Async-first API;

### Best Practices for Reducing Message Size

- Minimize Payload – Only send essential fields (command and executeAt);
- Use Binary Format – Instead of JSON, use **ProtoBuf* for smaller payloads;
- Dedicated Subjects – Use subjects like scheduled.jobs.destroygame for specific tasks to reduce unnecessary processing;

#### How is Protobuf Better?

- Smaller Size - Protobuf messages are compact and efficient;
- Faster Serialization - Binary format is much faster than JSON;
- Cross-Language Support - Works with other languages easily;

### Future implementation to improve message handler

- Implement retries and failure handling;
- Add logging and monitoring;
- Use JetStream if message persistence is needed;

## Structure

```mermaid flowchart
%% C4 Container Diagram
C4Container

title Distributed Architecture - API & Worker

Person(user, "Client Application", "Consumes the API")

System_Boundary(system, "Distributed System") {
    Container(api, "API", "ASP.NET Core", "Handles synchronous requests, queries SQL Database, and publishes messages to Service Bus queue.")
    Container(worker, "Worker", ".NET Worker Service", "Processes messages from Service Bus and publishes public events to another topic.")
    ContainerDb(db, "SQL Database", "Azure SQL", "Stores application data queried by the API.")
    ContainerQueue(queue, "Internal Queue", "Azure Service Bus Queue", "Receives messages from the API for asynchronous processing.")
    Container(topic, "Public Topic", "Azure Service Bus Topic", "Publishes public domain events for other services to consume.")
}

Rel(user, api, "Sends synchronous HTTP requests")
Rel(api, db, "Reads data from", "SQL Queries")
Rel(api, queue, "Publishes messages to", "Service Bus")
Rel(worker, queue, "Consumes messages from", "Service Bus")
Rel(worker, topic, "Publishes domain events", "Service Bus Topic")
```
