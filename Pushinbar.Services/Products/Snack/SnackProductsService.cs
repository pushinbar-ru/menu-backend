using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pushinbar.Common.Entities;
using Pushinbar.Common.Enums;
using Pushinbar.Common.Exstensions;
using Pushinbar.Common.Models.Eat;
using Pushinbar.Common.Models.Interfaces;
using Pushinbar.Common.Models.Snack;
using Pushinbar.KonturMarket.Client;
using Pushinbar.Repositories;

namespace Pushinbar.Services.Products.Snack
{
    public class SnackProductsService : IProductsService<SnackProduct>
    {
        private KonturMarketClient konturMarketClient { get; set; }
        private SnackRepository snackRepository;
        
        public SnackProductsService(KonturMarketClient konturMarketClient, SnackRepository snackRepository)
        {
            this.konturMarketClient = konturMarketClient;
            this.snackRepository = snackRepository;
        }

        public async Task<IEnumerable<SnackProduct>> GetAllAsync()
        {
            var products = await konturMarketClient.GetProductsAsync();
            var productsRests = await konturMarketClient.GetProductRestsAsync();
            var productGroups = await konturMarketClient.GetProductGroupsAsync();
            
            var snackProducts = products
                .Where(product => ProductsServiceHelper.IsSnack(product.GroupId, productGroups.ToArray()));
            
            var result = new List<SnackProduct>();
            var snackEntities = (await snackRepository.GetAll()).ToArray();
            foreach (var snackProduct in snackProducts)
            {
                var productEntity = snackEntities.FirstOrDefault(x => x.KonturMarketId == snackProduct.Id);
                if (productEntity == null)
                {
                    productEntity = new SnackEntity()
                    {
                        Id = Guid.NewGuid(),
                        KonturMarketId = snackProduct.Id,
                        Name = snackProduct.Name,
                        Photo = null,
                        Description = null,
                        Price = snackProduct.SellPricePerUnit,
                        Type = ProductType.Eat,
                        Status = ProductStatus.New,
                        LikesCount = 0,
                        Barcode = snackProduct.Barcodes?.FirstOrDefault(),
                        Subcategories = ""
                    };
                    await snackRepository.CreateAsync(productEntity);
                }

                var product = new SnackProduct()
                {
                    Rest = ProductsServiceHelper.GetProductRest(snackProduct.Id, productsRests)
                };
                product.UpdateFromEntity(productEntity);
                
                result.Add(product);
            }

            return result;
        }

        public async Task<SnackProduct> GetAsync(Guid id)
        {
            var foundItemEntity = await snackRepository.GetAsync(id);
            if (foundItemEntity == null)
                return null;
            var item = new SnackProduct();
            item.UpdateFromEntity(foundItemEntity);
            var rest = await konturMarketClient.GetProductRestsByIdAsync(foundItemEntity.KonturMarketId);
            if (rest != null)
                item.Rest = rest.Rest;
            return item;
        }

        public async Task<bool> TryUpdateAsync(Guid id, IUpdateProduct updateProduct)
        {
            if (updateProduct is not SnackUpdateProduct alcoholUpdateProduct)
                throw new ArgumentException("UpdateProduct should be is AlcoholUpdateProduct");
            
            var item = await snackRepository.GetAsync(id);
            if (item == null)
                return false;
            
            item.ApplyUpdate(alcoholUpdateProduct);

            await snackRepository.Update(item);

            return true;
        }
    }
}