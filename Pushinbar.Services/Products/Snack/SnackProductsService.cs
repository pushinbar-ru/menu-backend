using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pushinbar.Common.Enums;
using Pushinbar.Common.Models.Interfaces;
using Pushinbar.Common.Models.Snack;
using Pushinbar.KonturMarket.Client;

namespace Pushinbar.Services.Products.Snack
{
    public class SnackProductsService : IProductsService<SnackProduct>
    {
        private KonturMarketClient konturMarketClient { get; set; }
        
        public SnackProductsService(KonturMarketClient konturMarketClient)
        {
            this.konturMarketClient = konturMarketClient;
        }

        public async Task<IEnumerable<SnackProduct>> GetAllAsync()
        {
            var products = await konturMarketClient.GetProductsAsync();
            var productsRests = await konturMarketClient.GetProductRestsAsync();
            var productGroups = await konturMarketClient.GetProductGroupsAsync();
            
            var snackProducts = products
                .Where(product => ProductsServiceHelper.IsSnack(product.GroupId, productGroups.ToArray()));
            
            var result = new List<SnackProduct>();
            foreach (var snackProduct in snackProducts)
            {
                var product = new SnackProduct()
                {
                    Id = snackProduct.Id,
                    Name = snackProduct.Name,
                    Photo = null,
                    Description = null,
                    Price = snackProduct.SellPricePerUnit,
                    Type = ProductType.Snack,
                    Rest = ProductsServiceHelper.GetProductRest(snackProduct.Id, productsRests),
                    Status = ProductStatus.New,
                    LikesCount = 0,
                    Barcode = snackProduct.Barcodes?.FirstOrDefault(),
                    Subcategories = null
                };
                result.Add(product);
            }

            return result;
        }

        public Task<SnackProduct> GetAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(IUpdateProduct updateProduct)
        {
            throw new System.NotImplementedException();
        }
    }
}