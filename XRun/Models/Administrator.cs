using XRun.Extensions;
using XRun.Models.AIChats;

namespace XRun.Models;

public class Administrator : User
{
    public static List<Administrator> Administrators { get; } = new();
    public Administrator(string login, string password) : base(login, password) { }
    public void AssignChat(Client client, AIChat chat) => client.AddAIChat(chat);
    public void TakeChat(Client client, AIChat chat) => client.RemoveAIChat(chat);
    public static void AddAdministrator(Administrator admin) => Administrators.AddToExtent(admin);
    public static void RemoveAdministrator(Administrator admin) => Administrators.RemoveFromExtent(admin);
}