using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using DTOs;
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
        private readonly IUserPasswordService _userPasswordService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService,IUserPasswordService userPasswordService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _userPasswordService = userPasswordService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            List<UserDTO> users = await _userService.GetUsers();
            if(users.Count() == 0)
            {
                return NoContent();
            }
            return Ok(users);
        }

        // GET api/<Users>/5
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
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] UserRegisterDTO newUser)
        {
            int score = _userPasswordService.CheckPassword(newUser.Password);
            if(score < 2)
            {
                return BadRequest("Password is not strong enough");
            }
            UserDTO? user = await _userService.AddUser(newUser);
            return CreatedAtAction(nameof(GetUserById), new { Id = user.Id }, user);
        }
        // POST api/<UsersController>
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginDTO>> LogIn([FromBody] UserLoginDTO loginUser)
        {
            UserDTO user = await _userService.LogIn(loginUser);
            if (user == null)
                return Unauthorized("Name or password is wrong");
            else
            {
               _logger.LogInformation("User logged in: " + JsonSerializer.Serialize(user.FirstName));
                return Ok(user);
            }
        }
                

        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO updateUser)
        {
            int score = _userPasswordService.CheckPassword(updateUser.Password);
            if (score < 2)
            {
                return BadRequest("Password is not strong enough");
            }
            if(_userService.GetUserById(id) == null)
            {
                return NotFound();
            }
            await _userService.UpdateUser(id, updateUser);
            return Ok(updateUser);
        }

    }
}
