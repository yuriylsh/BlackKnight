using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperSample.Syncing
{
    public class SyncDebug
    {
        public List<string> InitializeList(IEnumerable<string> items)
        {
            var bag = new ConcurrentBag<string>();
            Parallel.ForEachAsync(items, async (i, _) =>
            {
                var r = await Task.Run(() => i).ConfigureAwait(false);
                bag.Add(r);
            }).Wait();  // typically we don't want to do Wait(), much prefer to make InitializeList async and await this call.
            var list = bag.ToList();
            return list;
        }

        public Dictionary<int, string> InitializeDictionary(Func<int, string> getItem)
        {
            var degreeOfParallelism = 3;
            var itemsToInitialize = Enumerable.Range(0, 100).ToList();
            var chunkSize = Math.DivRem(itemsToInitialize.Count, degreeOfParallelism, out var reminder);
            if (reminder > 0) chunkSize += 1;
            var chunks = itemsToInitialize.Chunk(chunkSize);

            var concurrentDictionary = new ConcurrentDictionary<int, string>();
            var threads = chunks
                .Select(itemsInChunk => new Thread(() => {
                    foreach (var item in itemsInChunk)
                    {
                        concurrentDictionary.AddOrUpdate(item, getItem, (_, s) => s);
                    }
                }))
                .ToList();

            foreach (var thread in threads)
            {
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }

            return concurrentDictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}