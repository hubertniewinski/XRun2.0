namespace XRun.Models;

public class HealthInformation
{
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal? BodyFatPercentage { get; set; }
    public decimal? LeanBodyMass { get; set; }
    public decimal? LactateThreshold { get; set; }

    public HealthInformation(decimal height, decimal weight, decimal? bodyFatPercentage, decimal? leanBodyMass, decimal? lactateThreshold)
    {
        Height = height;
        Weight = weight;
        BodyFatPercentage = bodyFatPercentage;
        LeanBodyMass = leanBodyMass;
        LactateThreshold = lactateThreshold;
    }
}