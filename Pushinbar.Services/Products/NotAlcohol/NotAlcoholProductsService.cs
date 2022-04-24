using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pushinbar.Common.Enums;
using Pushinbar.Common.Models.Interfaces;
using Pushinbar.Common.Models.NotAlcohol;
using Pushinbar.KonturMarket.Client;
using Pushinbar.Repositories;

namespace Pushinbar.Services.Products.NotAlcohol
{
    public class NotAlcoholProductsService : IProductsService<NotAlcoholProduct>
    {
        private KonturMarketClient konturMarketClient { get; set; }
        private NotAlcoholRepository notAlcoholRepository;

        public NotAlcoholProductsService(KonturMarketClient konturMarketClient, NotAlcoholRepository notAlcoholRepository)
        {
            this.konturMarketClient = konturMarketClient;
            this.notAlcoholRepository = notAlcoholRepository;
        }
        
        public async Task<IEnumerable<NotAlcoholProduct>> GetAllAsync()
        {
            var products = await konturMarketClient.GetProductsAsync();
            var productsRests = await konturMarketClient.GetProductRestsAsync();
            var productGroups = await konturMarketClient.GetProductGroupsAsync();
            
            var notAlcoholProducts = products
                .Where(product => ProductsServiceHelper.IsNotAlcohol(product.GroupId, productGroups.ToArray()));
            
            var result = new List<NotAlcoholProduct>();
            foreach (var notAlcoholProduct in notAlcoholProducts)
            {
                var product = new NotAlcoholProduct()
                {
                    Id = notAlcoholProduct.Id,
                    Name = notAlcoholProduct.Name,
                    Photo = null,
                    Description = null,
                    Price = notAlcoholProduct.SellPricePerUnit,
                    Type = ProductType.NotAlcohol,
                    Rest = ProductsServiceHelper.GetProductRest(notAlcoholProduct.Id, productsRests),
                    Status = ProductStatus.New,
                    LikesCount = 0,
                    Barcode = notAlcoholProduct.Barcodes?.FirstOrDefault(),
                    Subcategories = null,
                    Volume = null
                };
                result.Add(product);
            }

            return result;
        }

        public Task<NotAlcoholProduct> GetAsync(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(IUpdateProduct updateProduct)
        {
            throw new System.NotImplementedException();
        }
    }
}