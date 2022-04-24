using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pushinbar.Common.Enums;
using Pushinbar.Common.Models.Eat;
using Pushinbar.Common.Models.Interfaces;
using Pushinbar.KonturMarket.Client;
using Pushinbar.Repositories;

namespace Pushinbar.Services.Products.Eat
{
    public class EatProductsService : IProductsService<EatProduct>
    {
        private KonturMarketClient konturMarketClient { get; set; }
        private EatRepository eatRepository;
        
        public EatProductsService(KonturMarketClient konturMarketClient, EatRepository eatRepository)
        {
            this.konturMarketClient = konturMarketClient;
            this.eatRepository = eatRepository;
        }
        
        public async Task<IEnumerable<EatProduct>> GetAllAsync()
        {
            var products = await konturMarketClient.GetProductsAsync();
            var productsRests = await konturMarketClient.GetProductRestsAsync();
            var productGroups = await konturMarketClient.GetProductGroupsAsync();
            
            var eatProducts = products
                .Where(product => ProductsServiceHelper.IsEat(product.GroupId, productGroups.ToArray()));
            
            var result = new List<EatProduct>();
            foreach (var eatProduct in eatProducts)
            {
                var product = new EatProduct()
                {
                    Id = eatProduct.Id,
                    Name = eatProduct.Name,
                    Photo = null,
                    Description = null,
                    Price = eatProduct.SellPricePerUnit,
                    Type = ProductType.Eat,
                    Rest = ProductsServiceHelper.GetProductRest(eatProduct.Id, productsRests),
                    Status = ProductStatus.New,
                    LikesCount = 0,
                    Barcode = eatProduct.Barcodes?.FirstOrDefault(),
                    Subcategories = null
                };
                result.Add(product);
            }

            return result;
        }

        public Task<EatProduct> GetAsync(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(IUpdateProduct updateProduct)
        {
            throw new System.NotImplementedException();
        }
    }
}