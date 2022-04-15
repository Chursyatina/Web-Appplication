namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.DTO.Request;
    using Application.DTO.Request.AuthRequestDtos;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Application.Interfaces.ServicesInterfaces;
    using Domain.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Produces("application/json")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IBasketService _basketService;
        private readonly IPizzaVariationService _pizzaVariationService;
        private readonly IPizzaService _pizzaService;
        private readonly ISizeService _sizeService;
        private readonly IDoughService _doughService;
        private readonly IIngredientService _ingredientService;
        private readonly IAdditionalIngredientService _additionalIngredientService;
        private readonly IOrderLineService _orderLineService;
        private readonly IUserService _userService;

        // private readonly PizzaVariationValidator _pizzaVariationValidator;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(
            ILogger<AuthController> logger,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IBasketService basketService,
            IPizzaVariationService pizzaVariationService,
            IPizzaService pizzaService,
            ISizeService sizeService,
            IDoughService doughService,
            IIngredientService ingredientService,
            IAdditionalIngredientService additionalIngredientService,
            IOrderLineService orderLineService,
            IUserService userService)

            // PizzaVariationValidator pizzaVariationValidator
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _basketService = basketService;
            _pizzaVariationService = pizzaVariationService;
            _pizzaService = pizzaService;
            _sizeService = sizeService;
            _doughService = doughService;
            _ingredientService = ingredientService;
            _additionalIngredientService = additionalIngredientService;

            // _pizzaVariationValidator = pizzaVariationValidator;
            _orderLineService = orderLineService;
            _userService = userService;
        }

        [HttpPost]
        [Route("api/signup")]
        public async Task<IActionResult> Register([FromBody] SingupRequestDto model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.Phone, Phone = model.Phone, Password = model.Password, Basket = new Basket() };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    await _signInManager.SignInAsync(user, false);

                    // User fullUser = _userService.InitializaBasketByPhone(model.Phone);
                    var msg = new
                    {
                        message = $"Добавлен новый пользователь: {user.UserName}",
                    };
                    return Ok(msg);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    var errorMsg = new
                    {
                        message = "Пользователь не добавлен.",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage)),
                    };
                    return BadRequest(errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Неверные входные данные.",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage)),
                };
                return BadRequest(errorMsg);
            }
        }

        [Route("api/signin")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] SigninRequestDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Phone, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var msg = new
                    {
                        message = $"Выполнен вход пользователем: {model.Phone}",
                    };
                    return Ok(msg);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неправильный логин и (или) пароль");
                    var errorMsg = new
                    {
                        message = "Вход не выполнен.",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage)),
                    };
                    _logger.LogError(errorMsg.message);
                    return BadRequest(errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Вход не выполнен.",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage)),
                };
                return BadRequest(errorMsg);
            }
        }

        [HttpGet]
        [Route("api/signout")]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            var msg = new
            {
                message = "Выполнен выход.",
            };
            return Ok(msg);
        }

        [HttpGet]
        [Route("api/authinfo")]
        public async Task<IActionResult> AuthInfo()
        {
            User user = await GetCurrentUserAsync();
            bool isAuth = user != null;
            string role = isAuth ? (await _userManager.GetRolesAsync(user)).FirstOrDefault() : "noauth";

            var msg = new { isAuth, user, role };
            return Ok(msg);
        }

        [HttpPost]
        [Route("api/basket/add")]
        public async Task<ActionResult<User>> AddPizzaToBasket([FromBody] PizzaVariationUpdateRequestDto pizzaVariation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await GetCurrentUserAsync();

            if (user != null)
            {
                IEnumerable<string> pizzasIds = _pizzaService.GetIdentificators();
                IEnumerable<string> sizesIds = _sizeService.GetIdentificators();
                IEnumerable<string> doughsIds = _doughService.GetIdentificators();
                IEnumerable<string> ingredientsIds = _ingredientService.GetIdentificators();
                IEnumerable<string> additionalIngredientsIds = _additionalIngredientService.GetIdentificators();

                // ValidationResult validationResult = _pizzaVariationValidator.Validate(pizzaVariation, pizzasIds, sizesIds, doughsIds, ingredientsIds, additionalIngredientsIds);
                // if (!validationResult.IsValid)
                // {
                //    return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
                // }
                PizzaVariationDto pizzaToInsert = _pizzaVariationService.FullInsert(pizzaVariation);

                OrderLineCreateRequestDto orderLineCreateRequestDto = new OrderLineCreateRequestDto();
                orderLineCreateRequestDto.OrderId = user.Basket.Id;
                orderLineCreateRequestDto.PizzaVariationId = pizzaToInsert.Id;

                OrderLineDto returnedDto = _orderLineService.InsertToBasket(orderLineCreateRequestDto);

                return user;
            }
            else
            {
                var errorMsg = new
                {
                    message = "Вход не выполнен.",
                };
                return BadRequest(errorMsg);
            }
        }

        [HttpDelete]
        [Route("api/basket/{id}")]
        public async Task<ActionResult<User>> DeletePizzaFromBasket(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await GetCurrentUserAsync();

            if (user != null)
            {
                var temp = user.Basket.OrderLines.FirstOrDefault(bg => bg.PizzaVariation.Id == id);
                if (temp != null)
                {
                    user.Basket.OrderLines.Remove(temp);
                }

                try
                {
                    _basketService.UpdateByModel(user.Basket);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message, e);
                }

                return user;
            }
            else
            {
                var errorMsg = new
                {
                    message = "Вход не выполнен.",
                };
                _logger.LogError(errorMsg.message);
                return BadRequest(errorMsg);
            }
        }

        [HttpDelete]
        [Route("api/basket/clear")]
        public async Task<ActionResult<User>> ClearBasket()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await GetCurrentUserAsync();

            if (user != null)
            {
                user.Basket.OrderLines.Clear();

                try
                {
                    _basketService.UpdateByModel(user.Basket);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message, e);
                }

                _logger.LogInformation($"Корзина пользователя {user.UserName} успешно очищена");
                return user;
            }
            else
            {
                var errorMsg = new
                {
                    message = "Вход не выполнен.",
                };
                _logger.LogError(errorMsg.message);
                return BadRequest(errorMsg);
            }
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
