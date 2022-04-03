using System.Collections.Generic;
using System.Threading.Tasks;
using Pushinbar.Common.DTOs;
using Pushinbar.Common.Models;

namespace Pushinbar.Services.Products
{
    public interface IProductsService
    {
        public Task<IEnumerable<AlcoholProduct>> GetAlcoholProductsAsync();
        public Task<IEnumerable<NotAlcoholProduct>> GetNotAlcoholProductsAsync();
        public Task<IEnumerable<EatProduct>> GetEatProductsAsync();
        public Task<IEnumerable<SnackProduct>> GetSnackProductsAsync();
    }
}