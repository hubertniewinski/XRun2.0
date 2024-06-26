namespace XRun.Models;

public class User
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Login { get; set; }
    public string Password { get; set; }
    
    public User(string login, string password)
    {
        Login = login;
        Password = password;
    }
}