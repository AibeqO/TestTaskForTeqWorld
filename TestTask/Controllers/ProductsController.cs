using Microsoft.AspNetCore.Mvc;
using TestTask.Models;
using TestTask.Services;
using System.Collections.Generic;

namespace TestTask.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        private readonly ExcelService _excelService;

        public ProductsController(ProductService productService, ExcelService excelService)
        {
            _productService = productService;
            _excelService = excelService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string filepath = "C:\\Users\\zjhb\\OneDrive - Chevron\\Desktop\\dataFile.xlsx";
            var products = _excelService.ReadProductsFromExcel(filepath);
            _productService.SaveProducts(products);
            return View(products);
        }

        [HttpPost]
        public IActionResult GroupProducts()
        {
            var products = _productService.GetAllProducts();
            var productGroups = _productService.GroupProducts(products);
            return View(productGroups);
        }
    }
}
