using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Models
{
    public interface IDataStore<T> : IDisposable
    {
        Task AddItemAsync(T item);
        Task UpdateItemAsync(T item);
        Task DeleteItemAsync(T item);
        Task<T> GetItemAsync(long item);
        Task<List<T>> GetItemsAsync();
        Task DeleteAllAsync();
    }
}
