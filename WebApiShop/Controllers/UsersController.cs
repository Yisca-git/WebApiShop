using Microsoft.AspNetCore.Mvc;
using Entities;
using Entities.DTOs;
using Services;

namespace WebApiShop.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            IEnumerable<UserDTO> users = await _userService.GetUsers();
            if (users.Count() == 0)
            {
                return NoContent();
            }
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            UserDTO user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST api/<Users>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] User newUser)
        {
            UserDTO? user = await _userService.AddUser(newUser);
            if(user == null)
            {
                return BadRequest("Password is not strong enough");
            }
            return CreatedAtAction(nameof(GetUserById), new { Id = user.Id }, user);
        }
        // POST api/<UsersController>
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginDTO>> LogIn([FromBody] User loginUser)
        {
            UserLoginDTO user = await _userService.LogIn(loginUser);
            if (user == null)
                return Unauthorized("Invalid username or password");
            else
            {
               _logger.LogInformation("User logged in: " + JsonSerializer.Serialize(user));
              
                return Ok(user);
            }
        }
                

        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updateUser)
        {
            bool isUpdateSuccessful = await _userService.UpdateUser(id, updateUser);
            if (isUpdateSuccessful)
            {
                UserDTO? updatedUserFromDb = await _userService.GetUserById(id);
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
