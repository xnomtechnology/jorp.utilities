using System;
using System.Collections.Generic;
using System.Linq;
using Jorp.Utilities.Extentions;
using NUnit.Framework;

namespace Jorp.Utilities.Tests.Extentions
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [Test]
        public void BaseEnumerableExtensionsChunkTest()
        {
            var testList = Enumerable.Range(1, 10000).Select(_=> _);
            var batchList = testList.Chunk(100);

            Assert.That(batchList.Count(), Is.EqualTo(100), "batch size does not match");
        }

        [Test]
        public void EnumerableExtensionsPartielChunkTest()
        {
            var testList = Enumerable.Range(1, 10000).Select(_ => _);
            var batchList = testList.Chunk(350);

            Assert.IsTrue(batchList.Any());
            Assert.That(batchList.ToList()[28].Count(), Is.EqualTo(200), "last chunk should contain 200 items");
            Assert.That(batchList.Count(), Is.EqualTo(29), "batch size does not match");
        }

        [Test]
        public void EnumerableExtensionsParallelThreadingAsyncInvokeTest()
        {
            var counter = 0;
            var list = Enumerable.Range(1, 10000);
            IList<Exception> thredExceptions = new List<Exception>();

            TestContext.WriteLine($"Start at: {DateTime.Now.ToLongTimeString()}");
            list.ParallelThreadingInvoke(100, 10, ref thredExceptions, (data)=>  Action(data, ref counter));
            TestContext.WriteLine($"End at: {DateTime.Now.ToLongTimeString()}");

            Assert.That(counter, Is.GreaterThan(0));
            Assert.That(thredExceptions.Count(), Is.LessThanOrEqualTo(0));
        }


        [Test]
        public void EnumerableExtensionsParallelThreadingSyncInvokeTest()
        {
            var counter = 0;
            var list = Enumerable.Range(1, 10000);
            IList<Exception> thredExceptions = new List<Exception>();

            TestContext.WriteLine($"Start at: {DateTime.Now.ToLongTimeString()}");
            list.ParallelThreadingInvoke(100, 1, ref thredExceptions, (data) => Action(data, ref counter));
            TestContext.WriteLine($"End at: {DateTime.Now.ToLongTimeString()}");

            Assert.That(counter, Is.GreaterThan(0));
            Assert.That(thredExceptions.Count(), Is.LessThanOrEqualTo(0));
        }

        [Test]
        public void EnumerableExtensionsParallelThreadingSyncInvokeWithExceptionTest()
        {
            var counter = 0;
            var list = Enumerable.Range(1, 10000);
            IList<Exception> thredExceptions = new List<Exception>();

            TestContext.WriteLine($"Start at: {DateTime.Now.ToLongTimeString()}");
            list.ParallelThreadingInvoke(100, 1, ref thredExceptions, (data) =>
            {
                counter++;

                if (counter > 10)
                {
                    throw new Exception("test");
                }
            });

            TestContext.WriteLine($"End at: {DateTime.Now.ToLongTimeString()}");

            Assert.That(counter, Is.GreaterThan(0));
            Assert.That(thredExceptions.Count(), Is.GreaterThanOrEqualTo(1));
        }


        private void Action(IEnumerable<int> obj, ref int counter)
        {
            System.Threading.Thread.Sleep(100);
            counter++;
        }
    }
}
