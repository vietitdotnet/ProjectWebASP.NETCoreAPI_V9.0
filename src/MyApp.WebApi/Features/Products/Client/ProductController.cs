
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces;
using MyApp.Application.Models.Requests.Products;
using MyApp.Application.Models.Responses.Products;


namespace MyApp.WebApi.Features.Products.Client
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<GetProductsRes> GetProducts()
            => await _productService.GetProductsAsync();


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GetProductRes> GetProductById(int id)
            => await _productService.GetProductByIdAsync(id);


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateProductRes>> CreateProduct([FromBody] CreateProductRep req)
        {
            var result = await _productService.CreateProductAsync(req);

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = result.Data.Id },
                result
            );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UpdateProductRes>> UpdateProduct(int id, [FromBody] UpdateProductRep req)
        {
            var result = await _productService.UpdateProductAsync(id, req);
        
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            
            return NoContent();
        }



    }

}
