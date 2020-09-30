using System;
using System.Collections.Generic;
using System.Linq;

namespace Jorp.Utilities.Workflow
{
    /// <summary>
    /// Implementation of workflow flow, singelton execution
    /// </summary>
    public class WorkflowBuilder
    {
        public WorkflowBuilder()
        {
            Steps = new List<IStep>();
            Result = new WorkflowResult() 
            {
                Exceptions = new Exception(), 
                State = State.Ready 
            };
        }

        public WorkflowBuilder(params dynamic[] param) : this() 
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
        public WorkflowBuilder AddSteps(IStep step) 
        {
            Steps.Add(step);
            return this;
        }

        public WorkflowBuilder SetAbortOnError(bool abort)
        {
            AbortOnError = abort;
            return this;
        }
    }
}
