﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pushinbar.KonturMarket.Client.Models;
using Pushinbar.KonturMarket.Client.Responses;

namespace Pushinbar.KonturMarket.Client
{
    public class KonturMarketClient
    {
        private const string ShopUrlTemplate = "https://api.kontur.ru/market/v1/shops/{0}";
        private const string ApiKeyHeaderName = "x-kontur-apikey";
        
        private readonly string apiKey;
        private readonly string shopUrl;


        public KonturMarketClient(string apiKey, string shopId)
        {
            this.apiKey = apiKey;
            shopUrl = string.Format(ShopUrlTemplate, shopId);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var url = shopUrl + "/products";
            var response = await GetAsync<ProductsResponse>(url);
            return response.Items;
        }

        public async Task<IEnumerable<ProductGroup>> GetProductGroupsAsync()
        {
            var url = shopUrl + "/product-groups";
            var response = await GetAsync<ProductGroupsResponse>(url);
            return response.Items;
        }

        public async Task<IEnumerable<ProductRest>> GetProductRestsAsync()
        {
            var url = shopUrl + "/product-rests";
            var response = await GetAsync<ProductRestsResponse>(url);
            return response.Items;
        }
        
        public async Task<ProductRest> GetProductRestsByIdAsync(Guid id)
        {
            var productRests = await GetProductRestsAsync();
            return productRests.FirstOrDefault(x => x.ProductId == id);
        }

        private async Task<T> GetAsync<T>(string url)
        {
            var response = await GetResponseAsync(url,new [] { (ApiKeyHeaderName, apiKey) });

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.StatusDescription);
            }
            
            var streamReader = new StreamReader(response.GetResponseStream());

            var responseContent = await streamReader.ReadToEndAsync();
            var result = JsonConvert.DeserializeObject<T>(responseContent);

            response.Close();

            return result;
        }

        private async Task<HttpWebResponse> GetResponseAsync(string url,
            IEnumerable<(string Name, string Value)> headers = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Name, header.Value);
                }
            }
            var response = await request.GetResponseAsync();
            return (HttpWebResponse)response;
        }
    }
}
