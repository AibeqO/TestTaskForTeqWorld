using ClosedXML.Excel;
using System.Collections.Generic;
using TestTask.Models;

namespace TestTask.Services
{
    public class ExcelService
    {
        public List<Product> ReadProductsFromExcel(string filePath)
        {
            var products = new List<Product>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed();
                foreach (var row in rows.Skip(1))
                {
                    var product = new Product
                    {
                        Name = row.Cell(1).GetString(),
                        Unit = row.Cell(2).GetString(),
                        Price = row.Cell(3).GetValue<decimal>(),
                        Quantity = row.Cell(4).GetValue<int>()
                    };
                    products.Add(product);
                }
            }
            return products;
        }
    }
}
