using Jorp.Utilities.Models;
using System;
using System.Linq;

namespace Jorp.Utilities.Extentions
{
    public static class WorkflowExtentions
    {
        public static WorkflowResult Process(this Workflow workflow)
        {
            try
            {
                workflow.Steps.ToList()
                .ForEach(_ =>
                {
                    _.Execute();
                });

                workflow.Result.Status = Status.Completed;

            }
            catch (Exception e)
            {

                workflow.Result.Exceptions = e.InnerException;
                workflow.Result.Status = Status.Suspended;
            }            

            return workflow.Result;
        }
    }
}
