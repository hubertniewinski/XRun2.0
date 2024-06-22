using Microsoft.AspNetCore.Mvc;
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
}