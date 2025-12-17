
using Microsoft.AspNetCore.Mvc;
using MyApp.WebApi.Exceptions.Models;

namespace MyApp.WebApi.Features.Errors.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Lỗi 404 - Không tìm thấy tài nguyên
        /// </summary>
        [HttpGet("not-found")]
        public IActionResult GetNotFound()
        {
            var problem = new ApiProblemDetails
            {
                Title = "Resource Not Found",
                Status = StatusCodes.Status404NotFound,
                ErrorCode = "NOT_FOUND",
                ErrorMessage = "Tài nguyên bạn yêu cầu không tồn tại hoặc đã bị xóa.",
                TraceId = HttpContext.TraceIdentifier
            };

            return StatusCode(problem.Status.Value, problem);
        }

        /// <summary>
        /// Lỗi 400 - Yêu cầu không hợp lệ
        /// </summary>
        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            var problem = new ApiProblemDetails
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest,
                ErrorCode = "BAD_REQUEST",
                ErrorMessage = "Dữ liệu gửi lên không hợp lệ hoặc thiếu thông tin cần thiết.",
                TraceId = HttpContext.TraceIdentifier
            };

            return StatusCode(problem.Status.Value, problem);
        }

        /// <summary>
        /// Lỗi 500 - Database Error
        /// </summary>
        [HttpGet("database-error")]
        public IActionResult GetDatabaseError()
        {
            var problem = new ApiProblemDetails
            {
                Title = "Database Error",
                Status = StatusCodes.Status500InternalServerError,
                ErrorCode = "DATABASE_ERROR",
                ErrorMessage = "Đã xảy ra lỗi trong quá trình truy xuất cơ sở dữ liệu.",
                TraceId = HttpContext.TraceIdentifier
            };

            return StatusCode(problem.Status.Value, problem);
        }

        /// <summary>
        /// Lỗi chung không xác định (fallback)
        /// </summary>
        [HttpGet("unknown")]
        public IActionResult GetUnknownError()
        {
            var problem = new ApiProblemDetails
            {
                Title = "Unexpected Error",
                Status = StatusCodes.Status500InternalServerError,
                ErrorCode = "INTERNAL_SERVER_ERROR",
                ErrorMessage = "Lỗi hệ thống, vui lòng thử lại sau.",
                TraceId = HttpContext.TraceIdentifier
            };

            return StatusCode(problem.Status.Value, problem);
        }
    }
}
