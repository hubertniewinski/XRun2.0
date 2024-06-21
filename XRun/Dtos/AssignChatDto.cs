namespace XRun.Dtos;

public class AssignChatDto
{
    public Guid Token { get; set; }
    public string ChatType { get; set; }
    
    public AssignChatDto(Guid token, string chatType)
    {
        Token = token;
        ChatType = chatType;
    }
}