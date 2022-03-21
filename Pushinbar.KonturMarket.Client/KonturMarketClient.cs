using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using log4net;
using log4net.Core;
using Newtonsoft.Json;
using Pushinbar.Common.KonturMarket;
using Pushinbar.KonturMarket.Client.Responses;

namespace Pushinbar.KonturMarket.Client
{
    public class KonturMarketClient
    {
        private const string ShopUrlTemplate = "https://api.kontur.ru/market/v1/shops/{0}";
        private const string ApiKeyHeaderName = "x-kontur-apikey";
        
        private readonly string apiKey;
        private readonly string shopUrl;
        private readonly ILog log;


        public KonturMarketClient(string apiKey, string shopId, ILogger logger, ILog log)
        {
            this.apiKey = apiKey;
            shopUrl = string.Format(ShopUrlTemplate, shopId);
            this.log = log;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            try
            {
                var url = shopUrl + "/products";
                var response = await GetAsync<ProductsResponse>(url);
                return response.Items;
            }
            catch (Exception ex)
            {
                log.Error("Get products failed.", ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProductGroupDto>> GetProductGroupsAsync()
        {
            try
            {
                var url = shopUrl + "/product-groups";
                var response = await GetAsync<ProductGroupsResponse>(url);
                return response.Items;
            }
            catch (Exception ex)
            {
                log.Error("Get product groups failed.", ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProductRestDto>> GetProductRestsAsync()
        {
            try
            {
                var url = shopUrl + "/product-rests";
                var response = await GetAsync<ProductRestsResponse>(url);
                return response.Items;
            }
            catch (Exception ex)
            {
                log.Error("Get product rests failed.", ex);
                throw;
            }
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
