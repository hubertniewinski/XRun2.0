namespace XRun.Models.Trainings;

public interface IRunData
{
    public decimal Distance { get; set; }
    public decimal Time { get; set; }
    public decimal? RestingHeartRate { get; set; }
    public decimal? MaximumHeartRate { get; set; }
    public decimal? AverageHeartRate { get; set; }
    public decimal AveragePace { get; set; }
    public int BurnedCalories { get; set; }
    public decimal? ElevetionGain { get; set; }
}