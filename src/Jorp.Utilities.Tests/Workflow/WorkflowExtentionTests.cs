using NUnit.Framework;
using Jorp.Utilities.Workflow;

namespace Jorp.Utilities.Tests.Extentions
{
    [TestFixture]
    public class WorkflowExtentionTests
    {
        public WorkflowBuilder TestWorkflowBuilder { get; set; }

        [SetUp]
        public void Init()
        {            
            TestWorkflowBuilder = new Workflow.WorkflowBuilder();            
        }

        [TearDown]
        public void Dispose()
        {
            TestWorkflowBuilder = null;
        }

        [Test]
        public void BaseWorkflowTest()
        {
            var abortOnError = false;
            TestWorkflowBuilder
                .SetAbortOnError(true)
                .AddSteps(new Step1() 
                { 
                    StepSettings = new StepSettings() {  Status = Status.WaitingForResources }
                })
                .AddSteps(new Step2()
                {
                    StepSettings = new StepSettings() { Status = Status.WaitingForResources }
                });


            var result = TestWorkflowBuilder.ExecuteAsync(1, abortOnError);
            
            Assert.That(result.State, Is.EqualTo(State.Completed), "Expect state to be Completed");
            Assert.That(result.Exceptions.InnerException, Is.EqualTo(null), "Expect worklfow exception to be Empty/null");
        }


        [Test]
        public void BaseWorkflowAsyncTest()
        {
            var abortOnError = false;
            TestWorkflowBuilder
                .SetAbortOnError(true)
                .AddSteps(new Step1()
                {
                    StepSettings = new StepSettings() { Status = Status.WaitingForResources }
                })
                .AddSteps(new Step2()
                {
                    StepSettings = new StepSettings() { Status = Status.WaitingForResources }
                })
                .AddSteps(new Step1()
                {
                    StepSettings = new StepSettings() { Status = Status.WaitingForResources }
                })
                .AddSteps(new Step2()
                {
                    StepSettings = new StepSettings() { Status = Status.WaitingForResources }
                });


            var result = TestWorkflowBuilder.ExecuteAsync(2, abortOnError);

            Assert.That(result.State, Is.EqualTo(State.Completed), "Expect state to be Completed");
            Assert.That(result.Exceptions.InnerException, Is.EqualTo(null), "Expect worklfow exception to be Empty/null");
        }
    }
}
