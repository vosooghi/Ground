<h1 align="center">
  <br>
  <a href="https://github.com/vosooghi/Ground"><img src="https://raw.githubusercontent.com/vosooghi/Ground/main/src/2.Core/Ground.Core.Domain/Icon.png" alt="Markdownify" width="200"></a>
  <br>
  Ground Framework
  <br>
</h1>

<h4 align="center">a framework based on onion architecture to implement software projects regarding microservice architecture and DDD</h4>

<p align="center">
  <a href="https://www.nuget.org/packages/Ground.Solution.Templates">
    <img src="https://img.shields.io/static/v1?label=nuget&message=v2.0.0&color=green&link=https://www.nuget.org/packages/Ground.Solution.Templates" alt="Ground">
  </a>
</p>

<p align="center">
  <a href="#Onion Structure">Framework Structure</a> •
  <a href="#how-to-use">How To Use</a> •
  <a href="#license">License</a>
</p>

## Onion Structure
This framework is implemented by considering the following approaches:
*   Domain-Driven Design
*   Separation of Concern
*   Layer Architecture
*   Dependency Injection
*   You can use each layer separately in your projects.
* <b>src</b>
  1. Utilities
     * <a href="https://www.nuget.org/packages/Ground.Utilities">Ground.Utilities</a>
       - DateTime converters, Helper classes, Extensions (String, Guid, Linq, ...). 
  2. <b>Core</b>
    * <a href="https://www.nuget.org/packages/Ground.Core.Domain">Ground.Core.Domain</a>
       </br>This layer is an abstract of the real world: core business logic, aggregate roots, entities, value objects, and application business rules without any dependency on technology.
       </br>This  is layer is implemented according to the Domain-Driven Design <a href="https://github.com/ddd-crew/ddd-starter-modelling-process">DDD Crew</a>
       </br><b>Entities</b>
        * AggregateRoot: <a href="https://martinfowler.com/bliki/DDD_Aggregate.html">More Info</a>
          - Entity: Base Entity Class
          - IAuditableEntity: a markup interface to make an entity auditable (used in the Infra layer)
        * Events
        * Value Objects
        * Exceptions
    *  <a href="https://www.nuget.org/packages/Ground.Core.DomainToolkit">Ground.Core.DomainToolkit</a>
       <p>Shared Value Objects</p>
    *  <a href="https://www.nuget.org/packages/Ground.Core.RequestResponse">Ground.Core.RequestResponse</a>
         <p>Templates of messages between layers</p>
    *  <a href="https://www.nuget.org/packages/Ground.Core.Contracts">Ground.Core.Contracts</a>
         <p>The contracts of infrastructures, command and query handlers, and repositories. You can implement each contract using your specified technology and inject it as an extension.</p>         
    *  <a href="https://www.nuget.org/packages/Ground.Core.ApplicationServices">Ground.Core.ApplicationServices</a>
         <p>Implements use cases and orchestrates the interaction between the domain and infrastructure layers.</p>
  3. <b>Infra</b>
     <p>Handles external dependencies and technical details: Database, Serialization, API Caller, Security, ...</p>
    *  Data
         <p>The data infrastructure is implemented according to the CQRS</p>
    *    <a href="https://www.nuget.org/packages/Ground.Infra.Data.Sql">Ground.Infra.Data.Sql</a>
    *    <a href="https://www.nuget.org/packages/Ground.Infra.Data.Sql.Commands">Ground.Infra.Data.Sql.Commands</a>
           <p>Interceptors, Extensions (Audit, ShadowProperties), Value converters, Base CommandDBContext</p>
    *    <a href="https://www.nuget.org/packages/Ground.Infra.Data.Sql.Queries">Ground.Infra.Data.Sql.Queries</a>
  4. <b>Endpoint</b>
     <p>RESTful APIs</p>
* <b>tests</b>
* <b>sample</b>

## Extensions
* <a href="https://www.nuget.org/packages/Ground.Extensions.Abstractions">Ground.Extensions.Abstractions</a>
  * The abstract model of extensions. You can implement each extension and use it in the framework or utilize the following implemented extensions:
    * Ground.Extensions.DependencyInjection; automatically injects extensions to the DI container if your service is inherited from IScopelifetime, ISingletonLifetime, or ITransientLifetime.
    * <a href="https://www.nuget.org/packages/Ground.Extensions.Caching.InMemory">Ground.Extensions.Caching.InMemory</a>
    * Events (Transactional Messaging)
      * <a href="https://www.nuget.org/packages/Ground.Extensions.Events.Outbox">Ground.Extensions.Events.Outbox</a>, <a href="https://microservices.io/patterns/data/transactional-outbox.html">More Info</a>
      * <a href="https://www.nuget.org/packages/Ground.Extensions.Events.PollingPublisher">Ground.Extensions.Events.PollingPublisher</a>, <a href="https://microservices.io/patterns/data/polling-publisher.html">More Info</a>
    * Messaging, <a href="https://softwaremill.com/microservices-101/">More Info</a>
      * <a href="https://www.nuget.org/packages/Ground.Extensions.MessageBus.MessageInbox">Ground.Extensions.MessageBus.MessageInbox</a>
      * <a href="https://www.nuget.org/packages/Ground.Extensions.MessageBus.RabbitMQ">Ground.Extensions.MessageBus.RabbitMQ</a>
    * Serializers
      * <a href="https://www.nuget.org/packages/Ground.Extensions.Serializers.NewtonSoft">Ground.Extensions.Serializers.NewtonSoft</a>
    * Translations
      * <a href="https://www.nuget.org/packages/Ground.Extensions.Translations.Trani">Ground.Extensions.Translations.Trani</a>
    * UserManagement
      * <a href="https://www.nuget.org/packages/Ground.Extensions.UsersManagement">Ground.Extensions.UsersManagement</a>
    * ObjectMappers
      *  <a href="https://www.nuget.org/packages/Ground.Utilities.ObjectMappers.AutoMapper">Ground.Extensions.ObjectMappers.AutoMapper</a>

## Utilities
* Observability
  * <a href="https://www.nuget.org/packages/Ground.Utilities.OpenTelemetryRegistration">Ground.Utilities.OpenTelemetryRegistration</a>
    * Jaeger
  * <a href="https://www.nuget.org/packages/Ground.Utilities.SerilogRegistration">Ground.Utilities.SerilogRegistration</a>
    * ELK
    * SQL Server
    * File

## How To Use

To install Ground Template Project:
```bash
$ dotnet new install Ground.Solution.Templates
```
## License

MIT

---
