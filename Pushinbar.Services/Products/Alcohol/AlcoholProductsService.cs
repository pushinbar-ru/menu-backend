using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pushinbar.Common.Enums;
using Pushinbar.Common.Models.Alcohol;
using Pushinbar.Common.Models.Interfaces;
using Pushinbar.KonturMarket.Client;

namespace Pushinbar.Services.Products.Alcohol
{
    public class AlcoholProductsService : IProductsService<AlcoholProduct>
    {
        private KonturMarketClient konturMarketClient { get; set; }
        
        public AlcoholProductsService(KonturMarketClient konturMarketClient)
        {
            this.konturMarketClient = konturMarketClient;
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

        public Task<bool> UpdateAsync(IUpdateProduct updateProduct)
        {
            throw new System.NotImplementedException();
        }
    }
}