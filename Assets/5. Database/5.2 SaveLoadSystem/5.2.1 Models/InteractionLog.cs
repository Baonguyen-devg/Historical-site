using System;

[Serializable]
public class InteractionLog
{
    public string ID { get; set; }
    public string PlayerID { get; set; } 
    public string CheckpointID { get; set; } 
    public string ChallengeID { get; set; } 
    public DateTime Timestamp { get; set; }
}