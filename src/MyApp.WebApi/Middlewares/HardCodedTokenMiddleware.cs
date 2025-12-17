using System.Runtime.CompilerServices;

namespace MyApp.WebApi.Middlewares
{
    public class HardCodedTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _hardcodedToken;

        public HardCodedTokenMiddleware(RequestDelegate next)
        {
            _next = next;
            // Token test bạn muốn dùng
            _hardcodedToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkZjZiZmVmMS04MTEwLTQzMjgtODMzZS0wNzVjMjAxYTkyMGMiLCJqdGkiOiI5MTVkOTk0MS1jYjQzLTQ4NWUtOTYxMy04NjEwNGZmMzI1ZTIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ2aWV0OTljbUBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImRmNmJmZWYxLTgxMTAtNDMyOC04MzNlLTA3NWMyMDFhOTIwYyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2dpdmVubmFtZSI6IiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3N1cm5hbWUiOiIiLCJfVG9rZW5WZXJzaW9uIjoiMyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzY0MTYzOTcyLCJpc3MiOiJNeUFwcC5BcGkiLCJhdWQiOiJNeUFwcC5DbGllbnQifQ.KnnZi06E70V7i1p9PI1Jv32zOrx2Uga3tDf73RxZY04";
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Nếu chưa có Authorization header
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                 context.Request.Headers.Add("Authorization", $"Bearer {_hardcodedToken}");
            }
            await _next(context);
        }
    }
}
