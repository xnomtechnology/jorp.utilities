using System;
using System.Linq;

namespace Jorp.Utilities.Workflow
{
    public static class WorkflowBuilderExtentions
    {
        /// <summary>
        /// Execute steps synchronously 
        /// </summary>
        /// <param name="workflow"></param>
        /// <returns></returns>
        public static WorkflowResult Execute(this WorkflowBuilder workflow) => ExecuteAsync(workflow, null);

        /// <summary>
        /// Execute steps synchronously 
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public static WorkflowResult ExecuteAsync(this WorkflowBuilder workflow, int? maxDegreeOfParallelism, bool abortOnStepError = true)
        {
            workflow.Result.State = State.Ready;

            if (workflow == null) throw new ArgumentNullException(nameof(workflow));

            if (maxDegreeOfParallelism is null) maxDegreeOfParallelism = 1;
            
            var aggregateException = new AggregateException();

            try
            {
                workflow
                    .Steps
                    .ToList()                    
                    .ParallelThreadingInvoke(1, maxDegreeOfParallelism, 
                        abortOnStepError, 
                        ref aggregateException,
                        step => 
                        {
                            step.First()
                                .StepSettings?
                                .Status.SetSettingsStepStatus(Status.InProgress);

                            //Execute
                            step.FirstOrDefault().Execute();

                            step.FirstOrDefault()
                                .StepSettings?
                                .Status.SetSettingsStepStatus(Status.Succeeded);                        
                        });

                workflow.Result.State = State.Completed;

                if (aggregateException.InnerExceptions.Any())                
                    throw new AggregateException("Faild to execute WorkflowBuilder", aggregateException.InnerExceptions);
                
            }
            catch (Exception e)
            {

                workflow.Result.Exceptions = e.InnerException;
                workflow.Result.State = State.Suspended;
            }

            return workflow.Result;            
        }
    }
}
