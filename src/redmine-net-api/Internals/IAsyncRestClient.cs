#if !NET20

using System.Collections.Generic;
using System.Threading.Tasks;
using Redmine.Net.Api.Serialization;
#endif

namespace Redmine.Net.Api.Internals
{
    internal interface IAsyncRestClient
    {
#if NET20
#else
        Task CreateAsync<T>(T data) where T: class;
        Task UpdateAsync<T>(T data) where T: class;
        Task DeleteAsync(int id);
        Task<int> CountAsync<T>() where T: class;
        Task<PagedResults<TOut>> GetPagedResultAsync<TOut>() where TOut: class;
        Task<List<TOut>> GetAllAsync<TOut>() where TOut : class;
#endif
    }
}