using System.Text.Json.Serialization;
using Mcp.Models;

public record ApiResponse(
    [property: JsonPropertyName("data")] List<Assignment> Data
);