using System.Net.Http.Json;
using System.Reflection.Metadata;
using Mcp.Models.Assignments;
using Mcp.Models.Courses;
public interface ICanvasService
{
    Task<List<Assignment>> GetAssignmentsAsync(int courseId);
    Task<List<Course>> GetCoursesAsync();
}

public class CanvasService(HttpClient http) : ICanvasService
{
    public async Task<List<Assignment>> GetAssignmentsAsync(int courseId)
    {
        List<Assignment>? response = await http.GetFromJsonAsync<List<Assignment>>($"courses/{courseId}/assignments?per_page=2000");
        if (response is null) return new List<Assignment>();
        response.RemoveAll(a => a.PointsPossible == 0);
        return response ?? new List<Assignment>();
    }
    public async Task<List<Course>> GetCoursesAsync()
    {
        List<Course>? response = await http.GetFromJsonAsync<List<Course>>($"courses?enrollment_state=active&include[]=term&per_page=1000");
        if (response is null) return new List<Course>();
        return response ?? new List<Course>();
    }
}