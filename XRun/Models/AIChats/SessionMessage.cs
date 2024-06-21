namespace XRun.Models.AIChats;

public class SessionMessage
{
    public required SessionMessageRole Role { get; set; }
    public required IEnumerable<string> Parts { get; set; }
    public required DateTime OccurredAt { get; set; } = DateTime.Now;
    
    public SessionMessage(SessionMessageRole role, IEnumerable<string> parts)
    {
        Role = role;
        Parts = parts;
    }
}