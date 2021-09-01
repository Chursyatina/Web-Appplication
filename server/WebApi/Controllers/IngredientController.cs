namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Application.Validation;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Swashbuckle.AspNetCore.Annotations;

    [ApiController]
    [Route("api/ingredients")]
    public class IngredientController : Controller
    {
        private readonly ILogger<IngredientController> _logger;
        private readonly IIngredientService _ingredientService;
        private readonly IngredientValidator _ingredientValidator;

        public IngredientController(ILogger<IngredientController> logger, IIngredientService ingredientService)
        {
            _logger = logger;
            _ingredientService = ingredientService;
            _ingredientValidator = new IngredientValidator(_ingredientService);
        }

        [HttpGet]
        [SwaggerResponse(200, "Returns all ingredients")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<List<IngredientDto>> GetAll()
        {
            return Ok(_ingredientService.GetAll());
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Returns ingredient by input id")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<IngredientDto> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingIngredient = _ingredientService.GetById(id);
            if (existingIngredient == null)
            {
                return NotFound();
            }

            return Ok(existingIngredient);
        }

        [HttpPost]
        [SwaggerResponse(201, "Inserts new ingredient in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<IngredientDto> Insert([FromBody] IngredientCreateRequestDto ingredient)
        {
            ValidationResult validationResult = _ingredientValidator.Validate(ingredient);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            IngredientDto returnedDto = _ingredientService.Insert(ingredient);
            return Created("api/ingredients/" + returnedDto.Id.ToString(), returnedDto);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Updates existing ingredient in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<IngredientDto> Update([FromRoute] int id, [FromBody] IngredientUpdateRequestDto ingredient)
        {
            var existingIngredient = _ingredientService.GetById(id);
            if (existingIngredient == null)
            {
                return NotFound();
            }

            ValidationResult validationResult = _ingredientValidator.Validate(ingredient, id);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_ingredientService.Update(id, ingredient));
        }

        [HttpPatch("{id}")]
        [SwaggerResponse(200, "Updates existing ingredient in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<IngredientDto> Patch([FromRoute] int id, [FromBody] IngredientPatchRequestDto ingredient)
        {
            var existingIngredient = _ingredientService.GetById(id);
            if (existingIngredient == null)
            {
                return NotFound();
            }

            ValidationResult validationResult = _ingredientValidator.Validate(ingredient, id);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_ingredientService.Patch(id, ingredient));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Deletes existing ingredient from database(by changing flag IsDeleted)")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult Delete([FromRoute] int id)
        {
            var existingIngredient = _ingredientService.GetById(id);
            if (existingIngredient == null)
            {
                return NotFound();
            }

            _ingredientService.Delete(id);
            return NoContent();
        }
    }
}
