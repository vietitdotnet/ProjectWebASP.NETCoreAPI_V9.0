using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Entities;
using MyApp.Domain.Extentions;
using MyApp.Domain.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Specifications.Categorys
{
    public sealed class CategoryBySlugSpec : BaseSpecification<Category>
    {    
        public CategoryBySlugSpec(string slug) : base()
        {
            Criteria = x => x.Slug == slug;

        }
    }
}
