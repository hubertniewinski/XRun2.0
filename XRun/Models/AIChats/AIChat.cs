using XRun.Extensions;

namespace XRun.Models.AIChats;

public class AIChat
{
    public static string ModelName => "gemini-1.5-flash-latest";
    public static SafetySetting[] SafetySettings = {
        new ("HARM_CATEGORY_HARASSMENT", "BLOCK_MEDIUM_AND_ABOVE"),
        new ("HARM_CATEGORY_HATE_SPEECH", "BLOCK_MEDIUM_AND_ABOVE"),
        new ("HARM_CATEGORY_SEXUALLY_EXPLICIT", "BLOCK_MEDIUM_AND_ABOVE"),
        new ("HARM_CATEGORY_DANGEROUS_CONTENT", "BLOCK_MEDIUM_AND_ABOVE")
    };
    public static List<AIChat> Chats { get; set; } = new List<AIChat>();
    public string Type { get; set; }
    public string SystemInstructions { get; set; }
    public List<Client> Clients { get; set; } = new List<Client>();
    public List<ChatSession> Sessions { get; set; } = new List<ChatSession>();
    
    public AIChat(string type, string systemInstructions)
    {
        Type = type;
        SystemInstructions = systemInstructions;
    }
    
    public static void AddChat(AIChat chat) => Chats.AddToExtent(chat);
    
    public static void RemoveChat(AIChat chat) => Chats.RemoveFromExtent(chat);
    
    public static IEnumerable<AIChat> GetChats() => Chats;

    public void AddClient(Client client)
    {
        if(Clients.Contains(client))
        {
            return;
        }
        
        Clients.Add(client);
        client.AddAIChat(this);
    }
    
    public void RemoveClient(Client client)
    {
        if(!Clients.Contains(client))
        {
            return;
        }
        
        Clients.Remove(client);
        client.RemoveAIChat(this);
    }
    
    public void AddChatSession(ChatSession session)
    {
        if(Sessions.Contains(session))
        {
            return;
        }
        
        Sessions.Add(session);
        session.AIChat = this;
        session.Client.AddChatSession(session);
    }

    public static void ArchiveSessions()
    {
        foreach(var chat in Chats)
        {
            foreach(var session in chat.Sessions)
            {
                if(session.CanBeArchived())
                {
                    session.SetStatus(SessionStatus.Archived);
                }
            }
        }
    }
}