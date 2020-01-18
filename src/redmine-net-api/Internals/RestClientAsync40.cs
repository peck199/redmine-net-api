#if NET40
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Redmine.Net.Api.Serialization;

namespace Redmine.Net.Api.Internals
{
    internal sealed partial class RestClient : IAsyncRestClient
    {

        public Task CreateAsync<T>(T data) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync<T>(T data) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CountAsync<T>() where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedResults<TOut>> GetPagedResultAsync<TOut>() where TOut : class
        {
            throw new System.NotImplementedException();
        }

        public Task<List<TOut>> GetAllAsync<TOut>() where TOut : class
        {
            throw new System.NotImplementedException();
        }
    }
}
#endif