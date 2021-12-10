# Datadog logging demo project

## List of code that needs to be added

This is a list of the code that needs to be changed from a standard .Net core 5 C# Web API project, in order to:

- Use Serilog
- Enhance logs with static metadata enrichment (i.e. static metadata - service, environment, version etc..)
- Enhance logs rith dynamic metadata - specifically the CorrelationId
- Send the logs to a Datadog agent or direct to the datadog server
- Work in a docker / kubernetes environment
- Enable the .Net tracer to send traces to Datadog

### For writing direct to the Datadog server

- Install the following Nugets:
  - Serilog.AspNetCore
  - Serilog.Sinks.File
  - Serilog.Sinks.Datadog.Logs  (only for sending logs direct to the Datadog server)
  - Serilog.Extensions.Logging  (only for using the Microsoft ILogger interface with Serilog)
  - Serilog.Enrichers.CorrelationId  (for enriching the logs with CorrelationId)

- *Program.cs* methods:
  - *Main* to create the static Serilog log with required options (remove the WriteTo.Console option)
  - *CreateHostBuilder* to add the UseSerilog option 

- *Startup.cs*
  - *ConfigureServices* to add the dependency for IHttpContextAccessor (required for CorrelationId)

- Configure the .Net tracer to send traces to Datadog. This setup is all in the Dockerfile - the tracer is installed and environment variables are set to enable it.

- Configure this docker with environment variables to allow the logs to be enhanced - see *docker-compose.yaml*
   The required environment variables are DD_SERVICE, DD_VERSION, DD_ENV, DD_TRACE_ROUTE_TEMPLATE_RESOURCE_NAMES_ENABLED, DD_RUNTIME_METRICS_ENABLED

For testing this option, you can use the default Dockerfile directly, as there is no requirement for a Datadog agent docker instance.

### For automatic collection of logs by the Datadog agent

1. Perform the above steps for writing direct to the datadog server (removing the WriteTo.DatadogLogs option)
2. Ensure the Datadog agent docker is configured with the correct environment variables and volumes - see *docker-compose.yaml*

For testing this option, use the docker-compose file to create 2 related docker instances; Dockerfile and DatadogAgent/Dockerfile - one for the API and one for the Datadog agent.

## Structured logging example

In WeatherForecastController.Get there are some example log statements that use structured logging - i.e. logging actual objects tather than strings.
When viewed in Datadog, they can be viewed as data objects and search conditions can be set on specific values.