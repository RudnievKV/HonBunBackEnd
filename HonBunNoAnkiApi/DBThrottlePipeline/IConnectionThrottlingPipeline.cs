using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.DBThrottlePipeline
{
    public interface IConnectionThrottlingPipeline
    {
        public Task<T> AddRequest<T>(Func<Task<T>> task);
        public Task<IEnumerable<T>> AddRequestList<T>(IEnumerable<Func<Task<T>>> funcList);
    }
}