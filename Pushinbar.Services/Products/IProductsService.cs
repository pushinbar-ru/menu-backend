using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pushinbar.Common.DTOs;
using Pushinbar.Common.Models;
using Pushinbar.Common.Models.Interfaces;

namespace Pushinbar.Services.Products
{
    public interface IProductsService<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetAsync(Guid id);
        public Task<bool> TryUpdateAsync(Guid id, IUpdateProduct updateProduct);
    }
}