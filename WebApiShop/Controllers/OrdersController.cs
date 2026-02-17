using Microsoft.AspNetCore.Mvc;
using Entities;
using Repositories;
using Services;
using DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public OrdersController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> GetAllOrders()
        {
          List<OrderDTO> orders = await _orderService.GetAllOrders();
          if(orders == null || orders.Count == 0)
              return NoContent();
          return Ok(orders);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            OrderDTO order = await _orderService.GetOrderById(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpGet("date")]
        public async Task<ActionResult<List<OrderDTO>>> GetUnpackedOrdersUntilDate(DateOnly date)
        {
            if (!_orderService.CheckDate(date))
                return BadRequest("Date must be in the future");
            List<OrderDTO> orders = await _orderService.GetUnpackedOrdersUntilDate(date);
            if (orders == null || orders.Count == 0)
                return NoContent();
            return Ok(orders);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<OrderDTO>>> GetOrdersByUserId(int userId)
        {
            if(await _userService.GetUserById(userId) == null)
                return NotFound("User not found");
            List<OrderDTO> orders = await _orderService.GetOrdersByUserId(userId);
            if (orders == null || orders.Count == 0)
                return NoContent();
            return Ok(orders);
        }


        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> AddOrder([FromBody] NewOrderDTO newOrder)
        {
            if (!_orderService.CheckDate(newOrder.EventDate))
                return BadRequest("Event date must be in the future");
            if (!_orderService.CheckDate(newOrder.OrderDate, newOrder.EventDate))
                return BadRequest("Invalid order date and event date");
            if (!await _orderService.CheckFinalPrice(newOrder))
                return BadRequest("Final price is not correct");
            bool isValidOrder = await _orderService.CheckOrderItems(newOrder);
            if (!isValidOrder)
                return BadRequest("One or more dresses are not available for the event date");
            OrderDTO _order = await _orderService.AddOrder(newOrder);
            return CreatedAtAction(nameof(GetOrderById), new { Id = _order.Id }, _order); 
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(OrderDTO updateOrder, int id)
        {
            if (id != updateOrder.Id)
                return BadRequest("Id in the path and body do not match");
            if (!_orderService.CheckDate(updateOrder.EventDate))
                return BadRequest("Event date must be in the future");
            if (!_orderService.CheckDate(updateOrder.OrderDate, updateOrder.EventDate))
                return BadRequest("In valid order date and event date");
            if (!await _orderService.CheckFinalPrice(updateOrder))
                return BadRequest("Final price is not correct");
            bool isValidOrder = await _orderService.CheckOrderItems(updateOrder);
            if (!isValidOrder)
                return BadRequest("One or more dresses are not available for the event date");
            await _orderService.UpdateOrder(updateOrder);
            return Ok(updateOrder);
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatusOrder(OrderDTO upStsOrder, int statusId)
        {
            if (!_orderService.CheckStatus(statusId))
                return BadRequest("Invalid status id");
            if (!await _orderService.IsExistsOrderById(upStsOrder.Id))
                return NotFound();
            await _orderService.UpdateStatusOrder(upStsOrder, statusId);
            return Ok(upStsOrder);
        }


    }
}
