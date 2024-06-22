using Newtonsoft.Json;
using XRun.Extensions;
using XRun.Models;
using XRun.Models.AIChats;

namespace XRun.Seed;

public class Seed
{
    private List<AIChat> AIChats { get; set; } = new List<AIChat>();
    private List<Client> Clients { get; set; } = new List<Client>();
    private List<Administrator> Administrators { get; set; } = new List<Administrator>();
    
    public Seed()
    {
        string aichatspath = "./aichats.json";
        string clientspath = "./clients.json";
        string administratorspath = "./administrators.json";

        if (File.Exists(aichatspath) && File.Exists(clientspath) && File.Exists(administratorspath))
        {
            var aichats = File.ReadAllText(aichatspath);
            var clients = File.ReadAllText(clientspath);
            var administrators = File.ReadAllText(administratorspath);
            
            AIChats = JsonConvert.DeserializeObject<List<AIChat>>(aichats);
            Clients = JsonConvert.DeserializeObject<List<Client>>(clients);
            Administrators = JsonConvert.DeserializeObject<List<Administrator>>(administrators);
            
            foreach(var aiChat in AIChats)
            {
                foreach (var client in aiChat.Clients)
                {
                    client.AddAIChat(aiChat);
                }
            }
            
            foreach(var client in Clients)
            {
                foreach (var aiChat in client.AIChats)
                {
                    aiChat.AddClient(client);
                }
            }
        }
        else
        {
            SeedData();
            Save();
        }
        
        foreach(var aiChat in AIChats)
        {
            AIChat.AddChat(aiChat);
        }
        
        foreach(var client in Clients)
        {
            Client.AddClient(client);
        }
        
        foreach(var admin in Administrators)
        {
            Administrator.AddAdministrator(admin);
        }
    }

    public static void Save()
    {
        var settings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize
        };

        Func<Client, Client> Copy = (x) => new Client(x.Name, x.Surname,
            x.BirthDate, x.Sex, x.HealthInformation, x.Localization, x.Login, x.Password)
        {
            AIChats = x.AIChats.Select(x => new AIChat(x.Type, x.SystemInstructions)
            {
                Clients = new List<Client>(),
                Sessions = x.Sessions,
            }).ToList(),
            Sessions = x.Sessions,
            Trainings = x.Trainings
        };
        
        Func<AIChat, AIChat> CopyChat = (x) => new AIChat(x.Type, x.SystemInstructions)
        {
            Clients = x.Clients.Select(Copy).ToList(),
            Sessions = x.Sessions
        };
        
        var clients = Client.Clients.Select(Copy).ToList();
        var chats = AIChat.Chats.Select(CopyChat).ToList();
        
        string json = JsonConvert.SerializeObject(chats, Formatting.Indented, settings);
        Console.WriteLine(json);
        File.WriteAllText("aichats.json", json);
        
        json = JsonConvert.SerializeObject(clients, Formatting.Indented, settings);
        Console.WriteLine(json);
        File.WriteAllText("clients.json", json);
        
        json = JsonConvert.SerializeObject(Administrator.Administrators, Formatting.Indented, settings);
        Console.WriteLine(json);
        File.WriteAllText("administrators.json", json);
    }

    private void SeedData()
    {
        var trainingPlanAIChat = new AIChat("Today training plan", 
            "It will provide you with information about the person. " +
            "Based on it, propose a training plan for today. " +
            "Please try to take into account all the information provided in the first call. " +
            "Each subsequent one will be a direct interaction with the user and you can develop a plan based on this");
        AIChats.Add(trainingPlanAIChat);
        
        var injuryAdvisorAIChat = new AIChat("Injury advisor",
            "User will provide injuries with whom he is struggling. " +
            "Try to present advices before consulting to doctor to faster recover from them. " +
            "If possible, you can present a chronological list of steps. " +
            "Do not generate information that you cannot give advice as a doctor i will provide it by myself in my application. " +
            "Name each section with desired title when writing.");
        AIChats.Add(injuryAdvisorAIChat);
        
        var client = new Client("John", "Doe", 
            new DateTime(1995, 05, 25), true, null, 
            new Localization("Ottawa", "Canada"), "john.doe", "password");
        Clients.Add(client);
        
        var client2 = new Client("Jane", "Doe", 
            new DateTime(1998, 07, 15), false, null, 
            new Localization("Toronto", "Canada"), "jane.doe", "password");
        Clients.Add(client2);
        
        var client3 = new Client("Alice", "Smith", 
            new DateTime(1990, 01, 30), false, null, 
            new Localization("Vancouver", "Canada"), "alice.smith", "password");
        Clients.Add(client3);

        var client4 = new Client("Bob", "Johnson", 
            new DateTime(1985, 11, 20), true, null, 
            new Localization("Montreal", "Canada"), "bob.johnson", "password");
        Clients.Add(client4);

        var client5 = new Client("Charlie", "Brown", 
            new DateTime(1992, 03, 15), true, null, 
            new Localization("Calgary", "Canada"), "charlie.brown", "password");
        Clients.Add(client5);
        
        var client6 = new Client("Carlos", "Gomez",
            new DateTime(1987, 04, 12), true, null,
            new Localization("Madrid", "Spain"), "carlos.gomez", "password");
        Clients.Add(client6);

        var client7 = new Client("Sophie", "Martin",
            new DateTime(1992, 06, 18), false, null,
            new Localization("Paris", "France"), "sophie.martin", "password");
        Clients.Add(client7);

        var client8 = new Client("Luca", "Rossi",
            new DateTime(1990, 09, 30), true, null,
            new Localization("Rome", "Italy"), "luca.rossi", "password");
        Clients.Add(client8);
        
        var admin = new Administrator("admin", "admin");
        Administrators.Add(admin);
    }
}