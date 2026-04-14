# Copilot Instructions

## Scope
- Applies to `Ground.Utilities.OpenTelemetryRegistration` (including `Ground.Utilities.OpenTelemetryRegistration.Sample`)
- Also applies to `Ground.Utilities.SerilogRegistration` (including `Ground.Utilities.SerilogRegistration.Sample`)

## Project Guidelines
- Provides plug-in observability for ASP.NET Core services via `AddGroundObservabilitySupport()` and `UseGroundObservabilityMiddlewares()`.
- Exports **traces** via OTLP (`.WithTracing().AddOtlpExporter(...)`) and **metrics** via Prometheus scraping (`.WithMetrics().AddPrometheusExporter()` + `/metrics` endpoint).
- Custom HTTP metrics are produced by `ResponseMetricMiddleware` using `MetricReporter` (request count + request duration tagged by `path`, `httpMethod`, `statusCode`); `/metrics` is excluded from instrumentation.

## Project Guidelines (`Ground.Utilities.SerilogRegistration`)
- Plug-in Serilog via `WebApplicationBuilder.AddGroundSerilog(...)`; configure sinks/levels via `.ReadFrom.Configuration(...)`.
- Keep enrichers cheap + safe; never throw from `ILogEventEnricher.Enrich()` (null-check `Assembly.GetEntryAssembly()`, HttpContext, claims).
- Prefer adding properties with `logEvent.AddPropertyIfAbsent(...)` and stable property names (`ApplicationName`, `ServiceName`, `ServiceVersion`, `ServiceId`, `UserId`, etc.).