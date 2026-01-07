using MyApp.Domain.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }

        
        public string Name { get; set; }
    
        public string Slug { get; set; }

        public string Title { get; set; }

        public int? ParentCategoryID { get; set; }

        public virtual Category ParentCategory { set; get; }

        [DataType(DataType.Text)]
        public string Description { set; get; }

        public virtual ICollection<Category> CategoryChildrens { get; set; }

        public virtual ICollection<Product> Products { get; set; }


    }
}
