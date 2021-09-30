using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Domain.DataService
{
    public class ProductService
    {
        private List<Product> products = new List<Product>()
        {
                new Product
                {
                    Id= 1,
                    Name= "Product 1",
                    Price = 1,
                    Quantity = 100
                },
                new Product
                {
                    Id= 2,
                    Name= "Product 2",
                    Price = 1,
                    Quantity = 200
                },
                new Product
                {
                    Id= 3,
                    Name= "Product 3",
                    Price = 1,
                    Quantity = 300
                },
                new Product
                {
                    Id= 4,
                    Name= "Product 4",
                    Price = 1,
                    Quantity = 400
                },
                new Product
                {
                    Id= 5,
                    Name= "Product 5",
                    Price = 1,
                    Quantity = 500
                },
        };

        public async Task<bool> AddProductAsync(Product product)
        {
            if (product == null ||
                products.Any(x => x.Id.Equals(product.Id))) return false;

            products.Add(product);
            return true;
        }

        public async Task<bool> InsertProductAsync(int index, Product product)
        {
            if (product == null ||
                products.Any(x => x.Id.Equals(product.Id))) return false;

            products.Insert(index, product);
            return true;
        }

        public async Task<bool> UpdateProductAsync(int id, Product productTo)
        {
            var productIndex = await GetProductIndexAsync(id);
            if (productIndex == -1) return false;

            products[productIndex] = productTo;

            return true;
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var productIndex = await GetProductIndexAsync(id);
            if (productIndex == -1) return false;

            products.RemoveAt(productIndex);

            return true;
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return products.Where(x => x.Id.Equals(productId)).SingleOrDefault();
        }

        public async Task<int> GetProductIndexAsync(int productId)
        {
            return products.IndexOf(products.Find(x => x.Id.Equals(productId)));
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return products;
        }
    }
}
