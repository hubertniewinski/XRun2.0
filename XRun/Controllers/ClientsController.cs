using Microsoft.AspNetCore.Mvc;
using XRun.Dtos;
using XRun.Models;
using XRun.Models.AIChats;

namespace XRun.Controllers;

[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> GetClientsAsync([FromQuery] Guid token)
    {
        AuthService.ValidateAdmin(token);
        
        var clients = Client.Clients;
        var result = clients.Select(x => new ClientDashboardDto(x.Id, x.FullName, x.AIChats.Count)).ToList();
        return Task.FromResult<IActionResult>(Ok(result));
    }
    
    [HttpGet("{id}/assignedChats")]
    public Task<IActionResult> GetAssignedChatsAsync([FromRoute] Guid id, [FromQuery] Guid token)
    {
        AuthService.ValidateAdminOrClient(token, id);
        
        var client = Client.Clients.FirstOrDefault(x => x.Id == id);
        if (client is null)
        {
            return Task.FromResult<IActionResult>(NotFound());
        }
        
        var chatTypes = client.AIChats.Select(x => x.Type).ToList();
        return Task.FromResult<IActionResult>(Ok(chatTypes));
    }
    
    [HttpPost("assignChat/{clientId}")]
    public Task<IActionResult> AssignChatAsync([FromRoute] Guid clientId, [FromBody] AssignChatDto assignChatDto)
    {
        AuthService.ValidateAdmin(assignChatDto.Token);
        
        var client = Client.Clients.FirstOrDefault(x => x.Id == clientId);
        if (client is null)
        {
            return Task.FromResult<IActionResult>(NotFound());
        }
        
        var chat = AIChat.Chats.FirstOrDefault(x => x.Type == assignChatDto.ChatType);
        if (chat is null)
        {
            return Task.FromResult<IActionResult>(Conflict("Chat not found"));
        }
        
        var clientOwnsChat = client.AIChats.Any(x => x.Type == assignChatDto.ChatType);
        if (clientOwnsChat)
        {
            return Task.FromResult<IActionResult>(Conflict("Client already owns this chat"));
        }
        
        client.AddAIChat(chat);
        return Task.FromResult<IActionResult>(Ok());
    }
}