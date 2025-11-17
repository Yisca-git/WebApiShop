using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Collections.Generic;
using Entities;
using Repositories;
using Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/UsersPassword")]
    [ApiController]
    public class UsersPasswordController : ControllerBase
    {
        private readonly IUserPasswordService _userPasswordService;
        public UsersPasswordController(IUserPasswordService userPasswordService)
        {
            _userPasswordService= userPasswordService;
        }

        // GET: api/<UsersPassword>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersPassword>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersPassword>
        [HttpPost]
        public ActionResult<int> CheckPassword([FromBody] UserPassword userPassword)
        {
            int score = _userPasswordService.CheckPassword(userPassword.Password);
            if (score <= 2)
                return BadRequest(score);
            return Ok(score);
        }

        // PUT api/<UsersPassword>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersPassword>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
