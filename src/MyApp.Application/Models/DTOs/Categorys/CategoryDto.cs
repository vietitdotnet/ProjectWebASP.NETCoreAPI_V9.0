using MyApp.Domain.Core.Models;
using MyApp.Domain.Entities;


namespace MyApp.Application.Models.DTOs.Categorys
{
    public class CategoryDto : BaseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public int? ParentCategoryID { get; set; }

        public CategoryDto(Category category)
        {
            Id = category.Id;
            Slug = category.Slug;
            Name = category.Name;
            Title = category.Title;
            ParentCategoryID = category.ParentCategoryID;
        }
    }
}
