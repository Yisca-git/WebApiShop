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
        private readonly IModelService _modelService;

        public DressesController(IDressService dressService, IModelService modelService)
        {
            _dressService = dressService;
            _modelService = modelService;
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
            if (!_dressService.CheckPrice(newDress.Price))
                return BadRequest("Price must be more than 0");
            if(!await _modelService.IsExistsModelById(newDress.ModelId))
                return NotFound("Model not found");
            DressDTO dress = await _dressService.AddDress(newDress);
            return CreatedAtAction(nameof(GetDressById), new { Id = dress.Id }, dress);
        }

        // PUT api/<DressesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDress(int id, [FromBody] DressDTO updateDress)
        {
            if (!_dressService.CheckPrice(updateDress.Price))
            {
                return BadRequest("Price must be more than 0");
            }
            if(!await _dressService.IsExistsDressById(id))
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
            if(!await _dressService.IsExistsDressById(deleteDress.Id))
            {
                return NotFound();
            }
            await _dressService.DeleteDress(deleteDress);
            return Ok(deleteDress);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetCountByModelIdAndSizeForDate(int id, string size, DateOnly date)
        {
            if (await _modelService.GetModelById(id) == null)
            {
                return NotFound("Model not found");
            }
            if (!_dressService.CheckDate(date))
            {
                return BadRequest("Date must be in the future");
            }
            int count = await _dressService.GetCountByModelIdAndSizeForDate(id, size, date);
            return Ok(count);
        }

        [HttpGet("sizes")]
        public async Task<ActionResult<List<string>>> GetSizesByModelId(int id)
        {
            if (await _modelService.GetModelById(id) == null)
            {
                return NotFound("Model not found");
            }
            List<string> sizes = await _dressService.GetSizesByModelId(id);
            return Ok(sizes);
        }
    }
}
