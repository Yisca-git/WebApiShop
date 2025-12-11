using Microsoft.AspNetCore.Mvc;
using Entities;
using Repositories;
using Services;
using Entities.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
    
        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            OrderDTO order = await _orderService.GetOrderById(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> AddOrder([FromBody] Order order)

        {
            OrderDTO? _order = await _orderService.AddOrder(order);
            if (_order == null)
            {
                return BadRequest("Password is not strong enough");
            }
            return CreatedAtAction(nameof(GetOrderById), new { Id = _order.OrderId }, _order); 
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
