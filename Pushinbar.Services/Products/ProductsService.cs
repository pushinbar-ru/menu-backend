using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pushinbar.Common.Enums;
using Pushinbar.KonturMarket.Client.Models;
using Pushinbar.Common.Models;
using Pushinbar.KonturMarket.Client;

namespace Pushinbar.Services.Products
{
    public class ProductsService : IProductsService
    {
        private KonturMarketClient konturMarketClient { get; set; }
        
        public ProductsService(KonturMarketClient konturMarketClient)
        {
            this.konturMarketClient = konturMarketClient;
        }

        public async Task<IEnumerable<AlcoholProduct>> GetAlcoholProductsAsync()
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

        public async Task<IEnumerable<NotAlcoholProduct>> GetNotAlcoholProductsAsync()
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

        public async Task<IEnumerable<EatProduct>> GetEatProductsAsync()
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
                    Barcode = eatProduct.Barcodes?.FirstOrDefault()
                };
                result.Add(product);
            }

            return result;
        }

        public async Task<IEnumerable<SnackProduct>> GetSnackProductsAsync()
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
                    Barcode = snackProduct.Barcodes?.FirstOrDefault()
                };
                result.Add(product);
            }

            return result;
        }
    }
}