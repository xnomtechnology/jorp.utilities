using NUnit.Framework;
using Jorp.Utilities.Quartz;

namespace Jorp.Utilities.Tests.Quartz
{
    [TestFixture]
    public class TriggerBuilderTests
    {

        public QuartzBuilder Trigger { get; set; }

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
            //https://crontab.guru/examples.html    
            var triggerBuilder = Trigger
                .Create()
                .WithCronSchedule("*/2 * * * *")
                .WithIdentity("test", "test1")
                .Validate();

            Assert.IsTrue(triggerBuilder.Result);
        }

    }
}
