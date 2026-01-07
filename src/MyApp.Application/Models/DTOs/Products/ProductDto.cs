using MyApp.Domain.Core.Models;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Models.DTOs.Products
{
    public class ProductDto : BaseDto
    {
        public int Id { get; set; }

        public string Slug { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ProductDto(Product product)
        {
            Id = product.Id;
            Slug = product.Slug;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
        }
    }
}
