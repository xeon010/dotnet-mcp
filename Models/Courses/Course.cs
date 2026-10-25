using System.Text.Json.Serialization;

namespace Mcp.Models.Courses;
public record Course(
    [property: JsonPropertyName("id")] int Id, 
    [property: JsonPropertyName("name")] string Name, 
    [property: JsonPropertyName("term")] Term Term
);