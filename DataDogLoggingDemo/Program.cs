using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using Serilog.Sinks.Datadog.Logs;
using System;

namespace DataDogLoggingDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                // ** Only one of the following WriteTo.Console and WriteTo.DatadogLogs should be used **
                //  (else logs may be duplicated in Datadog)
                // Required for automatic collection of logs by the agent.
                .WriteTo.Console(formatter: new JsonFormatter(renderMessage: true))
                // Required for direct transmission of logs to Datadogs server.
                .WriteTo.DatadogLogs(
                    // Replace with relevant Datadog account API ID.
                    // (if this is invalid, the application still works, but nothing gets sent to Datadog).
                    "insert api key here",
                    configuration: new DatadogConfiguration() { Url = "https://http-intake.logs.datadoghq.eu" },
                    service: "datadogloggingdemo",
                    host: Environment.MachineName,
                    source: "datadogloggingdemo")
                // Not essential - useful for local debugging
                .WriteTo.File(new JsonFormatter(renderMessage: true), "log.json")
                .CreateLogger();
            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
