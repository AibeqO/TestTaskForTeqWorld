using System.Collections.Generic;
using System.Linq;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void SaveProducts(List<Product> products)
        {
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public List<List<Product>> GroupProducts(List<Product> products)
        {
            List<List<Product>> groups = new List<List<Product>>();
            List<Product> currentGroup = new List<Product>();
            decimal currentGroupPrice = 0;
            decimal maxGroupPrice = 200;

            foreach (var product in products.OrderByDescending(p => p.Price))
            {
                for (int i = 0; i < product.Quantity; i++)
                {
                    if (currentGroupPrice + product.Price > maxGroupPrice)
                    {
                        groups.Add(new List<Product>(currentGroup));
                        currentGroup.Clear();
                        currentGroupPrice = 0;
                    }

                    var existingProduct = currentGroup.FirstOrDefault(p => p.Name == product.Name && p.Unit == product.Unit && p.Price == product.Price);
                    if (existingProduct != null)
                    {
                        existingProduct.Quantity++;
                    }
                    else
                    {
                        currentGroup.Add(new Product
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Unit = product.Unit,
                            Price = product.Price,
                            Quantity = 1
                        });
                    }
                    currentGroupPrice += product.Price;
                }
            }
            if (currentGroup.Count > 0)
            {
                groups.Add(currentGroup);
            }

            return groups;
        }

    }
}
