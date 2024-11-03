using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly ExcelService _excelService;

        public IndexModel(ProductService productService, ExcelService excelService)
        {
            _productService = productService;
            _excelService = excelService;
        }

        public List<Product> Products { get; set; }
        public List<List<Product>> ProductGroups { get; set; }

        public void OnGet()
        {
            var products = _excelService.ReadProductsFromExcel("C:\\Users\\zjhb\\OneDrive - Chevron\\Desktop\\dataFile.xlsx");
            Products = products.ToList();
            _productService.SaveProducts(products);
        }

        public void OnPostGroupProducts()
        {
            Products = _productService.GetAllProducts();
            ProductGroups = _productService.GroupProducts(Products);
        }
    }
}
