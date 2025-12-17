using MyApp.Application.ModelValidation;

using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.Models.Requests.Products
{
    public class UpdateProductRep
    {
        [Required]
        public string Name { get; set; }

        [SlugValidation]
        [Required]
        public string Slug { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
