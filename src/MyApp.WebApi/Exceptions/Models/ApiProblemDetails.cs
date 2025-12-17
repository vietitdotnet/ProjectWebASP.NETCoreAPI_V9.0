using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace MyApp.WebApi.Exceptions.Models
{
    public class ApiProblemDetails : ProblemDetails
    {
        public bool Success { get; set; } = false;
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
        public string UrlRedirect { get; set; }
        public string TraceId { get; set; }
    }
}
