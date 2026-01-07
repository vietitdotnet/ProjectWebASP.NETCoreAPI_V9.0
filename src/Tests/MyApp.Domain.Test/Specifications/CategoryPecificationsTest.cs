using MyApp.Domain.Entities;
using MyApp.Domain.Specifications.Categorys;
using MyApp.Domain.Test.Helpers;
using MyApp.Domain.Test.TestData;
using Xunit;

namespace MyApp.Domain.Test.Specifications
{
    public class CategorySpecificationsTest
    {
        public CategorySpecificationsTest() { }

        /// <summary>
        /// Arrange – Act – Assert (AAA pattern)
        /// </summary>
        /// 
        [Fact]
        public void CategoryBySlugSpec_Should_Return_Category_With_Matching_Slug()
        {
            // Arrange
            var categories = CategoryTestData.Categories;
            var spec = new CategoryBySlugSpec("dien-thoai");

            // Act
            var result = SpecificationEvaluatorTestHelper<Category>
                .GetQuery(categories.AsQueryable(), spec)
                .SingleOrDefault();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result?.Id);
            Assert.Equal("dien-thoai", result?.Slug);
        }


    }
}
