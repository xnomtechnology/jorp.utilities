using System;

namespace Jorp.Utilities.Workflow
{
    public class WorkflowResult
    {
        public State State { get; set; }
        
        public Exception Exceptions { get; set; }
    }
}