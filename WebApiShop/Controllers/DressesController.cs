using DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DressesController : ControllerBase
    {
        private readonly IDressService _dressService;

        public DressesController(IDressService dressService)
        {
            _dressService = dressService;
        }
        
        // GET api/<DressesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DressDTO>> GetDressById(int id)
        {
            DressDTO dress = await _dressService.GetDressById(id);
            if (dress == null)
                return NotFound();
            return Ok(dress);
        }

        // POST api/<DressesController>
        [HttpPost]
        public async Task<ActionResult<DressDTO>> AddDress([FromBody] NewDressDTO newDress)
        {
            if(!_dressService.CheckPrice(newDress.Price))
                return BadRequest("Price must be more than 0");
            DressDTO dress = await _dressService.AddDress(newDress);
            return CreatedAtAction(nameof(GetDressById), new { Id = dress.Id }, dress);
        }

        // PUT api/<DressesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDress(int id, [FromBody] DressDTO updateDress)
        {
            if(_dressService.CheckPrice(updateDress.Price))
            {
                return BadRequest("Price must be more than 0");
            }
            if(_dressService.GetDressById(id) == null)
            {
                return NotFound();
            }
            await _dressService.UpdateDress(id, updateDress);
            return Ok(updateDress);
        }
        
        // DELETE api/<DressesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DressDTO deleteDress)
        {
            if (_dressService.GetDressById(deleteDress.Id) == null)
            {
                return NotFound();
            }
            await _dressService.DeleteDress(deleteDress);
            return Ok(deleteDress);
        }
    }
}
