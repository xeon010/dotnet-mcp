using System.ComponentModel;
using ModelContextProtocol.Server;
using Mcp.Models;

[McpServerToolType]
public class CanvasMcpTools
{
    private readonly ICanvasService _canvasService;

    // The DI container automatically resolves this service
    public CanvasMcpTools(ICanvasService canvasService)
    {
        _canvasService = canvasService;
    }
    
    [McpServerTool]
    [Description("Retrieves all Canvas assignments.")]
    public async Task<List<Assignment>> GetAssignmentsAsync(int courseId)
    {
        return await _canvasService.GetAssignmentsAsync(courseId);
    }
}