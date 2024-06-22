namespace XRun.Dtos;

public class ChatAssignmentDto
{
    public string Type { get; set; }
    public IEnumerable<string> Clients { get; set; }
    
    public ChatAssignmentDto(string type, IEnumerable<string> clients)
    {
        Type = type;
        Clients = clients;
    }
}