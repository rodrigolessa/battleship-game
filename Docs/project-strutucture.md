# How this projects is organized

## Solution Folders

0 - Arquitetura e Design

0.1 - Arquivos de Projeto
0.1.1 - Version

1 - Dependências Externas

1.1 - Databases - Scripts gerados ou versionados (EF ou Flyway)
1.1.1 - EventStore
1.1.2 - ReadModel
1.1.3 - Seed - Scripts de carga inicial (usuários, dados de domínio)

1.2 - Deploy
1.2.1 - Docker - Dockerfiles, docker-compose.yml
1.2.2 - Terraform - IaC (Terraform, scripts de infra)
1.2.3 - K8s - Arquivos YAML do Kubernetes

1.3 - Github
1.3.1 - Workflows - Pipelines, workflows


2 - Camadas - Código-fonte da aplicação

2.1 - Apresentação

2.2 - Serviços Distribuidos

- BattleshipGame.WebApi
- BattleshipGame.Worker

2.3 - Aplicação - Casos de uso (CQRS), DTOs, services, validações

- BattleshipGame.Application

2.4 - Domínio - Entidades, interfaces, enums, regras de negócio puras

- BattleshipGame

2.5 - Infraestrutura - EF Core, RabbitMQ, Serilog, HttpClients, bancos de dados

- BattleshipGame.Infrastructure
- BattleshipGame.Infrastructure.Persistence
- BattleshipGame.Infrastructure.ReadModel

- frm.Infrastructure.Persistence
- frm.Infrastructure.Messaging
- frm.Infrastructure.Cqrs
- frm.Infrastructure.EventSourcing 
- frm.Infrastructure.Messaging.RabbitMqPublisher

2.6 - Serviços Externos

- BattleshipGame.ExternalServices 

3 - Qualidade

3.1 - Testes Unitários - xUnit/Moq

- BattleshipGame.WebApi.UnitTests 
- BattleshipGame.Application.UnitTests
- BattleshipGame.Domain.UnitTests

3.2 - Testes de Carga
3.3 - Testes de Integração - banco real, containers, mock para dependências externas
3.4 - Testes de Contrato
3.5 - Testes Funcionais - Testes contra um ambiente real de Staging