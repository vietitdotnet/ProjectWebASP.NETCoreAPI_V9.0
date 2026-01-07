using MyApp.Domain.Entities;

namespace MyApp.Domain.Test.TestData
{
    public static class ProductTestData
    {
        public static IReadOnlyList<Product> Products => new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "iPhone 15",
                CategoryId = 1,
                Category = CategoryTestData.Phone,
                Price = 30000
            },
            new Product
            {
                Id = 2,
                Name = "iPhone 14",
                CategoryId = 1,
                Category = CategoryTestData.Phone,
                Price = 20000
            },
            new Product
            {
                Id = 3,
                Name = "Samsung S24",
                CategoryId = 1,
                Category = CategoryTestData.Phone,
                Price = 25000
            },
            new Product
            {
                Id = 4,
                Name = "Macbook Pro",
                CategoryId = 2,
                Category = CategoryTestData.Laptop,
                Price = 50000
            },
            new Product
            {
                Id = 5,
                Name = "SenCo",
                CategoryId = 3,
                Category = CategoryTestData.Fan,
                Price = 90000
            }
        };
    }   

}
