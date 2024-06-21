using XRun.Extensions;
using XRun.Models.AIChats;
using XRun.Models.Trainings;

namespace XRun.Models;

public class Client : User
{
    public static List<Client> Clients { get; } = new();
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public bool Sex { get; set; }
    public HealthInformation? HealthInformation { get; set; } = null;
    public Localization Localization { get; set; }
    public List<AIChat> AIChats { get; set; } = new List<AIChat>();
    public List<ChatSession> Sessions { get; set; } = new List<ChatSession>();
    public List<Training> Trainings { get; set; } = new List<Training>();

    public Client(string name, string surname, DateTime birthDate, bool sex, HealthInformation? healthInformation,
        Localization localization, string login, string password) : base(login, password)
    {
        Name = name;
        Surname = surname;
        BirthDate = birthDate;
        Sex = sex;
        HealthInformation = healthInformation;
        Localization = localization;
    }

    public void SetHealthInformation(HealthInformation healthInformation) => HealthInformation = healthInformation;
    
    public string FullName => $"{Name} {Surname}";
    
    public static void AddClient(Client client) => Clients.AddToExtent(client);

    public static void RemoveClient(Client client) => Clients.RemoveFromExtent(client);

    public void AddTraining(Training training)
    {
        if (training is null || Trainings.Contains(training))
        {
            return;
        }
        
        Trainings.Add(training);
        training.SetClient(this);
    }
    
    public void RemoveTraining(Training training)
    {
        if (!Trainings.Contains(training))
        {
            return;
        }
        
        Trainings.Remove(training);
        training.SetClient(null);
    }
    
    public void AddAIChat(AIChat aiChat)
    {
        if (AIChats.Contains(aiChat))
        {
            return;
        }
        
        AIChats.Add(aiChat);
        aiChat.AddClient(this);
    }

    public void RemoveAIChat(AIChat aiChat)
    {
        if (!AIChats.Contains(aiChat))
        {
            return;
        }
        
        AIChats.Remove(aiChat);
        aiChat.RemoveClient(this);
    }
    
    public void AddChatSession(ChatSession session)
    {
        if (Sessions.Contains(session))
        {
            return;
        }
        
        Sessions.Add(session);
        session.Client = this;
        session.AIChat.AddChatSession(session);
    }
    
    public ChatSession GetSession(string name) => Sessions.FirstOrDefault(s => s.Name == name);
    
    public void WriteMessage(string name, SessionMessage message)
    {
        var session = GetSession(name);
        session?.AddMessage(message);
    }
}