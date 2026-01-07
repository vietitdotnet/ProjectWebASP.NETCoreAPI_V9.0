using AutoMapper;
using MyApp.Application.Core.Services;
using MyApp.Application.Interfaces;
using MyApp.Application.Models.DTOs.Categorys;
using MyApp.Application.Services.Base;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Exceptions;
using MyApp.Domain.Specifications.Categorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, ILoggerService loggerService, IMapper mapper) : 
            base(unitOfWork, loggerService, mapper)
        {

        }

        public async Task<CategoryDto> GetCategoryBySlugAsync(string slug)
        {
            var spec = new CategoryBySlugSpec(slug);

            var result = await _unitOfWork.Repository<Category>().FirstOrDefaultAsync<CategoryDto>(spec);

           if (result == null) throw new NotFoundException(slug);
                                
           return result;
        }

        public async Task<int> GetCategoryIdBySlugAsync(string slug)
        {

            var spec = new CategoryBySlugSpec(slug);

            var categoryId = await _unitOfWork
                .Repository<Category>()
                .SingleOrDefaultAsyncWithSelectorAsync(spec, x => (int?)x.Id);

            if (!categoryId.HasValue)
                throw new NotFoundException($"không tìm thấy danh mục có slug '{slug}' !");

            return categoryId.Value;
        }
    }
}
