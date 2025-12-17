using AutoMapper;

namespace MyApp.Application.Mappings
{

    /// <summary>
    /// Base class cho mọi Profile mapping.
    /// Giúp chuẩn hóa cấu trúc Manager / Request / Response.
    /// </summary>
    public abstract class BaseProfile : Profile
    {
        protected BaseProfile()
        {

            ConfigureRequestsMapping();
            ConfigureResponsesMapping();
        }

        /// <summary>
        /// Mapping cho dữ liệu request từ client (Create/Update/...).
        /// </summary>
        protected virtual void ConfigureRequestsMapping() { }

        /// <summary>
        /// Mapping cho dữ liệu response trả về client (chi tiết / danh sách).
        /// </summary>
        protected virtual void ConfigureResponsesMapping() { }
    }
}

