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
    [Route("api/pizzasVariations")]
    public class PizzaVariationController : Controller
    {
        private readonly ILogger<PizzaVariationController> _logger;
        private readonly IPizzaVariationService _pizzaVariationService;
        private readonly IPizzaService _pizzaService;
        private readonly ISizeService _sizeService;
        private readonly IDoughService _doughService;
        private readonly IIngredientService _ingredientService;
        private readonly IAdditionalIngredientService _additionalIngredientService;
        private readonly PizzaVariationValidator _pizzaVariationValidator;

        public PizzaVariationController(
            ILogger<PizzaVariationController> logger,
            IPizzaVariationService pizzaVariationService,
            IPizzaService pizzaService,
            ISizeService sizeService,
            IDoughService doughService,
            IIngredientService ingredientService,
            IAdditionalIngredientService additionalIngredientService)
        {
            _logger = logger;
            _pizzaVariationService = pizzaVariationService;
            _pizzaService = pizzaService;
            _sizeService = sizeService;
            _doughService = doughService;
            _ingredientService = ingredientService;
            _additionalIngredientService = additionalIngredientService;
            _pizzaVariationValidator = new PizzaVariationValidator(_pizzaVariationService);
        }

        [HttpGet]
        [SwaggerResponse(200, "Returns all PizzasVariations")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<List<PizzaVariationDto>> GetAll()
        {
            return Ok(_pizzaVariationService.GetAll());
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Returns variation of pizza by input id")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<PizzaVariationDto> Get(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPizzaVariation = _pizzaVariationService.GetById(id);
            if (existingPizzaVariation == null)
            {
                return NotFound();
            }

            return Ok(existingPizzaVariation);
        }

        [HttpPost]
        [SwaggerResponse(201, "Inserts new variation of pizza in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<PizzaVariationDto> Insert([FromBody] PizzaVariationCreateRequestDto pizzaVariation)
        {
            IEnumerable<string> pizzasIds = _pizzaService.GetIdentificators();
            IEnumerable<string> sizesIds = _sizeService.GetIdentificators();
            IEnumerable<string> doughsIds = _doughService.GetIdentificators();
            IEnumerable<string> ingredientsIds = _ingredientService.GetIdentificators();
            IEnumerable<string> additionalIngredientsIds = _additionalIngredientService.GetIdentificators();

            ValidationResult validationResult = _pizzaVariationValidator.Validate(pizzaVariation, pizzasIds, sizesIds, doughsIds, ingredientsIds, additionalIngredientsIds);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            PizzaVariationDto returnedDto = _pizzaVariationService.Insert(pizzaVariation);
            return Created("api/pizzasVariations/" + returnedDto.Id.ToString(), returnedDto);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Updates existing variation of pizza in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<PizzaVariationDto> Update([FromRoute] string id, [FromBody] PizzaVariationUpdateRequestDto pizza)
        {
            var existingPizzaVarition = _pizzaVariationService.GetById(id);
            if (existingPizzaVarition == null)
            {
                return NotFound();
            }

            IEnumerable<string> pizzasIds = _pizzaService.GetIdentificators();
            IEnumerable<string> sizesIds = _sizeService.GetIdentificators();
            IEnumerable<string> doughsIds = _doughService.GetIdentificators();
            IEnumerable<string> ingredientsIds = _ingredientService.GetIdentificators();
            IEnumerable<string> additionalIngredientsIds = _additionalIngredientService.GetIdentificators();

            ValidationResult validationResult = _pizzaVariationValidator.Validate(pizza, pizzasIds, sizesIds, doughsIds, ingredientsIds, additionalIngredientsIds);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_pizzaVariationService.Update(id, pizza));
        }

        [HttpPatch("{id}")]
        [SwaggerResponse(200, "Updates existing variation of pizza in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<PizzaVariationDto> Patch([FromRoute] string id, [FromBody] PizzaVariationPatchRequestDto pizzaVariation)
        {
            var existingPizzaVariation = _pizzaVariationService.GetById(id);
            if (existingPizzaVariation == null)
            {
                return NotFound();
            }

            IEnumerable<string> pizzasIds = _pizzaService.GetIdentificators();
            IEnumerable<string> sizesIds = _sizeService.GetIdentificators();
            IEnumerable<string> doughsIds = _doughService.GetIdentificators();
            IEnumerable<string> ingredientsIds = _ingredientService.GetIdentificators();
            IEnumerable<string> additionalIngredientsIds = _additionalIngredientService.GetIdentificators();

            ValidationResult validationResult = _pizzaVariationValidator.Validate(pizzaVariation, pizzasIds, sizesIds, doughsIds, ingredientsIds, additionalIngredientsIds);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_pizzaVariationService.Patch(id, pizzaVariation));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Deletes existing variation of pizza from database(by changing flag IsDeleted)")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult Delete([FromRoute] string id)
        {
            var existingPizzaVariation = _pizzaVariationService.GetById(id);
            if (existingPizzaVariation == null)
            {
                return NotFound();
            }

            _pizzaVariationService.Delete(id);
            return NoContent();
        }
    }
}
