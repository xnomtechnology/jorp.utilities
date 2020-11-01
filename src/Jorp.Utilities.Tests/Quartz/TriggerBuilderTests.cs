using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Jorp.Utilities.Quartz;

namespace Jorp.Utilities.Tests.Quartz
{
    [TestFixture]
    public class TriggerBuilderTests
    {

        public TriggerBuilder Trigger { get; set; }

        [SetUp]
        public void Init()
        {
            
        }

        [TearDown]
        public void Dispose()
        {
            
        }

        [Test]
        public void BaseWorkflowTest()
        {
            var builder = Trigger
                .Create()
                .WithCronSchedule("test").WithIdentity("");
        }

    }
}
