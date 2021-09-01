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
    [Route("api/pizzas")]
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;
        private readonly IPizzaService _pizzaService;
        private readonly IIngredientService _ingredientService;
        private readonly PizzaValidator _pizzaValidator;

        public PizzaController(ILogger<PizzaController> logger, IPizzaService pizzaService, IIngredientService ingredientService)
        {
            _logger = logger;
            _pizzaService = pizzaService;
            _ingredientService = ingredientService;
            _pizzaValidator = new PizzaValidator(_pizzaService);
        }

        [HttpGet]
        [SwaggerResponse(200, "Returns all Pizzas")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<List<PizzaDto>> GetAll()
        {
            return Ok(_pizzaService.GetAll());
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Returns pizza by input id")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<PizzaDto> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPizza = _pizzaService.GetById(id);
            if (existingPizza == null)
            {
                return NotFound();
            }

            return Ok(existingPizza);
        }

        [HttpPost]
        [SwaggerResponse(201, "Inserts new pizza in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<PizzaDto> Insert([FromBody] PizzaCreateRequestDto pizza)
        {
            ValidationResult validationResult = _pizzaValidator.Validate(pizza);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            PizzaDto returnedDto = _pizzaService.Insert(pizza);
            return Created("api/pizzas/" + returnedDto.Id.ToString(), returnedDto);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Updates existing pizza in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<PizzaDto> Update([FromRoute] int id, [FromBody] PizzaUpdateRequestDto pizza)
        {
            var existingPizza = _pizzaService.GetById(id);
            if (existingPizza == null)
            {
                return NotFound();
            }

            IEnumerable<int> ingredientsIdentificators = _ingredientService.GetIdentificators();

            ValidationResult validationResult = _pizzaValidator.Validate(pizza, id, ingredientsIdentificators);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_pizzaService.Update(id, pizza));
        }

        [HttpPatch("{id}")]
        [SwaggerResponse(200, "Updates existing pizza in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<PizzaDto> Patch([FromRoute] int id, [FromBody] PizzaPatchRequestDto pizza)
        {
            var existingPizza = _pizzaService.GetById(id);
            if (existingPizza == null)
            {
                return NotFound();
            }

            ValidationResult validationResult;

            if (pizza.Ingredients == null)
            {
                validationResult = _pizzaValidator.Validate(pizza, id);
            }
            else
            {
                IEnumerable<int> identificators = _ingredientService.GetIdentificators();
                validationResult = _pizzaValidator.Validate(pizza, id, identificators);
            }

            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_pizzaService.Patch(id, pizza));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Deletes existing pizza from database(by changing flag IsDeleted)")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult Delete([FromRoute] int id)
        {
            var existingPizza = _pizzaService.GetById(id);
            if (existingPizza == null)
            {
                return NotFound();
            }

            _pizzaService.Delete(id);
            return NoContent();
        }
    }
}
