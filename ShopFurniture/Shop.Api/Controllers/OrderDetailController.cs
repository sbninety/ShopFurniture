using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Dto;
using Shop.Application.Services;

namespace Shop.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        [HttpGet]
        public IActionResult GetOrderDetails()
        {
            return Ok(_orderDetailService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetOrderDetail(int id)
        {
            var orderDetail = _orderDetailService.Get(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return Ok(orderDetail);
        }
        [HttpPost]
        public IActionResult AddOrderDetail(OrderDetailDto orderDetail)
        {
            if (_orderDetailService.Add(orderDetail))
            {
                return NoContent();
            }
            return Ok("OrderDetail đã tồn tại");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateOrderDetail(OrderDetailDto orderDetail)
        {
            if (_orderDetailService.Update(orderDetail))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetail(int id)
        {
            if (_orderDetailService.Delete(id))
            {
                return NoContent();
            }
            return NotFound("Không thể xóa vì OrderDetail không tồn tại");
        }
    }
}
