#if !(NET20 || NET40)

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

        public async Task CreateAsync<T>(T data) where T : class
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateAsync<T>(T data) where T : class
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> CountAsync<T>() where T : class
        {
            throw new System.NotImplementedException();
        }

        public async Task<PagedResults<TOut>> GetPagedResultAsync<TOut>() where TOut : class
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<TOut>> GetAllAsync<TOut>() where TOut : class
        {
            throw new System.NotImplementedException();
        }
    }
}

#endif