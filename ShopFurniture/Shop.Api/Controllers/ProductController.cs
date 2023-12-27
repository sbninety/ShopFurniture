using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Dto;
using Shop.Application.Services;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetAll());
        }
        [Authorize]
        [HttpGet("GetLastProduct")]
        public IActionResult GetLastProduct()
        {
            var products = _productService.GetAll();
            return Ok(products[products.Count-1]);
        }
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productService.Get(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddProduct(ProductDto product)
        {
            if(_productService.Add(product))
            {
                return NoContent();
            }
            return Ok("Product đã tồn tại");
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(ProductDto product)
        {
            if (_productService.Update(product))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if(_productService.Delete(id)) 
            { 
                return NoContent();
            }
            return NotFound("Không thể xóa vì product không tồn tại");
        }
    }
}
