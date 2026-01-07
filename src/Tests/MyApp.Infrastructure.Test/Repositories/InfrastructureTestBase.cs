using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Repositories;
using Xunit;

namespace MyApp.Infrastructure.Test.Repositories
{
    public abstract class InfrastructureTestBase : IDisposable
    {
        protected readonly MyAppDbContext _dbContext;
        protected readonly UnitOfWork _unitOfWork;
        protected readonly IServiceProvider _serviceProvider;

        protected InfrastructureTestBase()
        {
            var services = new ServiceCollection();

            // DbContext
            services.AddDbContext<MyAppDbContext>(options =>
                options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            // Register repositories (generic)
            services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
            
            services.AddAutoMapper(
              typeof(ApplicationAssemblyMarker).Assembly

            );
            // UnitOfWork
            services.AddScoped<UnitOfWork>();

            _serviceProvider = services.BuildServiceProvider();

            _dbContext = _serviceProvider.GetRequiredService<MyAppDbContext>();
            _unitOfWork = _serviceProvider.GetRequiredService<UnitOfWork>();
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

    }
}