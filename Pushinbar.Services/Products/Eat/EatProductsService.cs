using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pushinbar.Common.Enums;
using Pushinbar.Common.Exstensions;
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

        public async Task<EatProduct> GetAsync(Guid id)
        {
            var foundItemEntity = await eatRepository.GetAsync(id);
            if (foundItemEntity == null)
                return null;
            var item = new EatProduct();
            item.UpdateFromEntity(foundItemEntity);
            var rest = await konturMarketClient.GetProductRestsByIdAsync(foundItemEntity.KonturMarketId);
            if (rest != null)
                item.Rest = rest.Rest;
            return item;
        }

        public async Task<bool> TryUpdateAsync(Guid id, IUpdateProduct updateProduct)
        {
            if (updateProduct is not EatUpdateProduct eatUpdateProduct)
                throw new ArgumentException("UpdateProduct should be is AlcoholUpdateProduct");
            
            var item = await eatRepository.GetAsync(id);
            if (item == null)
                return false;
            
            item.ApplyUpdate(eatUpdateProduct);

            eatRepository.Update(item);
            await eatRepository.SaveAsync();

            return true;
        }
    }
}