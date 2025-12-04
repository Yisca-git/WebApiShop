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
        public async Task<IEnumerable<User>> Get()
        {
            IEnumerable<User> users = await  _userService.GetUsers();
            return users;
        }

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST api/<Users>
        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User newUser)
        {
            User? user = await _userService.AddUser(newUser);
            if(user == null)
            {
                return BadRequest("Password is not strong enough");
            }
            return CreatedAtAction(nameof(Get), new { Id = user.UserId }, user);
        }
        // POST api/<UsersController>
        [HttpPost("login")]
        public async Task<ActionResult<User>> LogIn([FromBody] User loginUser)
        {
            User user = await _userService.LogIn(loginUser);
            if (user == null)
                return Unauthorized("שם משתמש או סיסמא שגויים");
            return Ok(user);
        }

        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updateUser)
        {
            bool isUpdateSuccessful = await _userService.UpdateUser(id, updateUser);
            if (isUpdateSuccessful)
            {
                User? updatedUserFromDb = await _userService.GetUserById(id);
                return Ok(updatedUserFromDb);
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
