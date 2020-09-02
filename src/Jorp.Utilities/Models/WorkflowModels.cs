using Jorp.Utilities.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jorp.Utilities.Models
{
    public interface IStep
    {
        void Execute();

        Exception InnerExceptions { get; set; } 
    }



    public class Workflow
    {
        public Workflow()
        {
            Steps = new List<IStep>();
            Result = new WorkflowResult() 
            {
                Exceptions = new Exception(), 
                Status = Status.Ready 
            };
        }

        public Workflow(params dynamic[] param) : this()
        {
            GlobalParameters = param.ToArray<dynamic>();
        }

        public IEnumerable<dynamic> GlobalParameters { get; set; }

        public IList<IStep> Steps { get; set; } 

        public WorkflowResult Result { get; set; }

        public Workflow AddSteps(IStep step) 
        {
            Steps.Add(step);
            return this;
        }
    }

    public class WorkflowResult
    {
        public Status Status { get; set; }
        public Exception Exceptions { get; set; }
    }

    public enum Status
    {
        Ready,
        Suspended,
        Locked,
        Completed
    }
}
