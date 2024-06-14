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

* <b>src</b>
  1. Utilities
     * Ground.Utilities
       - DateTime converters, Helper classes, Extensions (String, Guid, Linq, ...)
  2. <b>Core</b>
    * Ground.Core.Domain
       </br>This layer is an abstract of the real world: core business logic, aggregate roots, entities, value objects, and application business rules without any dependency on technology.
       </br>This  is layer is implemented according to the Domain-Driven Design <a href="https://github.com/ddd-crew/ddd-starter-modelling-process">DDD Crew</a>
       </br><b>Entities</b>
        * AggregateRoot: <a href="">More Info</a>
          - Entity: Base Entity Class
          - IAuditableEntity: a markup interface to make an entity auditable (used in the Infra layer)
        * Events
        * Value Objects
        * Exceptions
    *  Ground.Core.DomainToolkit
       <p>Shared Value Objects</p>
    *  Ground.Core.RequestResponse
         <p>Templates of messages between layers</p>
    *  Ground.Core.Contracts
         <p>The contracts of infrastructures, command and query handlers, and repositories. You can implement each contract using your specified technology and inject it as an extension.</p>         
    *  Ground.Core.ApplicationServices
         <p>Implements use cases and orchestrates the interaction between the domain and infrastructure layers.</p>
  3. <b>Infra</b>
     <p>Handles external dependencies and technical details: Database, Serialization, API Caller, Security, ...</p>
    *  Data
         <p>The data infrastructure is implemented according to the CQRS</p>
    *    Ground.Infra.Data.Sql
    *    Ground.Infra.Data.Sql.Commands
           <p>Interceptors, Extensions (Audit, ShadowProperties), Value converters, Base CommandDBContext</p>
    *    Ground.Infra.Data.Sql.Queries
  4. <b>Endpoint</b>
     <p>RESTful APIs</p>
* <b>tests</b>
* <b>sample</b>

## Extensions
* 
## How To Use

To install Ground Template Project:
```bash
$ dotnet new install Ground.Solution.Templates
```
## License

MIT

---
