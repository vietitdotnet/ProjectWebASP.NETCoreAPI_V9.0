using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyApp.Infrastructure.Test.Repositories
{
    public class UnitOfWorkTests : InfrastructureTestBase
    {
        [Fact]
        public void Repository_Should_Not_Be_Null()
        {
            var repo = _unitOfWork.Repository<Product>();

            Assert.NotNull(repo);
        }
    }
}
