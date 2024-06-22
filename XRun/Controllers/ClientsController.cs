using Microsoft.AspNetCore.Mvc;
using XRun.Dtos;
using XRun.Models;
using XRun.Models.AIChats;

namespace XRun.Controllers;

[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> GetClientsAsync(CancellationToken cancellationToken)
    {
        var clients = Client.Clients;
        var result = clients.Select(x => new ClientDashboardDto(x.Id, x.FullName, x.AIChats.Count)).ToList();
        return Task.FromResult<IActionResult>(Ok(result));
    }
    
    [HttpGet("{id}/assignedChats")]
    public Task<IActionResult> GetAssignedChatsAsync([FromRoute] Guid id)
    {
        var client = Client.Clients.FirstOrDefault(x => x.Id == id);
        if (client is null)
        {
            return Task.FromResult<IActionResult>(NotFound());
        }
        
        var chatTypes = client.AIChats.Select(x => x.Type).ToList();
        return Task.FromResult<IActionResult>(Ok(chatTypes));
    }
    
    [HttpPost("assignChat/{clientId}")]
    public Task<IActionResult> AssignChatAsync([FromRoute] Guid clientId, [FromBody] string chatType)
    {
        var client = Client.Clients.FirstOrDefault(x => x.Id == clientId);
        if (client is null)
        {
            return Task.FromResult<IActionResult>(NotFound());
        }
        
        var chat = AIChat.Chats.FirstOrDefault(x => x.Type == chatType);
        if (chat is null)
        {
            return Task.FromResult<IActionResult>(Conflict("Chat not found"));
        }
        
        var clientOwnsChat = client.AIChats.Any(x => x.Type == chatType);
        if (clientOwnsChat)
        {
            return Task.FromResult<IActionResult>(Conflict("User already has this chat on list. Choose another one and then try again"));
        }
        
        client.AddAIChat(chat);
        return Task.FromResult<IActionResult>(Ok());
    }
}