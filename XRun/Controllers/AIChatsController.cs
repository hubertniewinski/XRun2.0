using Microsoft.AspNetCore.Mvc;
using XRun.Dtos;
using XRun.Models.AIChats;

namespace XRun.Controllers;

[Route("api/[controller]")]
public class AIChatsController : ControllerBase
{
    [HttpGet("availableChats")]
    public Task<IActionResult> GetAvailableChatsAsync([FromQuery] Guid token)
    {
        AuthService.ValidateAdmin(token);
        
        var chats = AIChat.Chats;
        var result = chats.Select(x => x.Type).ToList();

        return Task.FromResult<IActionResult>(Ok(result));
    }

    [HttpGet("chatAssignments")]
    public Task<IActionResult> GetChatAssignmentAsync([FromQuery] Guid token)
    {
        AuthService.ValidateAdmin(token);
        var chatAssignments = AIChat.Chats.Select(x => new ChatAssignmentDto(x.Type, x.Clients.Select(y => y.FullName))).ToList();  
        return Task.FromResult<IActionResult>(Ok(chatAssignments));
    }
}