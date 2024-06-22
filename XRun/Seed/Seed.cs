using XRun.Models;
using XRun.Models.AIChats;

namespace XRun.Seed;

public class Seed
{
    public Seed()
    {
        var trainingPlanAIChat = new AIChat("Today training plan", 
            "It will provide you with information about the person. " +
            "Based on it, propose a training plan for today. " +
            "Please try to take into account all the information provided in the first call. " +
            "Each subsequent one will be a direct interaction with the user and you can develop a plan based on this");
        AIChat.AddChat(trainingPlanAIChat);
        
        var injuryAdvisorAIChat = new AIChat("Injury advisor",
            "User will provide injuries with whom he is struggling. " +
            "Try to present advices before consulting to doctor to faster recover from them. " +
            "If possible, you can present a chronological list of steps. " +
            "Do not generate information that you cannot give advice as a doctor i will provide it by myself in my application. " +
            "Name each section with desired title when writing.");
        AIChat.AddChat(injuryAdvisorAIChat);
        
        var client = new Client("John", "Doe", 
            new DateTime(1995, 05, 25), true, null, 
            new Localization("Ottawa", "Canada"), "john.doe", "password");
        Client.AddClient(client);
        
        var client2 = new Client("Jane", "Doe", 
            new DateTime(1998, 07, 15), false, null, 
            new Localization("Toronto", "Canada"), "jane.doe", "password");
        Client.AddClient(client2);
        
        var client3 = new Client("Alice", "Smith", 
            new DateTime(1990, 01, 30), false, null, 
            new Localization("Vancouver", "Canada"), "alice.smith", "password");
        Client.AddClient(client3);

        var client4 = new Client("Bob", "Johnson", 
            new DateTime(1985, 11, 20), true, null, 
            new Localization("Montreal", "Canada"), "bob.johnson", "password");
        Client.AddClient(client4);

        var client5 = new Client("Charlie", "Brown", 
            new DateTime(1992, 03, 15), true, null, 
            new Localization("Calgary", "Canada"), "charlie.brown", "password");
        Client.AddClient(client5);
        
        var client6 = new Client("Carlos", "Gomez",
            new DateTime(1987, 04, 12), true, null,
            new Localization("Madrid", "Spain"), "carlos.gomez", "password");
        Client.AddClient(client6);

        var client7 = new Client("Sophie", "Martin",
            new DateTime(1992, 06, 18), false, null,
            new Localization("Paris", "France"), "sophie.martin", "password");
        Client.AddClient(client7);

        var client8 = new Client("Luca", "Rossi",
            new DateTime(1990, 09, 30), true, null,
            new Localization("Rome", "Italy"), "luca.rossi", "password");
        Client.AddClient(client8);
        
        var admin = new Administrator("admin", "admin");
        Administrator.AddAdministrator(admin);
    }
}