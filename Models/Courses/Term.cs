using System.Text.Json.Serialization;

namespace Mcp.Models.Courses;
public record Term(
    [property: JsonPropertyName("name")] string Name, 
    [property: JsonPropertyName("start_at")] string StartAt, 
    [property: JsonPropertyName("end_at")] string EndAt
);