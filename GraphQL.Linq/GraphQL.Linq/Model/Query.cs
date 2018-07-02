using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace GraphQL.Linq.Model
{
    public class Query
    {
        private static readonly Random _rnd = new Random();
        private static readonly List<Product> _products = new List<Product>
        {
            new Product("Product-A", "Category-1", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-B", "Category-2", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-C", "Category-3", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-D", "Category-1", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-E", "Category-1", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-F", "Category-2", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-G", "Category-2", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-H", "Category-2", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-I", "Category-3", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-J", "Category-3", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-K", "Category-3", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-L", "Category-3", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-M", "Category-4", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-N", "Category-4", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-O", "Category-2", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-P", "Category-3", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-Q", "Category-17", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-R", "Category-4", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-S", "Category-1", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-T", "Category-2", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-U", "Category-3", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-V", "Category-3", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-X", "Category-4", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-Y", "Category-2", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-Z", "Category-4", (decimal)_rnd.NextDouble() * 100),
            new Product("Product-W", "Category-1", (decimal)_rnd.NextDouble() * 100),
        };

        public Query()
        {
            Products = new Products(_products);
        }

        public Products Products { get; }
    }

    public class Products : IEnumerable<Product>
    {
        private readonly IEnumerable<Product> _products;

        public Products(IEnumerable<Product> products)
        {
            _products = products;
        }

        public IEnumerator<Product> GetEnumerator()
        {
            return _products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Products Where(string exp)
        {
            return new Products(_products.AsQueryable().Where(exp));
        }

        public Products OrderBy(string exp)
        {
            return new Products(_products.AsQueryable().OrderBy(exp));
        }

        public List<Product> Select(int start = -1, int count = -1)
        {
            if (start == -1)
                start = 0;

            if (count == -1)
                count = _products.Count();

            return _products.Skip(start).Take(count).ToList();
        }
    }

    public class Product
    {
        public Product(string name, string category, decimal price)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Category = category;
            Price = price;
        }

        public string Id { get; }
        public string Name { get; }
        public string Category { get; }
        public decimal Price { get; }
    }
}
