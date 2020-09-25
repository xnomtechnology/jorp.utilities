namespace Jorp.Utilities.Models
{
    public interface IStep
    {
        void Execute();        
        StepSettings StepSettings { get; set; }
    }
}