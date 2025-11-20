using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Repositories;
using Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            List<User> users = _userService.GetUsers();
            if (users.Count == 0)
                return NoContent();
            return Ok(users);
        }

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST api/<Users>
        [HttpPost]
        public ActionResult<User> AddUser([FromBody] User newUser)
        {
            User user = _userService.AddUser(newUser);
            if(user == null)
            {
                return BadRequest("Password is not strong enough");
            }
            return CreatedAtAction(nameof(Get), new { Id = user.Id }, user);
        }
        // POST api/<UsersController>
        [HttpPost("login")]
        public ActionResult<User> LogIn([FromBody] User loginUser)
        {
            User user = _userService.LogIn(loginUser);
            if (user == null)
                return Unauthorized();
            return Ok(user);
        }

        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updateUser)
        {
            bool isUpdateSuccessful = _userService.UpdateUser(id, updateUser);
            if (isUpdateSuccessful)
            {
                return NoContent();
            }
            return BadRequest("Password is not strong enough");
        }

        // DELETE api/<Users>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
