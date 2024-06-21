namespace XRun.Models.Trainings;

public class IntervalTraining : IRunData
{
    public required SpeedTraining SpeedTraining { get; set; }
    public required decimal Distance { get; set; }
    public required decimal Time { get; set; }
    public decimal? RestingHeartRate { get; set; }
    public decimal? MaximumHeartRate { get; set; }
    public required decimal? AverageHeartRate { get; set; }
    public required decimal AveragePace { get; set; }
    public required int BurnedCalories { get; set; }
    public decimal? ElevetionGain { get; set; }

    public IntervalTraining(SpeedTraining speedTraining, decimal distance, decimal time, decimal? restingHeartRate, decimal? maximumHeartRate,
        decimal? averageHeartRate, decimal averagePace, int burnedCalories, decimal? elevetionGain)
    {
        SetSpeedTraining(speedTraining);
        Distance = distance;
        Time = time;
        RestingHeartRate = restingHeartRate;
        MaximumHeartRate = maximumHeartRate;
        AverageHeartRate = averageHeartRate;
        AveragePace = averagePace;
        BurnedCalories = burnedCalories;
        ElevetionGain = elevetionGain;
    }
    
    public void SetSpeedTraining(SpeedTraining speedTraining)
    {
        if (SpeedTraining == speedTraining)
        {
            return;
        }
        
        if(SpeedTraining is not null)
        {
            SpeedTraining.RemoveIntervalTraining(this);
        }

        SpeedTraining = speedTraining;
        SpeedTraining?.AddIntervalTraining(this);
    }
}