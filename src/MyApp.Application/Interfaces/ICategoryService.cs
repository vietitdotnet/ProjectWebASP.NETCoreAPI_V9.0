using MyApp.Application.Models.DTOs.Categorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetCategoryBySlugAsync(string slug);
        Task<int> GetCategoryIdBySlugAsync(string slug);

    }
}
