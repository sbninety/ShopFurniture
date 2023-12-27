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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_orderService.GetAll());
        }
        [HttpGet("GetLastOrder")]
        public IActionResult GetLastOrder()
        {
            var orders = _orderService.GetAll();
            return Ok(orders[orders.Count - 1]);
        }
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _orderService.Get(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpPost]
        public IActionResult AddOrder(OrderDto order)
        {
            if (_orderService.Add(order))
            {
                return NoContent();
            }
            return Ok("Order đã tồn tại");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(OrderDto order)
        {
            if (_orderService.Update(order))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            if (_orderService.Delete(id))
            {
                return NoContent();
            }
            return NotFound("Không thể xóa vì Order không tồn tại");
        }
    }
}
