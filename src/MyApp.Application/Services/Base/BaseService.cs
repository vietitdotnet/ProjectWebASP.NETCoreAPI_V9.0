using AutoMapper;
using MyApp.Application.Core.Services;
using MyApp.Domain.Core.Repositories;


namespace MyApp.Application.Services.Base
{
    public class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILoggerService _logger;
        protected readonly IMapper _mapper;
        public BaseService(IUnitOfWork unitOfWork, ILoggerService loggerService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = loggerService;
            _mapper = mapper;
            
        }
    }
}
