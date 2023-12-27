using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Dto;
using Shop.Application.Services;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _productImageService;
        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }
        [HttpGet]
        public IActionResult GetProductImages()
        {
            return Ok(_productImageService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetProductImage(int id)
        {
            var productImage = _productImageService.Get(id);
            if(productImage == null)
            {
                return NotFound();
            }
            return Ok(productImage);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddProductImage(ProductImageDto productImage)
        {
            if (_productImageService.Add(productImage))
            {
                return NoContent();
            }
            return Ok("ProductImage đã tồn tại");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProductImage(ProductImageDto productImage)
        {
            if(_productImageService.Update(productImage))
            {
                return NoContent();
            }
            return NotFound();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteProductImage(int id)
        {
            if (_productImageService.Delete(id))
            {
                return NoContent();
            }
            return NotFound("Không thể xóa vì ProductImage không tồn tại");
        }
    }
}
