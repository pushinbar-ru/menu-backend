using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pushinbar.Common.Entities;
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
            var alcoholEntities = (await alcoholRepository.GetAll()).ToArray();
            foreach (var alcoholProduct in alcoholProducts)
            {
                var productEntity = alcoholEntities.FirstOrDefault(x => x.KonturMarketId == alcoholProduct.Id);
                if (productEntity == null)
                {
                    productEntity = new AlcoholEntity()
                    {
                        Id = Guid.NewGuid(),
                        KonturMarketId = alcoholProduct.Id,
                        Name = alcoholProduct.Name,
                        Photo = null,
                        Description = null,
                        Price = alcoholProduct.SellPricePerUnit,
                        Type = ProductType.Alcohol,
                        Status = ProductStatus.New,
                        LikesCount = 0,
                        Barcode = alcoholProduct.Barcodes?.FirstOrDefault(),
                        Subcategories = "",
                        Alc = null,
                        IBU = null,
                        UntappdUrl = null,
                        Brewery = null,
                        Volume = null
                    };
                    await alcoholRepository.CreateAsync(productEntity);
                }

                var product = new AlcoholProduct()
                {
                    Rest = ProductsServiceHelper.GetProductRest(alcoholProduct.Id, productsRests)
                };
                product.UpdateFromEntity(productEntity);
                
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

        public async Task<bool> TryUpdateAsync(Guid id, IUpdateProduct updateProduct)
        {
            if (updateProduct is not AlcoholUpdateProduct alcoholUpdateProduct)
                throw new ArgumentException("UpdateProduct should be is AlcoholUpdateProduct");
            
            var item = await alcoholRepository.GetAsync(id);
            if (item == null)
                return false;
            
            item.ApplyUpdate(alcoholUpdateProduct);

            // или без await тут и в других продутках?
            await alcoholRepository.Update(item);

            return true;
        }
    }
}