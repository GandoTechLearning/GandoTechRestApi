using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppToUseApi
{
    public class ProductService
    {
        private readonly HttpClient _client;
        public ProductService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await HttpClientHelper.GetAsync<List<Product>>(_client, $"{_client.BaseAddress}");
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await HttpClientHelper.GetAsync<Product>(_client, $"{_client.BaseAddress}/{id}");
        }

        public async Task<HttpResponseMessage> AddProductAsync(Product product)
        {
            var productContent = HttpClientHelper.GetContent(product);
            //new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            return await _client.PostAsync($"{_client.BaseAddress}", productContent);
        }

        public async Task<HttpResponseMessage> InsertProductAsync(int id, Product product)
        {
            var productContent = HttpClientHelper.GetContent(product);
            //new StringContent(JsonConvert.SerializeObject(product));
            return await _client.PostAsync($"{_client.BaseAddress}/{id}", productContent);
        }

        public async Task<HttpResponseMessage> UpdateProductAsync(int id, Product product)
        {
            var productContent = HttpClientHelper.GetContent(product);
            //new StringContent(JsonConvert.SerializeObject(product));
            return await _client.PutAsync($"{_client.BaseAddress}/{id}", productContent);
        }

        public async Task<HttpResponseMessage> DeleteProductAsync(int id)
        {
            return await _client.DeleteAsync($"{_client.BaseAddress}/{id}");
        }
    }

}
