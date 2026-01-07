using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Test.TestData
{
    public static class CategoryTestData
    {
        public static IReadOnlyList<Category> Categories => new List<Category>()
        {
            Phone,
            Laptop,
            Fan
        };

        public static Category Phone => new() { Id = 1, Slug = "dien-thoai" };
        public static Category Laptop => new() { Id = 2, Slug = "laptop" };
        public static Category Fan => new() { Id = 3, Slug = "may-quat" };
    }
}
