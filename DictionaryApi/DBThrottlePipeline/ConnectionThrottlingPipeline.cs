using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DictionaryApi.DBContext
{
    public class ConnectionThrottlingPipeline : IConnectionThrottlingPipeline
    {
        private readonly SemaphoreSlim openConnectionSemaphore;
        private int i = 0;
        public ConnectionThrottlingPipeline(MongoClient client)
        {
            //Only grabbing half the available connections to hedge against collisions.
            //If you send every operation through here
            //you should be able to use the entire connection pool.
            //openConnectionSemaphore = new SemaphoreSlim(100);
            openConnectionSemaphore = new SemaphoreSlim(client.Settings.MaxConnectionPoolSize / 10);
        }

        public async Task<IEnumerable<T>> AddRequestList<T>(IEnumerable<Func<Task<T>>> funcList)
        {
            var taskList = new List<Task<T>>();

            foreach (var func in funcList)
            {

                var funcRes = AddRequest(func);
                taskList.Add(funcRes);

            }
            var res = await Task.WhenAll(taskList);
            var test = new List<T>(res);


            i = 0;
            return test;
        }
        public async Task<T> AddRequest<T>(Func<Task<T>> task)
        {
            System.Diagnostics.Debug.WriteLine("Before entering " + openConnectionSemaphore.CurrentCount);
            await openConnectionSemaphore.WaitAsync();
            try
            {
                System.Diagnostics.Debug.WriteLine("After entering " + openConnectionSemaphore.CurrentCount);

                var result = await task();
                Interlocked.Increment(ref i);
                System.Diagnostics.Debug.WriteLine("i = " + i);
                return result;
            }
            finally
            {
                openConnectionSemaphore.Release();
                System.Diagnostics.Debug.WriteLine("After releasing " + openConnectionSemaphore.CurrentCount);
            }
        }
    }
}
