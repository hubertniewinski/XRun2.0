using XRun.Models;

namespace XRun;

public static class AuthService
{
    public static Dictionary<Guid, Guid> Tokens { get; } = new();
    
    public static void ValidateAdmin(Guid token)
    {
        if (!IsAdmin(token))
        {
            throw new UnauthorizedAccessException();
        }
    }
    
    public static Administrator GetAdmin(Guid token)
    {
        if (!IsAdmin(token))
        {
            throw new UnauthorizedAccessException();
        }
        
        return Administrator.Administrators.First(x => x.Id == Tokens[token]);
    }
    
    public static void ValidateAdminOrClient(Guid token, Guid clientId)
    {
        if (!IsAdmin(token) && !IsClient(token, clientId))
        {
            throw new UnauthorizedAccessException();
        }
    }

    public static bool IsAdmin(Guid token)
    {
        return Tokens.ContainsKey(token) && Administrator.Administrators.Any(x => x.Id == Tokens[token]);
    }
    
    public static bool IsClient(Guid token, Guid clientId)
    {
        var client = Client.Clients.FirstOrDefault(x => x.Id == clientId);
        return Tokens.ContainsKey(token) && client is not null && client.Id == Tokens[token];
    }
    
    public static Guid? GetToken(string login, string password)
    {
        var administrator = Administrator.Administrators.FirstOrDefault(x => x.Login == login && x.Password == password);
        if (administrator is not null)
        {
            var token = Guid.NewGuid();
            Tokens[token] = administrator.Id;
            return token;
        }
        
        var client = Client.Clients.FirstOrDefault(x => x.Login == login && x.Password == password);
        if (client is not null)
        {
            var token = Guid.NewGuid();
            Tokens[token] = client.Id;
            return token;
        }

        return null;
    }
}