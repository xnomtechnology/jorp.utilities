using System;
using System.Collections.Generic;
using System.Linq;

namespace Jorp.Utilities.Models
{
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
}
