using Jorp.Utilities.Models;
using NUnit.Framework;
using System;
using Jorp.Utilities.Extentions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jorp.Utilities.Tests.Extentions
{
    [TestFixture]
    public class WorkflowExtentionTests
    {
        public Workflow TestWorkflow { get; set; }

        [SetUp]
        public void Init()
        {            
            TestWorkflow = new Workflow();            
        }

        [TearDown]
        public void Dispose()
        {
            TestWorkflow = null;
        }

        [Test]
        public void BaseWorkflowTest()
        {
            var abortOnError = false;
            TestWorkflow
                .SetAbortOnError(true)
                .AddSteps(new Step1() 
                { 
                    StepSettings = new StepSettings() {  Status = Status.WaitingForResources }
                })
                .AddSteps(new Step2()
                {
                    StepSettings = new StepSettings() { Status = Status.WaitingForResources }
                });


            var result = TestWorkflow.ExecuteAsync(1, abortOnError);
            
            Assert.That(result.State, Is.EqualTo(State.Completed), "Expect state to be Completed");
            //Assert.That(result.Exceptions, Is.EqualTo(null), "Expect worklfow exception to be Completed");
        }


        [Test]
        public void BaseWorkflowAsyncTest()
        {
            var abortOnError = false;
            TestWorkflow
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


            var result = TestWorkflow.ExecuteAsync(2, abortOnError);

            Assert.That(result.State, Is.EqualTo(State.Completed), "Expect state to be Completed");
            //Assert.That(result.Exceptions, Is.EqualTo(null), "Expect worklfow exception to be Completed");
        }
    }


    public class Step1 : IStep
    {
        public Exception InnerExceptions { get; set; }
        public StepSettings StepSettings { get; set; }

        public void Execute()
        {
            //Execute custom code
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("step1:" + DateTime.Now.ToString());
        }
    }

    public class Step2 : IStep
    {
        public Exception InnerExceptions { get; set; }
        public StepSettings StepSettings { get; set; }

        public void Execute()
        {
            //Execute custom code
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("step2: " + DateTime.Now.ToString());
        }
    }
}
