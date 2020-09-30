using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Jorp.Utilities.Tests
{
    [TestFixture]
    public class EnumerableExtensionTests
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
            var aggregateException = new AggregateException();


            TestContext.WriteLine($"Start at: {DateTime.Now.ToLongTimeString()}");
            list.ParallelThreadingInvoke(100, 10, ref aggregateException, (data)=>  Action(data, ref counter));
            TestContext.WriteLine($"End at: {DateTime.Now.ToLongTimeString()}");

            Assert.That(counter, Is.GreaterThan(0));
            Assert.That(aggregateException.InnerExceptions.Count(), Is.LessThanOrEqualTo(0));
        }


        [Test]
        public void EnumerableExtensionsParallelThreadingAsyncInvokeWithExceptionTest()
        {
            var counter = 0;
            var list = Enumerable.Range(1, 10);
            var abortOnError = true;
            var aggregateException = new AggregateException();

            TestContext.WriteLine($"Start at: {DateTime.Now.ToLongTimeString()}");
            list.ParallelThreadingInvoke(1, 2, abortOnError, ref aggregateException, (data) => ActionWithExceptionItiration(data, ref counter));

            TestContext.WriteLine($"End at: {DateTime.Now.ToLongTimeString()}");

            Assert.That(counter, Is.GreaterThan(0));
            Assert.That(aggregateException.InnerExceptions.Count(), Is.GreaterThan(0));
        }


        [Test]
        public void EnumerableExtensionsParallelThreadingSyncInvokeTest()
        {
            var counter = 0;
            var list = Enumerable.Range(1, 10000);
            var aggregateException = new AggregateException();

            TestContext.WriteLine($"Start at: {DateTime.Now.ToLongTimeString()}");
            list.ParallelThreadingInvoke(100, 1, ref aggregateException, (data) => Action(data, ref counter));
            TestContext.WriteLine($"End at: {DateTime.Now.ToLongTimeString()}");

            Assert.That(counter, Is.GreaterThan(0));
            Assert.That(aggregateException.InnerExceptions.Count(), Is.LessThanOrEqualTo(0));
        }

        [Test]
        public void EnumerableExtensionsParallelThreadingSyncInvokeWithExceptionTest()
        {
            var counter = 0;
            var list = Enumerable.Range(1, 10000);
            var thredExceptions = new AggregateException();

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
            Assert.That(thredExceptions.InnerExceptions.Count(), Is.GreaterThanOrEqualTo(1));
        }




        private void Action(IEnumerable<int> obj, ref int counter)
        {
            System.Threading.Thread.Sleep(100);
            counter++;
        }

        private void ActionWithExceptionItiration(IEnumerable<int> obj, ref int counter)
        {
            System.Threading.Thread.Sleep(100);

            if (counter == 5)
            {
                throw new ExecutionEngineException("abort process");
            }

            counter++;
        }
    }
}
