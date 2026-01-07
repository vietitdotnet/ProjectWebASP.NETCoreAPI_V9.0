using MyApp.Application.Specifications.Products;
using MyApp.Domain.Entities;
using MyApp.Domain.Parameters;
using MyApp.Domain.Specifications.Products;
using MyApp.Domain.Test.Helpers;
using MyApp.Domain.Test.TestData;
using Xunit;

namespace MyApp.Domain.Test.Specifications
{
    public class ProductPecificationsTest
    {

        public ProductPecificationsTest()
        {
        }

        /// <summary>
        /// Arrange – Act – Assert (AAA pattern)
        /// </summary>
        [Fact]
        public void ProdcutsByIdCategoryWithPageSpec_Should_Filter_And_Sort_By_Price_Asc()
        {
            // Arrange
            var products = ProductTestData.Products;

            var parameters = new ProductParameters
            {
                PriceFrom = 20000,
                PriceTo = 30000,
                Sort = new("price_desc"),

            };
            parameters.Normalize();

            var spec = new ProdcutsByIdCategoryWithPageSpec(1, parameters);

            // Act
            var result = SpecificationEvaluatorTestHelper<Product>
                .GetQuery(products.AsQueryable(), spec)
                .ToList();

            // Assert - filter
            Assert.Equal(3, result.Count);
            Assert.All(result, x => Assert.Equal(1, x.CategoryId));
            Assert.All(result, x => Assert.InRange(x.Price, 20000, 30000));

            // Assert - sort
            var prices = result.Select(x => x.Price).ToList();
            Assert.True(prices.SequenceEqual(prices.OrderByDescending(x => x)));
        }

        [Fact]
        public void ProdcutsBySlugCategoryWithPageSpec_Should_Filter_And_Sort_By_Price_Asc()
        {
            // Arrange
            var products = ProductTestData.Products;

            var parameters = new ProductParameters
            {
                PriceFrom = 20000,
                PriceTo = 30000,
                Sort = new("price_desc"),

            };
            parameters.Normalize();

            var spec = new ProdcutsBySlugCategoryWithPageSpec("dien-thoai", parameters);

            // Act
            var result = SpecificationEvaluatorTestHelper<Product>
                .GetQuery(products.AsQueryable(), spec)
                .ToList();

            // Assert - filter
            Assert.Equal(3, result.Count);
            Assert.All(result, x => Assert.Equal("dien-thoai", x.Category.Slug));
            Assert.All(result, x => Assert.InRange(x.Price, 20000, 30000));

            // Assert - sort
            var prices = result.Select(x => x.Price).ToList();
            Assert.True(prices.SequenceEqual(prices.OrderByDescending(x => x)));
        }
    }
}
