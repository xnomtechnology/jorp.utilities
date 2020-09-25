namespace Jorp.Utilities.Models
{
    /// <summary>
    /// Workflow state of complette singelton run
    /// </summary>
    public enum State
    {
        Ready,
        Suspended,
        Locked,
        Completed
    }
}