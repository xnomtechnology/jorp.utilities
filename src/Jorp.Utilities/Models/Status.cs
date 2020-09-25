namespace Jorp.Utilities.Models
{
    /// <summary>
    /// Status for step status
    /// </summary>
    public enum Status
    {        
        WaitingForResources,
        Waiting,
        InProgress,
        Canceling,
        Succeeded,
        Failed,
        Canceled
    }
}