using MyApp.Application.Specifications.Products;
using MyApp.Domain.Extentions;
using MyApp.Domain.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Specifications.Products
{
    public sealed class ProdcutsBySlugCategoryWithPageSpec : ProductFilterSpec
    {
        public ProdcutsBySlugCategoryWithPageSpec(string slugCategory, ProductParameters p) : base(p)
        {
            Criteria = Criteria.And(x => x.Category.Slug == slugCategory);
           
        }
    }
}
