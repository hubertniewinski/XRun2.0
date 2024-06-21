namespace XRun.Models;

public class Localization
{
    public string City { get; set; }
    public string Country { get; set; }

    public Localization(string city, string country)
    {
        City = city;
        Country = country;
    }
}