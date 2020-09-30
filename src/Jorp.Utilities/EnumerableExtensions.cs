using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jorp.Utilities
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Split list into yield parts of chunks
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="chunksize"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
        {
            while (source.Any())
            {
                yield return source.Take(chunksize);
                source = source.Skip(chunksize);
            }
        }


        /// <summary>
        /// Add Value to Enumerable list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<T> Add<T>(this IEnumerable<T> e, T value)
        {

            foreach (var cur in e)
            {
                yield return cur;
            }

            yield return value;
        }

        /// <summary>
        /// Invoke parallel threading processing
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="threadSize"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <param name="action"></param>
        /// <param name="aggregateException"></param>
        public static void ParallelThreadingInvoke<T>(this IEnumerable<T> source, int? threadSize,
            int? maxDegreeOfParallelism, ref AggregateException aggregateException, Action<IEnumerable<T>> action) 
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
                        
            var cancellationTokenSource = new CancellationTokenSource();

            var batches = source
                .ToList()
                .Chunk(threadSize ?? source.ToList().Count())
                .Count();

            var actions = new List<Action>(batches);

            source
                .Select(_ => _)
                .ToList()
                .Chunk(threadSize ?? source.ToList().Count())
                .ToList()
                .ForEach(batch =>
                    actions.Add(() =>
                    {
                        try
                        {
                            action(batch);
                        }
                        catch (Exception e)
                        {
                            cancellationTokenSource.Cancel();
                            throw;
                        }
                    }));

            var parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism ?? 1,
                CancellationToken = cancellationTokenSource.Token
            };

            try
            {
                Parallel.Invoke(parallelOptions, actions.ToArray());
            }
            catch (Exception e)
            {
                if (aggregateException is null) throw;

                aggregateException = new AggregateException("Failed to iterate", e);
            }
        }

        /// <summary>
        /// Invoke parallel threading processing
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="threadSize"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <param name="action"></param>
        /// <param name="aggregateException"></param>
        public static void ParallelThreadingInvoke<T>(this IEnumerable<T> source, int? threadSize,
            int? maxDegreeOfParallelism, bool abortOnError, ref AggregateException aggregateException, Action<IEnumerable<T>> action)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var internalAggregateException = new AggregateException();
            var cancellationTokenSource = new CancellationTokenSource();

            var batches = source
                .ToList()
                .Chunk(threadSize ?? source.ToList().Count())
                .Count();

            var actions = new List<Action>(batches);

            source
                .Select(_ => _)
                .ToList()
                .Chunk(threadSize ?? source.ToList().Count())
                .ToList()
                .ForEach(batch =>
                    actions.Add(() =>
                    {
                        try
                        {
                            action(batch);
                        }
                        catch (Exception e)
                        {
                            
                            if (!abortOnError)
                            {
                                internalAggregateException.InnerExceptions.Add(e);
                                return;
                            }

                            //abort invoke if some of execution fails
                            cancellationTokenSource.Cancel();
                            throw;
                        }
                    }));

            var parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism ?? 1,
                CancellationToken = cancellationTokenSource.Token
            };

            try
            {
                Parallel.Invoke(parallelOptions, actions.ToArray());

                if (abortOnError && internalAggregateException.InnerExceptions.Any())
                    throw new AggregateException(internalAggregateException.InnerExceptions);
                
            }
            catch (Exception e)
            {
                if (aggregateException is null) throw;
                aggregateException = new AggregateException("Failed to iterate", e);
            }
        }
    }
}
