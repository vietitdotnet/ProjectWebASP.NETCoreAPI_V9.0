using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Specifications.Base;
using MyApp.Domain.Core;
using MyApp.Domain.Core.Models;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Parameters;
using MyApp.Infrastructure.Data;
using System.Linq.Expressions;

namespace MyApp.Infrastructure.Repositories
{
    public class BaseRepositoryAsync<T> : IBaseRepositoryAsync<T> where T : BaseEntity
    {
        protected readonly MyAppDbContext _dbContext;
        protected readonly IConfigurationProvider _mapperConfig;

        public BaseRepositoryAsync(MyAppDbContext dbContext, IConfigurationProvider mapperConfig)
        {
            _dbContext = dbContext;
            _mapperConfig = mapperConfig;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }

        public IQueryable<T> GetQueryableSpecification(ISpecification<T> spec)
        {
            return ApplySpecification(spec);
        }

        public async Task<PagedResponse<TDto, TQuery>> GetPagedDataAsync<TDto, TQuery>(
            ISpecification<T> spec,
            TQuery pagingParams)
            where TDto : BaseDto
            where TQuery : PagingParameters
        {
            var query = ApplySpecification(spec);
            var totalCount = await query.CountAsync();

            var items = await query
                .ProjectTo<TDto>(_mapperConfig)
                .ToListAsync();

            return new PagedResponse<TDto, TQuery>(items, totalCount, pagingParams.PageIndex, pagingParams.PageSize)
            {
                Query = pagingParams
            };
        }


        public async Task<List<TDto>> ListAllAsync<TDto>() where TDto : BaseDto
        {
            return await _dbContext.Set<T>()
                .ProjectTo<TDto>(_mapperConfig)
                .ToListAsync();
        }


        public async Task<List<TDto>> ListAsync<TDto>(ISpecification<T> spec) where TDto : BaseDto
        {
            return await ApplySpecification(spec)
                .ProjectTo<TDto>(_mapperConfig)
                .ToListAsync();
        }


        public async Task<TDto> FirstOrDefaultAsync<TDto>(ISpecification<T> spec) where TDto : BaseDto
        {
            return await ApplySpecification(spec)
                .ProjectTo<TDto>(_mapperConfig)
                .FirstOrDefaultAsync();
        }

        public async Task<TResult> SingleOrDefaultAsyncWithSelectorAsync<TResult>(ISpecification<T> spec, Expression<Func<T
            , TResult>> selector)
        {
                return await ApplySpecification(spec)
                    .Select(selector)
                    .SingleOrDefaultAsync();
        }


    }
}
