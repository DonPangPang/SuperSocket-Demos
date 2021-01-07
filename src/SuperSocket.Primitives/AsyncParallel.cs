using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SuperSocket
{
    public static class AsyncParallel
    {
        /// <summary>
        /// ������
        /// </summary>
        /// <typeparam name="TItem">��������</typeparam>
        /// <param name="source">����Դ</param>
        /// <param name="operation">����</param>
        /// <param name="maxDegreeOfParallelism">�����</param>
        /// <returns></returns>
        public static async Task ForEach<TItem>(IEnumerable<TItem> source, Func<TItem, Task> operation, int maxDegreeOfParallelism = 5)
        {
            await ForEach(source, operation, new ParallelOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism,
                CancellationToken = CancellationToken.None
            });
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <typeparam name="TItem">��������</typeparam>
        /// <param name="source">����Դ</param>
        /// <param name="operation">����</param>
        /// <param name="parallelOptions">��������</param>
        /// <returns></returns>
        public static async Task ForEach<TItem>(IEnumerable<TItem> source, Func<TItem, Task> operation, ParallelOptions parallelOptions)
        {
            var allTasks = new List<Task>();
            var throttler = new SemaphoreSlim(initialCount: parallelOptions.MaxDegreeOfParallelism);

            foreach (var item in source)
            {
                await throttler.WaitAsync(parallelOptions.CancellationToken);

                if (parallelOptions.CancellationToken.IsCancellationRequested)
                    break;

                allTasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        await operation(item);
                    }
                    finally
                    {
                        throttler.Release();
                    }
                }, parallelOptions.CancellationToken));
            }

            await Task.WhenAll(allTasks);
        }
    }
}