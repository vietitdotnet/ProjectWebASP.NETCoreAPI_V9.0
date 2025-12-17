using AutoMapper;
using MyApp.Application.Core.Services;
using MyApp.Application.Interfaces;
using MyApp.Application.Models.DTOs.Products;
using MyApp.Application.Models.Requests.Products;
using MyApp.Application.Models.Responses.Products;
using MyApp.Application.Services.Base;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Exceptions;
using MyApp.Domain.Exceptions.APIRoutes;
using MyApp.Domain.Exceptions.CodeErrors;
using MyApp.Domain.Specifications;

namespace MyApp.Application.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, 
            ILoggerService loggerService, 
            IMapper mapper) 
            : base(unitOfWork, loggerService, mapper)
        {

        }

        public async Task<CreateProductRes> CreateProductAsync(CreateProductRep req)
        {

            var dataMap = _mapper.Map<Product>(req);

            var result = await _unitOfWork.Repository<Product>().AddAsync(dataMap);

            await _unitOfWork.SaveChangesAsync();

            return new CreateProductRes()
            {
                Data = new ProductDto(result)
            };
        }

        public async Task<DeleteProductRes> DeleteProductAsync(int id)
        {
            var spec = ProductSpecifications.GetProdcutByIdSpec(id);
            var result = await _unitOfWork.Repository<Product>().FirstOrDefaultAsync(spec);

            if (result == null) throw new NotFoundException();

            _unitOfWork.Repository<Product>().Delete(result);

            await _unitOfWork.SaveChangesAsync();

            return new DeleteProductRes() { Data = new ProductDto(result) };
        }

        public async Task<GetProductsRes> GetProductsAsync()
        {
            var result = await _unitOfWork.Repository<Product>().ListAllAsync();

            _logger.LogInfo("Danh sách product");
            return new GetProductsRes()
            {
                Data = _mapper.Map<IList<ProductDto>>(result)
            };

        }

        public async Task<GetProductRes> GetProductByIdAsync(int id)
        {
            var spec = ProductSpecifications.GetProdcutByIdSpec(id);
            var result = await _unitOfWork.Repository<Product>().FirstOrDefaultAsync(spec);

            if (result == null)
                throw new NotFoundException($"Không tìm thấy ID");

            return new GetProductRes()
            {
                Data = new ProductDto(result),
            };
        }

        public async Task<UpdateProductRes> UpdateProductAsync(int id, UpdateProductRep req)
        {
            var spec = ProductSpecifications.GetProdcutByIdSpec(id);

            var result = await _unitOfWork.Repository<Product>().FirstOrDefaultAsync(spec);


            if (result == null)
                throw new RedirectRequestException("Chưa Xác thực Email" , RedirectRequest.ConfirmedEmail, RedirectCodes.EmailNotConfirmed);


            _mapper.Map(req, result);

            _unitOfWork.Repository<Product>().Update(result);

            await _unitOfWork.SaveChangesAsync();

            return new UpdateProductRes() { Data = new ProductDto(result) };

        }
    }
}
