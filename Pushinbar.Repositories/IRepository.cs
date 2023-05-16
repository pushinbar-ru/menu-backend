using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pushinbar.Common.Entities;

namespace Pushinbar.Repositories
{
    internal interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetAsync(Guid id);
        public Task<T> CreateAsync(T item);
        public Task Update(T item);
    }
}