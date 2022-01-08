namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Application.Interfaces.ServicesInterfaces;
    using Application.Validation;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Swashbuckle.AspNetCore.Annotations;

    [ApiController]
    [Route("api/ordersLines")]
    public class OrderLineController : Controller
    {
        private readonly ILogger<OrderLineController> _logger;
        private readonly IOrderLineService _orderLineService;
        private readonly IOrderService _orderService;
        private readonly IPizzaVariationService _pizzaVariationsService;
        private readonly OrderLineValidator _orderLineValidator;

        public OrderLineController(ILogger<OrderLineController> logger, IOrderLineService orderLineService, IPizzaVariationService pizzaVariationService, IOrderService orderService)
        {
            _logger = logger;
            _orderLineService = orderLineService;
            _pizzaVariationsService = pizzaVariationService;
            _orderLineValidator = new OrderLineValidator(_orderLineService);
            _orderService = orderService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Returns all OrdersLines")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<List<OrderLineDto>> GetAll()
        {
            return Ok(_orderLineService.GetAll());
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Returns order line by input id")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<OrderLineDto> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingOrderLine = _orderLineService.GetById(id);
            if (existingOrderLine == null)
            {
                return NotFound();
            }

            return Ok(existingOrderLine);
        }

        [HttpPost]
        [SwaggerResponse(201, "Inserts new order line in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<OrderLineDto> Insert([FromBody] OrderLineCreateRequestDto orderLine)
        {
            IEnumerable<int> pizzasVariationsIds = _pizzaVariationsService.GetIdentificators();
            IEnumerable<int> ordersIds = _orderService.GetIdentificators();

            ValidationResult validationResult = _orderLineValidator.Validate(orderLine, pizzasVariationsIds, ordersIds);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            OrderLineDto returnedDto = _orderLineService.Insert(orderLine);
            return Created("api/pizzasVariations/" + returnedDto.Id.ToString(), returnedDto);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Updates existing order line in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<OrderLineDto> Update([FromRoute] int id, [FromBody] OrderLineUpdateRequestDto orderLine)
        {
            var existingOrderLine = _orderLineService.GetById(id);
            if (existingOrderLine == null)
            {
                return NotFound();
            }

            IEnumerable<int> pizzasVariationsIdentificators = _pizzaVariationsService.GetIdentificators();

            ValidationResult validationResult = _orderLineValidator.Validate(orderLine, pizzasVariationsIdentificators);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_orderLineService.Update(id, orderLine));
        }

        [HttpPatch("{id}")]
        [SwaggerResponse(200, "Updates existing order line in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<OrderLineDto> Patch([FromRoute] int id, [FromBody] OrderLinePatchRequestDto orderLine)
        {
            var existingOrderLine = _orderLineService.GetById(id);
            if (existingOrderLine == null)
            {
                return NotFound();
            }

            ValidationResult validationResult;

            if (orderLine.PizzaVariationId == null)
            {
                validationResult = _orderLineValidator.Validate(orderLine);
            }
            else
            {
                IEnumerable<int> pizzasVariationsIdentificators = _pizzaVariationsService.GetIdentificators();
                validationResult = _orderLineValidator.Validate(orderLine, pizzasVariationsIdentificators);
            }

            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_orderLineService.Patch(id, orderLine));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Deletes existing order line from database(by changing flag IsDeleted)")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult Delete([FromRoute] int id)
        {
            var existingOrderLine = _orderLineService.GetById(id);
            if (existingOrderLine == null)
            {
                return NotFound();
            }

            _orderLineService.Delete(id);
            return NoContent();
        }
    }
}
