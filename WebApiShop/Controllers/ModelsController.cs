using Microsoft.AspNetCore.Mvc;
using Entities;
using DTOs;
using Repositories;
using Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IModelService _modelService;
        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }
        // GET: api/<ModelsController>
        [HttpGet]
        public async Task<ActionResult<FinalModels>> GetModels(string? Description, int? minPrice,
                       int? maxPrice,[FromQuery] int[] categoriesId, string color, int position =1 , int skip = 8)
        {
            if(!_modelService.ValidateQueryParameters(position, skip, minPrice, maxPrice))
            {
                return BadRequest("Invalid query parameters. Please check your request and try again.");
            }
            FinalModels models = await _modelService.GetModels(Description, minPrice, maxPrice, categoriesId, color, position, skip);
            if (models.Models.Count() == 0)
            {
                return NoContent();
            }
            return Ok(models);  
        }

        // GET api/<ModelsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelDTO>> GetModelById(int id)
        {
            ModelDTO model = await _modelService.GetModelById(id);
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        // POST api/<ModelsController>
        [HttpPost]
        public async Task<ActionResult<ModelDTO>> AddModel([FromBody] NewModelDTO newModel)
        {
            if(!_modelService.CheckBasePrice(newModel.BasePrice))
            {
                return BadRequest("Not valid price.");
            }
            ModelDTO model = await _modelService.AddModel(newModel);
            return CreatedAtAction(nameof(GetModelById), new { Id = model.Id }, model);
        }

        // PUT api/<ModelsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModel(int id, [FromBody] ModelDTO updateModel)
        {
            if (_modelService.GetModelById(updateModel.Id) == null)
            {
                return NotFound();
            }
            if(!_modelService.CheckBasePrice(updateModel.BasePrice))
            {
                return BadRequest("Not valid price.");
            }
            await _modelService.UpdateModel(updateModel);
            return Ok(updateModel);
        }

        // DELETE api/<ModelsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoedl([FromBody] ModelDTO deleteModel)
        {
            if (_modelService.GetModelById(deleteModel.Id) == null)
            {
                return NotFound();
            }
            await _modelService.DeleteMoedl(deleteModel);
            return Ok(deleteModel);
        }
    }
}
