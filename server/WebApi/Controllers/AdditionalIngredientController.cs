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
    [Route("api/additionalIngredients")]
    public class AdditionalIngredientController : Controller
    {
        private readonly ILogger<AdditionalIngredientController> _logger;
        private readonly IAdditionalIngredientService _additionalIngredientService;
        private readonly AdditionalIngredientValidator _additionalIngredientValidator;

        public AdditionalIngredientController(ILogger<AdditionalIngredientController> logger, IAdditionalIngredientService additionalIngredientService)
        {
            _logger = logger;
            _additionalIngredientService = additionalIngredientService;
            _additionalIngredientValidator = new AdditionalIngredientValidator(additionalIngredientService);
        }

        [HttpGet]
        [SwaggerResponse(200, "Returns all additional ingredients")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<List<AdditionalIngredientDto>> GetAll()
        {
            return Ok(_additionalIngredientService.GetAll());
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Returns additional ingredient by input id")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<AdditionalIngredientDto> Get(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAdditionalIngredient = _additionalIngredientService.GetById(id);
            if (existingAdditionalIngredient == null)
            {
                return NotFound();
            }

            return Ok(existingAdditionalIngredient);
        }

        [HttpPost]
        [SwaggerResponse(201, "Inserts new additional ingredient in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<AdditionalIngredientDto> Insert([FromBody] AdditionalIngredientCreateRequestDto additionalIngredient)
        {
            ValidationResult validationResult = _additionalIngredientValidator.Validate(additionalIngredient);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            AdditionalIngredientDto returnedDto = _additionalIngredientService.Insert(additionalIngredient);
            return Created("api/additionalIngredients/" + returnedDto.Id.ToString(), returnedDto);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Updates existing additional ingredient in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<AdditionalIngredientDto> Update([FromRoute] string id, [FromBody] AdditionalIngredientUpdateRequestDto additionalIngredient)
        {
            var existingAdditionalIngredient = _additionalIngredientService.GetById(id);
            if (existingAdditionalIngredient == null)
            {
                return NotFound();
            }

            ValidationResult validationResult = _additionalIngredientValidator.Validate(additionalIngredient, id);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_additionalIngredientService.Update(id, additionalIngredient));
        }

        [HttpPatch("{id}")]
        [SwaggerResponse(200, "Updates existing additional ingredient in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<AdditionalIngredientDto> Patch([FromRoute] string id, [FromBody] AdditionalIngredientPatchRequestDto additionalIngredient)
        {
            var existingAdditionalIngredient = _additionalIngredientService.GetById(id);
            if (existingAdditionalIngredient == null)
            {
                return NotFound();
            }

            ValidationResult validationResult = _additionalIngredientValidator.Validate(additionalIngredient, id);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_additionalIngredientService.Patch(id, additionalIngredient));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Deletes existing additional ingredient from database(by changing flag IsDeleted)")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult Delete([FromRoute] string id)
        {
            var existingAdditionalIngredient = _additionalIngredientService.GetById(id);
            if (existingAdditionalIngredient == null)
            {
                return NotFound();
            }

            _additionalIngredientService.Delete(id);
            return NoContent();
        }
    }
}
