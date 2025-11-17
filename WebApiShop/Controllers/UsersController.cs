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
        public IEnumerable<User> Get()
        {
            return _userService.GetUsers();
        }

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userService.GetUserById(id);
        }

        // POST api/<Users>
        [HttpPost]
        public ActionResult<User> AddUser([FromBody] User newUser)
        {
            User user = _userService.AddUser(newUser);
            if(user==null)
            {
                return BadRequest(user);
            }
            return CreatedAtAction(nameof(Get), new { Id = user.Id }, user);
        }
        // POST api/<UsersController>
        [HttpPost("{login}")]
        public ActionResult<User> LogIn([FromBody] User LogInUser)
        {
            User user = _userService.LogIn(LogInUser);
            return CreatedAtAction(nameof(Get), new { Id = user.Id }, user);
        }

        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, [FromBody] User updateUser)
        {
            bool flag = _userService.UpdateUser(id, updateUser);
            if (flag)
            {
                return Ok(updateUser);
            }
            return BadRequest(updateUser);
        }

        // DELETE api/<Users>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
