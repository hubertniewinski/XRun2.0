namespace XRun.Models.Trainings;

public class Training
{
    public required Client Client { get; set; }
    public required DateTime Date { get; set; }
    public IRunData? LongRunTraining { get; set; }
    public SpeedTraining? SpeedTraining { get; set; }
    public required List<TrainingType> TrainingType { get; set; } = new List<TrainingType>();

    public Training(Client client, DateTime date, IRunData longRunTraining)
        : this(client, date, new List<TrainingType>()
        {
            Trainings.TrainingType.LongRunTraining
        })
    {
        LongRunTraining = longRunTraining;
    }
    
    public Training(Client client, DateTime date, SpeedTraining speedTraining)
        : this(client, date, new List<TrainingType>()
        {
            Trainings.TrainingType.SpeedTraining
        })
    {
        SetSpeedTraining(speedTraining);
    }
    
    public Training(Client client, DateTime date, IRunData longRunTraining, SpeedTraining speedTraining)
        : this(client, date, new List<TrainingType>() { 
            Trainings.TrainingType.LongRunTraining, 
            Trainings.TrainingType.SpeedTraining 
        })
    {
        LongRunTraining = longRunTraining;
        SetSpeedTraining(speedTraining);
    }
    
    private Training(Client client, DateTime date, List<TrainingType> trainingTypes)
    {
        SetClient(client);
        Date = date;
        TrainingType = trainingTypes;
    }
    
    public void SetSpeedTraining(SpeedTraining speedTraining)
    {
        if(!TrainingType.Contains(Trainings.TrainingType.SpeedTraining))
        {
            throw new InvalidOperationException("This training does not support speed training");
        }
        
        if (SpeedTraining == speedTraining)
        {
            return;
        }
        
        if(SpeedTraining is not null)
        {
            SpeedTraining.SetTraining(null);
        }
        
        SpeedTraining = speedTraining;
        SpeedTraining?.SetTraining(this);
    }

    public void SetClient(Client client)
    {
        if (Client == client)
        {
            return;
        }
        
        if(Client is not null)
        {
            Client.RemoveTraining(this);
        }

        Client = client;
        Client?.AddTraining(this);
    }
    
    public int GetBurnedCalories()
    {
        var burnedCalories = 0;
        foreach(var trainingType in TrainingType)
        {
            burnedCalories += trainingType switch
            {
                Trainings.TrainingType.LongRunTraining => LongRunTraining?.BurnedCalories ?? 0,
                Trainings.TrainingType.SpeedTraining => SpeedTraining?.BurnedCalories ?? 0,
                _ => 0
            };
        }

        return burnedCalories;
    }
}