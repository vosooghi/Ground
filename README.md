<h1 align="center">
  <br>
  <a href="https://github.com/vosooghi/Ground"><img src="https://raw.githubusercontent.com/vosooghi/Ground/main/src/2.Core/Ground.Core.Domain/Icon.png" alt="Markdownify" width="200"></a>
  <br>
  Ground Framework
  <br>
</h1>

<h4 align="center">a framework based on onion architecture to implement software projects regarding microservice architecture</h4>

<p align="center">
  <a href="https://www.nuget.org/packages/Ground.Solution.Templates">
    <img src="https://img.shields.io/static/v1?label=nuget&message=v2.0.0&color=green&link=https://www.nuget.org/packages/Ground.Solution.Templates" alt="Ground">
  </a>
</p>

<p align="center">
  <a href="#Structure">Framework Structure</a> •
  <a href="#how-to-use">How To Use</a> •
  <a href="#license">License</a>
</p>

## Structure

* Onion Structure
  - Sepration of Concern
  - Inner layers do not know the outer layers. 
* Domain Layer
  - This layer is an abstract of the real world: core business logic, aggregate roots, entities, value objects, and application business rules without any dependency on technology.
  - This layer is implemented according to the Domain-Driven Design <a href="https://github.com/ddd-crew/ddd-starter-modelling-process">DDD Crew</a>
* Contract Layer
  - Application service layer and Data layer contracts: Query and Command handlers and DTOs.
  - Repository definition
  - UoW definition
* Application Service
  - Implements use cases and orchestrates the interaction between the domain and infrastructure layers.
* Request Response Layer
  - Command/Query request and response abstraction and implementation
* Infrastructure Layer
  - Handles external dependencies and technical details: Database, Serialization, API Caller, Security, Notification
* Endpoint Layer
  - Web API
  - Web UI

## How To Use

To install Ground Template Project:
```bash
$ dotnet new install Ground.Solution.Templates
```
## License

MIT

---
