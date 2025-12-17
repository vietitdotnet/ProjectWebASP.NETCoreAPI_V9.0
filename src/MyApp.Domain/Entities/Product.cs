using MyApp.Domain.Core.Models;

using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class Product : BaseEntity
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }


}
