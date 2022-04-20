namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.DTO.Request.OrderRequestDtos;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Application.Interfaces.ServicesInterfaces;
    using Application.Validation;
    using Domain.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Swashbuckle.AspNetCore.Annotations;

    [ApiController]
    [Route("api/orders")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly IOrderLineService _orderLinesService;
        private readonly IOrderStatusService _orderStatusService;
        private readonly OrderValidator _orderValidator;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService, IOrderLineService orderLinesService, IOrderStatusService orderStatusService, UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService)
        {
            _logger = logger;
            _orderService = orderService;
            _orderLinesService = orderLinesService;
            _orderStatusService = orderStatusService;
            _orderValidator = new OrderValidator(_orderService);
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Returns all Orders")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<List<OrderDto>> GetAll()
        {
            return Ok(_orderService.GetAll());
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Returns order by input id")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<OrderDto> Get(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingOrder = _orderService.GetById(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            return Ok(existingOrder);
        }

        [HttpPost]
        [SwaggerResponse(201, "Inserts new order in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public async Task<ActionResult<OrderDto>> InsertAsync([FromBody] OrderCreateRequestDto order)
        {
            User user = await GetCurrentUserAsync();
            OrderDto returnedDto = _orderService.Insert(order, user);
            return Created("api/pizzasVariations/" + returnedDto.Id.ToString(), returnedDto);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Updates existing order in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<OrderDto> Update([FromRoute] string id, [FromBody] OrderUpdateRequestDto order)
        {
            var existingOrder = _orderService.GetById(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            IEnumerable<string> ordersLinesIds = _orderLinesService.GetIdentificators();
            IEnumerable<string> orderStatusesIds = _orderStatusService.GetIdentificators();

            ValidationResult validationResult = _orderValidator.Validate(order, ordersLinesIds, orderStatusesIds);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_orderService.Update(id, order));
        }

        [HttpPatch("{id}")]
        [SwaggerResponse(200, "Updates existing order in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<OrderDto> Patch([FromRoute] string id, [FromBody] OrderPatchRequestDto order)
        {
            var existingOrder = _orderService.GetById(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            ValidationResult validationResult;

            if (order.OrderLinesIds == null && order.OrderStatusId == null)
            {
                validationResult = _orderValidator.Validate(order);
            }
            else if (order.OrderLinesIds != null && order.OrderStatusId == null)
            {
                IEnumerable<string> ordersLinesIds = _orderLinesService.GetIdentificators();
                validationResult = _orderValidator.Validate(order, ordersLinesIds, null);
            }
            else if (order.OrderLinesIds == null && order.OrderStatusId != null)
            {
                IEnumerable<string> orderStatusesIds = _orderStatusService.GetIdentificators();
                validationResult = _orderValidator.Validate(order, null, orderStatusesIds);
            }
            else
            {
                IEnumerable<string> ordersLinesIds = _orderLinesService.GetIdentificators();
                IEnumerable<string> orderStatusesIds = _orderStatusService.GetIdentificators();
                validationResult = _orderValidator.Validate(order, ordersLinesIds, orderStatusesIds);
            }

            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_orderService.Patch(id, order));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Deletes existing order from database(by changing flag IsDeleted)")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult Delete([FromRoute] string id)
        {
            var existingOrder = _orderService.GetById(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _orderService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        private async Task<User> GetCurrentUserAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                User existingUser = _userService.GetModelById(user.Id);

                return existingUser;
            }

            return user;
        }
    }
}
