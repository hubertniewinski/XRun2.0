namespace XRun.Models.AIChats;

public class ChatSession
{
    public required string Name { get; set; }
    public List<SessionMessage> History { get; set; } = new List<SessionMessage>();
    public SessionStatus Status { get; set; } = SessionStatus.Active;
    public required AIChat AIChat { get; set; }
    public required Client Client { get; set; }
    
    public ChatSession(string name, AIChat aiChat, Client client)
    {
        Name = name;
        AIChat = aiChat;
        Client = client;
    }
    
    public void AddMessage(SessionMessage message) => History.Add(message);
    public void SetStatus(SessionStatus status) => Status = status;
    public bool CanBeArchived() => History.Last().OccurredAt.AddDays(30) < DateTime.Now;
}