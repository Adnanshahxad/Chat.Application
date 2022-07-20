# Chat Application Challenge

## Introduction

This application sample app which allow several users to talk in a chatroom and also to get stock quotes
from an API using a specific command.

## Tech Stack

 - Target Framework: .NET 6.0

## Dependencies

### Runtime

- SQL Server: [Container Image](https://hub.docker.com/_/microsoft-mssql-server)
- RabbitMQ: [Container Image](https://hub.docker.com/_/rabbitmq)

## Settings

The following settings can either be set in appsettings.json files in Chat.Application.Web.Service,Chat.Application.Client,Chat.Application.Bot.Service:

| Parameter | Example | Description |
|-----------|---------|-------------|
| `QueueConfig:HostName` | `localhost` | The DNS name of the RabbitMQ instance |
| `QueueConfig:UserName` | `guest` | Username for accessing RabbitMQ |
| `QueueConfig:Password` | `guest` | Password for accessing RabbitMQ |
| `QueueConfig:QueueName` | `Stock_queue` | The name of the queue to use for revceiving and sending messages|
| `ChatBotServiceUrl` | 'https://localhost:44302' | chat bot service url  |
| `ChatAppWebServiceUrl` | 'https://localhost:44304' | chat web service url  |
| `ConnectionStrings:Database` | `((localdb)\\MSSQLLocalDB ....)` | connection string for sql database |
| `CsvFilePath` | `AppData\\stock.csv` | path of stock file |

## Database Initialization
For initialization of sql database, we have to run migration first for that you have to select DAL.csproj in visual studio and run "Update-Database" from package manager 
or you can use "dotnet ef database update" in CLI.
reference:https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

## Startup Projects
#### Chat.Application.Web.Service
#### Chat.Application.Client
#### Chat.Application.Bot.Service

### Make sure rabbitmq is running and accepting connections on port 5672.
