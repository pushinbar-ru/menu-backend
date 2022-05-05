using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pushinbar.Common.Entities;
using Pushinbar.Common.Enums;
using Pushinbar.Common.Exstensions;
using Pushinbar.Common.Models.Alcohol;
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
            var notAlcoholEntities = notAlcoholRepository.GetAll().ToArray();
            foreach (var notAlcoholProduct in notAlcoholProducts)
            {
                var productEntity = notAlcoholEntities.FirstOrDefault(x => x.KonturMarketId == notAlcoholProduct.Id);
                if (productEntity == null)
                {
                    productEntity = new NotAlcoholEntity()
                    {
                        Id = Guid.NewGuid(),
                        KonturMarketId = notAlcoholProduct.Id,
                        Name = notAlcoholProduct.Name,
                        Photo = null,
                        Description = null,
                        Price = notAlcoholProduct.SellPricePerUnit,
                        Type = ProductType.Alcohol,
                        Status = ProductStatus.New,
                        LikesCount = 0,
                        Barcode = notAlcoholProduct.Barcodes?.FirstOrDefault(),
                        Subcategories = null,
                        Volume = null
                    };
                    await notAlcoholRepository.CreateAsync(productEntity);
                    await notAlcoholRepository.SaveAsync();
                }

                var product = new NotAlcoholProduct()
                {
                    Rest = ProductsServiceHelper.GetProductRest(notAlcoholProduct.Id, productsRests)
                };
                product.UpdateFromEntity(productEntity);
                
                result.Add(product);
            }

            return result;
        }

        public async Task<NotAlcoholProduct> GetAsync(Guid id)
        {
            var foundItemEntity = await notAlcoholRepository.GetAsync(id);
            if (foundItemEntity == null)
                return null;
            var item = new NotAlcoholProduct();
            item.UpdateFromEntity(foundItemEntity);
            var rest = await konturMarketClient.GetProductRestsByIdAsync(foundItemEntity.KonturMarketId);
            if (rest != null)
                item.Rest = rest.Rest;
            return item;
        }

        public async Task<bool> TryUpdateAsync(Guid id, IUpdateProduct updateProduct)
        {
            if (updateProduct is not NotAlcoholUpdateProduct alcoholUpdateProduct)
                throw new ArgumentException("UpdateProduct should be is AlcoholUpdateProduct");
            
            var item = await notAlcoholRepository.GetAsync(id);
            if (item == null)
                return false;
            
            item.ApplyUpdate(alcoholUpdateProduct);

            notAlcoholRepository.Update(item);
            await notAlcoholRepository.SaveAsync();

            return true;
        }
    }
}