using System.Text.Json.Serialization;

namespace Mcp.Models.Assignments;
public record Assignment(
    [property: JsonPropertyName("id")] int Id, 
    [property: JsonPropertyName("description")] string Description, 
    [property: JsonPropertyName("points_possible")] double PointsPossible,
    [property: JsonPropertyName("name")] string Name
);