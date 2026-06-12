# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository Overview

Ground is an onion-architecture framework for building microservices with DDD and CQRS, published as a set of NuGet packages (.NET 10, `net10.0`). It ships a custom command/query/event mediator (not MediatR), EF Core-based command/query data access, and pluggable extensions (caching, serialization, messaging, observability) wired through dependency injection.

## Solutions and Build

The repo contains **multiple independent solutions**:

- `Ground.sln` (root) — `src/`, `samples/`, `tests/`, plus the extension abstractions and the simpler extensions (Caching, DI, ObjectMappers, Serializers, Translations, UsersManagement) under a "Dependencies" solution folder.
- Self-contained solutions with their own sample apps, **not** in the root solution:
  - `Extensions/Events/Ground.Extensions.Events.Outbox/` and `.../Ground.Extensions.Events.PollingPublisher/`
  - `Extensions/MessageBus/...` (RabbitMQ, MessageInbox — each implementation lives under its own folder with a `.sln`)
  - `Utilities/Authentication/`, `Utilities/OpenTelemetryRegistration/`, `Utilities/SerilogRegistration/`
- All projects cross-reference via `ProjectReference` (extensions reference `Extensions/Ground.Extensions.Abstractions/*`), so building any one solution builds its dependencies.

```powershell
dotnet build Ground.sln                # main solution
dotnet test Ground.sln                 # all tests (xUnit + Moq + Shouldly)
dotnet test tests/1.Core/Ground.Core.Domain.Tests   # single test project
dotnet test --filter "FullyQualifiedName~EntityTests"  # single test class/method
dotnet build Extensions/MessageBus/Ground.Extensions.MessageBus.RabbitMQ/Ground.Extensions.MessageBus.RabbitMQ.sln  # an out-of-root solution
```

CI (`.github/workflows/dotnet.yml`) runs restore/build/test in Release on pushes and PRs to `main` and `release/*`. Publishing (`main.yml`) triggers on a GitHub release: it packs **every** `.sln` in the repo with the version taken from the release tag (leading `v` stripped) and pushes to NuGet — package metadata comes from `Directory.Build.props`.

Samples expect SQL Server at `Server=.` (and RabbitMQ at `localhost` for the message-bus sample); connection strings are hardcoded in the samples' `HostingExtensions.cs`/`Program.cs`.

## Architecture

Layers are numbered folders; references only point inward (`4.Endpoints` → `3.Infra` → `2.Core` → `1.Utilities`):

### `src/2.Core` — the heart of the framework

- **`Ground.Core.Domain`**: `Entity<TId>`/`AggregateRoot<TId>` (aggregates collect `IDomainEvent`s; supports event sourcing via apply/replay), `BaseValueObject`, `BusinessId`, `IAuditableEntity` (marker enabling audit shadow properties in Infra), domain exceptions (`DomainStateException`, `InvalidEntityStateException`, `InvalidValueObjectStateException`).
- **`Ground.Core.RequestResponse`**: message shapes — `ICommand`/`ICommand<TResult>`, `IQuery<TResult>`, `PageQuery`, and result envelopes `CommandResult`/`QueryResult` carrying `ApplicationServiceStatus` + messages.
- **`Ground.Core.Contracts`**: all interfaces — `ICommandHandler<,>`, `IQueryHandler<,>`, `IDomainEventHandler<>`, the dispatchers (`ICommandDispatcher`, `IQueryDispatcher`, `IEventDispatcher`), and data contracts (`ICommandRepository<,>`, `IQueryRepository`, `IUnitOfWork`, `IDomainEventStore`).
- **`Ground.Core.ApplicationServices`**: the custom mediator. `CommandDispatcher` is wrapped by decorators registered with Scrutor: `CommandDispatcherValidationDecorator` (FluentValidation, runs first) → `CommandDispatcherDomainExceptionHandlerDecorator` (translates domain exceptions into failed `CommandResult`s) → `CommandDispatcher`. Handlers inherit `CommandHandler`/`QueryHandler` base classes.
- **`Ground.Core.Domain.Toolkits`**: shared value objects (`Title`, `Description`, `Priority`).

### `src/3.Infra/Data` — CQRS persistence (EF Core / SQL Server)

- **`...Sql.Commands`** (folder is `Ground.Infra.Data.Sql.Command`, project/package is plural): `BaseCommandDbContext`, `BaseCommandRepository`, `BaseEntityFrameworkUnitOfWork`, `AddAuditDataInterceptor` (the recommended way to populate audit data), and `ModelBuilder` extensions: `AddAuditableShadowProperties()`, `AddRowVersionShadowProperty()`, `AddBusinessId()`, `UseValueConverterForType()`.
- **`...Sql.Queries`**: `BaseQueryDbContext`, `BaseQueryRepository` — read side, kept separate from the write side; consuming services define two DbContexts.

### `src/4.Endpoints/Ground.Endpoints.WebApi` — composition root

- `AddGroundApiCore(params string[] assemblyNamesForLoad)` = `AddControllers` (+ `TrackActionPerformanceFilter`) + FluentValidation + `AddGroundDependencies(...)`, which assembly-scans (Scrutor) and registers by convention: command/query/event handlers, repositories, unit-of-works, FluentValidation validators, and any class implementing a lifetime marker interface. Assembly names are **substring-matched** against loaded runtime libraries (e.g. `"Ground"` matches everything Ground-prefixed).
- `UseGroundApiExceptionHandler()` — global exception middleware producing problem-details-style errors.
- `BaseController` exposes the dispatchers and `Create`/`Edit`/`Delete`/`Query` helpers that map `ApplicationServiceStatus` to HTTP status codes — derive API controllers from it and pass commands/queries through these helpers rather than calling handlers directly.

### DI conventions (used everywhere)

- Anything implementing `ICommandHandler/IQueryHandler/IDomainEventHandler/ICommandRepository/IQueryRepository/IUnitOfWork` is auto-registered (transient) by the assembly scan — no manual registration in consuming services.
- For arbitrary services, implement a lifetime marker from `Ground.Extensions.DependencyInjection.Abstractions`: `ITransientLifetime`, `IScopeLifetime`, or `ISingletoneLifetime` (sic).
- Extensions follow the pattern: an `*.Abstractions` project defines the interface; concrete packages register implementations via `AddGroundXxx(...)` extension methods (e.g. `AddGroundNewtonSoftSerializer`, `AddGroundInMemoryCaching`, `AddGroundAutoMapperProfiles`, `AddGroundTraniTranslator`, `AddGroundWebUserInfoService`, `AddGroundRabbitMqMessageBus`, `AddGroundMessageInbox`/`AddGroundMessageInboxDalSql`, `AddGroundPollingPublisher`/`AddGroundPollingPublisherDalSql`). See `samples/3.Endpoints/Ground.Samples.Endpoints.WebApi/HostingExtensions.cs` for the canonical end-to-end wiring.

### Eventing / messaging (transactional messaging patterns)

- `Extensions/Events/Ground.Extensions.Events.Outbox` (EF DAL) + `Ground.Extensions.Events.PollingPublisher` (Dapper DAL) implement the transactional-outbox and polling-publisher patterns.
- `Extensions/MessageBus/...RabbitMQ` is the bus transport (consumers implement `IMessageConsumer`; subscribe at startup via `ReceiveEventFromRabbitMqMessageBus`/`ReceiveCommandFromRabbitMqMessageBus`); `...MessageInbox` provides idempotent consumption (inbox pattern, Dapper DAL).

### Observability utilities

- `AddGroundObservabilitySupport()` / `UseGroundObservabilityMiddlewares()` (OpenTelemetryRegistration): traces via OTLP exporter, metrics via Prometheus scraping at `/metrics` (excluded from instrumentation); custom HTTP metrics come from `ResponseMetricMiddleware` + `MetricReporter` tagged by `path`/`httpMethod`/`statusCode`.
- `AddGroundSerilog(...)` (SerilogRegistration): sinks/levels configured via `ReadFrom.Configuration(...)`. Enrichers must be cheap and never throw from `Enrich()` (null-check `Assembly.GetEntryAssembly()`, HttpContext, claims); add properties with `AddPropertyIfAbsent(...)` using stable names (`ApplicationName`, `ServiceName`, `ServiceVersion`, `ServiceId`, `UserId`).
- `AddGroundApiAuthentication` (Authentication utility): JWT and reference-token support.

## Conventions and Gotchas

- **Misspellings are shipped public API** — do not "fix" them in passing, it's a breaking change for NuGet consumers: `ISingletoneLifetime`, `IScopeLifetime` (not `IScopedLifetime`), `AddCustomeDepenecies` (in `Ground.Extensions.DependencyInjection` — distinct from `AddCustomDependencies` in `Ground.Endpoints.WebApi`), `AssmblyNamesForLoadProfiles`, `AddJwtTokenSupoort`, `AddReferenceTokenSupoort`, namespace `Ground.Endpoints.WebApi.Extentions` (alongside the correctly-spelled `Extensions`).
- The framework is a fork/rebrand of "Zamin"; one leftover folder remains (`Extensions/MessageBus/Ground.Extensions.MessageBus.MessageInbox/Zamin.Extensions.MessageBus.MessageInbox.Sample`).
- Folder names and project names diverge in places (`Ground.Infra.Data.Sql.Command` folder → `Ground.Infra.Data.Sql.Commands` project; `samples/1.Core/Ground.Sample.Core.*` folders → `Ground.Samples.Core.*` projects) — trust the `.csproj` name.
- `Directory.Build.props` applies package metadata to every project; library projects set `IsPackable=true` and individual descriptions in their own `.csproj`.
- Current development branch is `release/10.x`; PRs usually target `main`.
