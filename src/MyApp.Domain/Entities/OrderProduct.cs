using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Domain.Core.Models;

namespace MyApp.Domain.Entities
{
    public class OrderProduct : BaseEntity
    {
        public int OrderId { get; set; }

        [ForeignKey("OderId")]
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
