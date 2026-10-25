using Microsoft.Extensions.Configuration;
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

// User secrets only load automatically in Development; `dotnet run` defaults to Production.
builder.Configuration.AddUserSecrets<Program>(optional: true);

var token = builder.Configuration["Canvas:Token"];
if (string.IsNullOrWhiteSpace(token))
    throw new InvalidOperationException("Canvas:Token is missing. Set it with `dotnet user-secrets set Canvas:Token <token>`.");

builder.Services.AddHttpClient<ICanvasService, CanvasService>(client =>
{
    client.BaseAddress = new Uri("https://wsu.instructure.com/api/v1/");
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", token);
    client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-mcp/1.0");
});

// Add the MCP services: the transport to use (stdio) and the tools to register.
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<CanvasMcpTools>();
 
await builder.Build().RunAsync();
