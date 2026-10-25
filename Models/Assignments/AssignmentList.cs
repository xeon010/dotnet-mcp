using System.Text.Json.Serialization;
using Mcp.Models.Assignments;

public record ApiResponse(
    [property: JsonPropertyName("data")] List<Assignment> Data
);