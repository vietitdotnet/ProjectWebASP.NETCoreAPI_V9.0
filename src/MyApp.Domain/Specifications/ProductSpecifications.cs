using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Entities;

namespace MyApp.Domain.Specifications
{
    public static class ProductSpecifications
    {
        public static BaseSpecification<Product> GetProdcutByIdSpec(int id)
        {
            return new BaseSpecification<Product>(x => x.Id == id);
        }


    }
}
