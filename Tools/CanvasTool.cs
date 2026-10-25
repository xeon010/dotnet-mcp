using System.ComponentModel;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using Mcp.Models.Assignments;
using Mcp.Models.Courses;

// TaskSupport is an experimental SDK API; Forbidden stops clients (e.g. the inspector) from calling these as tasks.
#pragma warning disable MCPEXP001

[McpServerToolType]
public class CanvasMcpTools
{
    private readonly ICanvasService _canvasService;

    // The DI container automatically resolves this service
    public CanvasMcpTools(ICanvasService canvasService)
    {
        _canvasService = canvasService;
    }
    
    [McpServerTool(TaskSupport = ToolTaskSupport.Forbidden)]
    [Description("Retrieves assignments from a course via its courseId.")]
    public async Task<List<Assignment>> GetAssignmentsAsync(int courseId = 1894777)
    {
        return await _canvasService.GetAssignmentsAsync(courseId);
    }
    [McpServerTool(TaskSupport = ToolTaskSupport.Forbidden)]
    [Description("Retrieves all current courses in Canvas.")]
    public async Task<List<Course>> GetCoursesAsync()
    {
        return await _canvasService.GetCoursesAsync();
    }
}