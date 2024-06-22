using Microsoft.AspNetCore.Mvc;
using XRun.Dtos;
using XRun.Models;

namespace XRun.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost]
    public Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        var token = AuthService.GetToken(loginDto.Login, loginDto.Password);
        if (token is null)
        {
            return Task.FromResult<IActionResult>(Conflict("Invalid login or password"));
        }

        var isAdmin = AuthService.IsAdmin(token.Value);
        var obj = new
        {
            Token = token,
            IsAdmin = isAdmin,
            Id = AuthService.Tokens[token.Value],
            Name = isAdmin ? "Admin" : Client.Clients.First(x => x.Id == AuthService.Tokens[token.Value]).FullName
        };
        return Task.FromResult<IActionResult>(Ok(obj));
    }

    [HttpPost("info/{id}")]
    public Task<IActionResult> GetUserInfo([FromBody] Guid id, [FromQuery] Guid token)
    {
        var isAdmin = AuthService.IsAdmin(token);
        
        if (!isAdmin && !AuthService.IsClient(token, id))
        {
            return Task.FromResult<IActionResult>(Unauthorized());
        }
        
        var clientName = Client.Clients.First(x => x.Id == id).FullName;
        return Task.FromResult<IActionResult>(Ok(new { Name = clientName }));
    }
}