using System;

namespace Jorp.Utilities.Models
{
    public class WorkflowResult
    {
        public State State { get; set; }
        
        public Exception Exceptions { get; set; }
    }
}