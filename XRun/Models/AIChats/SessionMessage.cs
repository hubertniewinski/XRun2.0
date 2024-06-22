namespace XRun.Models.AIChats;

public class SessionMessage
{
    public SessionMessageRole Role { get; set; }
    public IEnumerable<string> Parts { get; set; }
    public DateTime OccurredAt { get; set; } = DateTime.Now;
    
    public SessionMessage(SessionMessageRole role, IEnumerable<string> parts)
    {
        Role = role;
        Parts = parts;
    }
}