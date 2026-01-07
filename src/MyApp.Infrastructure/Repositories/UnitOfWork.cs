using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Core.Models;
using MyApp.Domain.Core.Repositories;
using MyApp.Infrastructure.Data;

namespace MyApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly MyAppDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDictionary<Type, dynamic> _repositories;

        public UnitOfWork(
            MyAppDbContext dbContext,
            IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
            _repositories = new Dictionary<Type, dynamic>();
        }
         public IBaseRepositoryAsync<T> Repository<T>()
         where T : BaseEntity
        {
            var entityType = typeof(T);

            if (_repositories.TryGetValue(entityType, out var repo))
            {
                return (IBaseRepositoryAsync<T>)repo;
            }

            var repository = _serviceProvider
                .GetRequiredService<IBaseRepositoryAsync<T>>();

            _repositories.Add(entityType, repository);

            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task RollBackChangesAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
    }
}