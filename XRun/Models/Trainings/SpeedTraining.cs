namespace XRun.Models.Trainings;

public class SpeedTraining
{
    public Training Training { get; set; }
    public decimal RegenerationTime { get; set; }
    private List<IntervalTraining> _intervals { get; set; } = new();
    public IEnumerable<IntervalTraining> Intervals => _intervals;
    public int BurnedCalories => Intervals.Sum(i => i.BurnedCalories);
    
    public SpeedTraining(Training training, decimal regenerationTime)
    {
        SetTraining(training);
        RegenerationTime = regenerationTime;
    }
    
    public void SetTraining(Training training)
    {
        if (Training == training)
        {
            return;
        }
        
        if(Training is not null)
        {
            Training.SetSpeedTraining(null);
        }
        
        Training = training;
        Training?.SetSpeedTraining(this);
    }

    public void AddIntervalTraining(IntervalTraining intervalTraining)
    {   
        if(intervalTraining is null || Intervals.Contains(intervalTraining))
        {
            return;
        }
        
        _intervals.Add(intervalTraining);
        intervalTraining.SetSpeedTraining(this);
    }

    public void RemoveIntervalTraining(IntervalTraining intervalTraining)
    {   
        if(!Intervals.Contains(intervalTraining))
        {
            return;
        }
        
        _intervals.Remove(intervalTraining);
        intervalTraining.SetSpeedTraining(null);
    }
}