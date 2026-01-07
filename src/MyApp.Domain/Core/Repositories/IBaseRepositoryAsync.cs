using MyApp.Domain.Core.Models;
using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Parameters;
using System.Linq.Expressions;

namespace MyApp.Domain.Core.Repositories
{
    public interface IBaseRepositoryAsync<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IList<T>> ListAllAsync();
        Task<IList<T>> ListAsync(ISpecification<T> spec);

        Task<T?> FirstOrDefaultAsync(ISpecification<T?> spec);
        Task<T> AddAsync(T entity);
        void Update(T entity);

        void Delete(T entity);

        Task<int> CountAsync(ISpecification<T> spec);

        IQueryable<T> GetQueryableSpecification(ISpecification<T> spec);

        Task<List<TDto>> ListAllAsync<TDto>() where TDto : BaseDto;

        Task<List<TDto>> ListAsync<TDto>(ISpecification<T> spec) where TDto : BaseDto;

        Task<TDto> FirstOrDefaultAsync<TDto>(ISpecification<T> spec) where TDto : BaseDto;


        Task<TResult> SingleOrDefaultAsyncWithSelectorAsync<TResult>(ISpecification<T> spec, Expression<Func<T, TResult>> selector);

        Task<PagedResponse<TDto, TQuery>> GetPagedDataAsync<TDto, TQuery>(ISpecification<T> spec,  TQuery pagingParams)
        where TDto : BaseDto
        where TQuery : PagingParameters;


    }
}
