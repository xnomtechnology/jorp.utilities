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
            TestWorkflow
                .AddSteps(new Step1())
                .AddSteps(new Step2());

            var result = TestWorkflow.Process();

            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }
    }


    public class Step1 : IStep
    {
        public Exception InnerExceptions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Execute()
        {
            
        }
    }

    public class Step2 : IStep
    {
        public Exception InnerExceptions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Execute()
        {

        }
    }
}
