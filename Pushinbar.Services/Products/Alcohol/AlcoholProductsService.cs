using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pushinbar.Common.Enums;
using Pushinbar.Common.Exstensions;
using Pushinbar.Common.Models.Alcohol;
using Pushinbar.Common.Models.Interfaces;
using Pushinbar.KonturMarket.Client;
using Pushinbar.Repositories;

namespace Pushinbar.Services.Products.Alcohol
{
    public class AlcoholProductsService : IProductsService<AlcoholProduct>
    {
        private KonturMarketClient konturMarketClient { get; set; }
        private AlcoholRepository alcoholRepository;
        
        public AlcoholProductsService(KonturMarketClient konturMarketClient, AlcoholRepository alcoholRepository)
        {
            this.konturMarketClient = konturMarketClient;
            this.alcoholRepository = alcoholRepository;
        }
        
        public async Task<IEnumerable<AlcoholProduct>> GetAllAsync()
        {
            var products = await konturMarketClient.GetProductsAsync();
            var productsRests = await konturMarketClient.GetProductRestsAsync();
            var productGroups = await konturMarketClient.GetProductGroupsAsync();
            
            var alcoholProducts = products
                .Where(product => ProductsServiceHelper.IsAlcohol(product.GroupId, productGroups.ToArray()));
            
            var result = new List<AlcoholProduct>();
            foreach (var alcoholProduct in alcoholProducts)
            {
                var product = new AlcoholProduct()
                {
                    Id = alcoholProduct.Id,
                    Name = alcoholProduct.Name,
                    Photo = null,
                    Description = null,
                    Price = alcoholProduct.SellPricePerUnit,
                    Type = ProductType.Alcohol,
                    Rest = ProductsServiceHelper.GetProductRest(alcoholProduct.Id, productsRests),
                    Status = ProductStatus.New,
                    LikesCount = 0,
                    Barcode = alcoholProduct.Barcodes?.FirstOrDefault(),
                    Alc = null,
                    IBU = null,
                    Subcategories = null,
                    UntappdUrl = null,
                    Brewery = null,
                    Volume = null
                };
                result.Add(product);
            }

            return result;
        }

        public async Task<AlcoholProduct> GetAsync(Guid id)
        {
            var foundItemEntity = await alcoholRepository.GetAsync(id);
            if (foundItemEntity == null)
                return null;
            var item = new AlcoholProduct();
            item.UpdateFromEntity(foundItemEntity);
            var rest = await konturMarketClient.GetProductRestsByIdAsync(foundItemEntity.KonturMarketId);
            if (rest != null)
                item.Rest = rest.Rest;
            return item;
        }

        public Task<bool> UpdateAsync(IUpdateProduct updateProduct)
        {
            throw new System.NotImplementedException();
        }
    }
}