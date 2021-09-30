using IdentityModel.Client;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AppToUseApi
{
    class Program
    {
        private static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            // request token
            var requestClient = new HttpClient();
            var tokenResponse = await requestClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:44395/connect/token",
                ClientId = "Inventory",
                ClientSecret = "MyClientSecret",
                Scope = "inventoryapi.write"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            client.BaseAddress = new Uri("https://localhost:44360/api/product");
            client.SetBearerToken(tokenResponse.AccessToken);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var productService = new ProductService(client);

            var products = await productService.GetProductsAsync();

            if (products == null)
                Console.WriteLine($"You don't have enough permission to fetch product.");
            else
            {
                foreach (var product in products)
                {
                    Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
                }
            }

            var product1 = await productService.GetProductAsync(5);

            if (product1 == null)
                Console.WriteLine($"You don't have enough permission to fetch product.");
            else
            {
                Console.WriteLine($"\n\nId: {product1.Id}, Name: {product1.Name}, Price: {product1.Price}, Quantity: {product1.Quantity}");
            }

            var newProduct = new Product()
            {
                Id = 6,
                Name = "Product 8",
                Price = 550,
                Quantity = 4
            };

            var result = await productService.AddProductAsync(newProduct);
            if (result.StatusCode == HttpStatusCode.Forbidden)
                Console.WriteLine($"You don't have enough permission to add product.");
            else
                Console.WriteLine($"{result.Content.ReadAsStringAsync().Result}");

            var updateProduct = new Product()
            {
                Id = 3,
                Name = "Product 3 - Updated",
                Price = 350,
                Quantity = 9
            };

            result = await productService.UpdateProductAsync(3, updateProduct);
            if (result.StatusCode == HttpStatusCode.Forbidden)
                Console.WriteLine($"You don't have enough permission to update product.");
            Console.WriteLine($"{result.Content.ReadAsStringAsync().Result}");

            result = await productService.DeleteProductAsync(4);
            if (result.StatusCode == HttpStatusCode.Forbidden)
                Console.WriteLine($"You don't have enough permission to delete product.");
            Console.WriteLine($"{result.Content.ReadAsStringAsync().Result}");

            Console.WriteLine("Hello World!");
        }
    }
}
