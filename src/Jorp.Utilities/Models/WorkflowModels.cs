using System;
using System.Collections.Generic;
using System.Linq;

namespace Jorp.Utilities.Models
{
    public interface IStep
    {
        void Execute();        
        StepSettings StepSettings { get; set; }
    }


    /// <summary>
    /// Implementation of workflow flow, singelton execution
    /// </summary>
    public class Workflow
    {
        public Workflow()
        {
            Steps = new List<IStep>();
            Result = new WorkflowResult() 
            {
                Exceptions = new Exception(), 
                State = State.Ready 
            };
        }

        public Workflow(params dynamic[] param) : this() 
            => GlobalParameters = param.ToArray<dynamic>();
        

        public IEnumerable<dynamic> GlobalParameters { get; set; }

        public bool AbortOnError { get; set; }
        /// <summary>
        /// list of steps
        /// </summary>
        public IList<IStep> Steps { get; set; } 

        /// <summary>
        /// Result of every workflow execution
        /// </summary>
        public WorkflowResult Result { get; set; }

        /// <summary>
        /// Add workflow steps
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public Workflow AddSteps(IStep step) 
        {
            Steps.Add(step);
            return this;
        }

        public Workflow SetAbortOnError(bool abort)
        {
            AbortOnError = abort;
            return this;
        }
    }

    /// <summary>
    /// Step settings for requried properties
    /// </summary>
    public class StepSettings
    {
        public Status Status { get; set; }
        public Exception InnerExceptions { get; set; }


        
        
    }

    public static class StepSettingHelper
    {
        public static Status SetSettingsStepStatus(this Status status, Status newStatus) => status != null ? newStatus : status;
    }

    public class WorkflowResult
    {
        public State State { get; set; }
        
        public Exception Exceptions { get; set; }
    }


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
