
using MyApp.Domain.Abstractions;
using MyApp.Domain.Core.Models;
using MyApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    public class Order : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Description { set; get; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; } = default;

        public string CreatedByUserId { get; set; }


        // Navigation property trừu tượng
        public virtual IAppUserReference User { get; set; }

        public virtual IList<OrderProduct> OrderProducts { get; set; }

       
        
    }
}
