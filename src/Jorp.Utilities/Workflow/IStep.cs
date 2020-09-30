namespace Jorp.Utilities.Workflow
{
    public interface IStep
    {
        void Execute();        
        StepSettings StepSettings { get; set; }
    }
}