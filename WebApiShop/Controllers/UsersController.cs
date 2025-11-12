using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Repositories;
using Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserService userService = new();

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return userService.GetUsers();
        }

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return userService.GetUserById(id);
        }

        // POST api/<Users>
        [HttpPost]
        public ActionResult<User> AddUser([FromBody] User user)
        {
            return userService.AddUser(user);
        }
        // POST api/<UsersController>
        [HttpPost("{login}")]
        public ActionResult<User> LogIn([FromBody] User LogInUser)
        {
            return userService.LogIn(LogInUser);
        }

        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public void UpdateUser(int id, [FromBody] User updateUser)
        {
            userService.UpdateUser(id, updateUser);
        }

        // DELETE api/<Users>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
