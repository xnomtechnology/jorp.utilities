namespace Jorp.Utilities.Workflow
{
    /// <summary>
    /// WorkflowBuilder state of complette singelton run
    /// </summary>
    public enum State
    {
        Ready,
        Suspended,
        Locked,
        Completed
    }
}