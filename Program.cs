using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using Microsoft.Extensions.Http;

var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
{
    Args = args,
    ContentRootPath = AppContext.BaseDirectory
});

// Configure all logs to go to stderr (stdout is used for the MCP protocol messages).
builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

builder.Services.AddHttpClient<ICanvasService, CanvasService>(client =>
{
    client.BaseAddress = new Uri("https://wsu.instructure.com/api/v1/");
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", builder.Configuration["Canvas:Token"]);
    client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-mcp/1.0");
});

var token = builder.Configuration["Canvas:Token"]
    ?? throw new InvalidOperationException("Canvas:Token missing from appsettings.json");

// Add the MCP services: the transport to use (stdio) and the tools to register.
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<CanvasMcpTools>();

await builder.Build().RunAsync();