using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pushinbar.Repositories
{
    internal interface IRepository<T> : IDisposable where T : class
    {
        public IEnumerable<T> GetAll();
        public Task<T> GetAsync(int id);
        public Task CreateAsync(T item);
        public void Update(T item);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
    }
}