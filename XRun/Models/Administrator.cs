using XRun.Models.AIChats;

namespace XRun.Models;

public class Administrator : User
{
    public void AssignChat(Client client, AIChat chat)
        => client.AddAIChat(chat);
    
    public void TakeChat(Client client, AIChat chat)
        => client.RemoveAIChat(chat);
}