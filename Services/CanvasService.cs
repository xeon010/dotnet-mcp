using System.Net.Http.Json;
using System.Reflection.Metadata;
using Mcp.Models;
public interface ICanvasService
{
    Task<List<Assignment>> GetAssignmentsAsync(int courseId);
}

public class CanvasService(HttpClient http) : ICanvasService
{
    public async Task<List<Assignment>> GetAssignmentsAsync(int courseId)
    {
        List<Assignment>? response = await http.GetFromJsonAsync<List<Assignment>>($"courses/{courseId}/assignments?per_page=1000");
        if (response is null) return new List<Assignment>();
        response.RemoveAll(a => a.PointsPossible == 0);
        return response ?? new List<Assignment>();
    }
}