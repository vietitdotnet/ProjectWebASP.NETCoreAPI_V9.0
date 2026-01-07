using MyApp.Domain.Core.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [DataType(DataType.Text)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }


}
