using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces;

namespace MyApp.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _productService.GetProductsAsync();
            return View(model.ToString());
        }
    }
}
