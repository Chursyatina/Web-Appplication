namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.DTO.Request.AuthRequestDtos;
    using Application.Interfaces.ServicesInterfaces;
    using Domain.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Produces("application/json")]
    public class AuthController : Controller
    {
        private readonly ILogger<PizzaController> _logger;
        private readonly IBasketService _basketService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(ILogger<PizzaController> logger, UserManager<User> userManager, SignInManager<User> signInManager, IBasketService basketService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _basketService = basketService;
        }

        [HttpPost]
        [Route("api/signup")]
        public async Task<IActionResult> Register([FromBody] SingupRequestDto model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Phone = model.Phone, Basket = new Basket() };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    await _signInManager.SignInAsync(user, false);
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

        [HttpPost]
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

        [HttpPost]
        [Route("api/authinfo")]
        public async Task<IActionResult> AuthInfo()
        {
            User user = await GetCurrentUserAsync();
            bool isAuth = user != null;
            string role = isAuth ? (await _userManager.GetRolesAsync(user)).FirstOrDefault() : "noauth";

            var msg = new { isAuth, user, role };
            return Ok(msg);
        }

        // [HttpPost]
        // [Route("api/basket/add")]
        // public async Task<ActionResult<User>> AddOrderLineToBasket(Game game)
        // {
        //    if (!ModelState.IsValid)
        //    {
        //        logger.LogError($"BadRequest: {ModelState}");
        //        return BadRequest(ModelState);
        //    }

        // var user = await GetCurrentUserAsync();

        // if (user != null)
        //    {
        //        user.Basket.BasketGames.Add(new BasketGame() { Game = await baseContext.Game.GetItemByID(game.Id) });

        // try
        //        {
        //            baseContext.Basket.UpdateItem(user.Basket);
        //            baseContext.Save();
        //        }
        //        catch (Exception e)
        //        {
        //            logger.LogError(e.Message, e);
        //        }

        // logger.LogInformation($"Игра {game.Name} была добавлена в корзину пользователя {user.UserName}");
        //        return user;
        //    }
        //    else
        //    {
        //        var errorMsg = new
        //        {
        //            message = "Вход не выполнен.",
        //        };
        //        logger.LogError(errorMsg.message);
        //        return BadRequest(errorMsg);
        //    }
        // }
        private async Task<User> GetCurrentUserAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                var baskets = _basketService.GetModels();
                var basket = baskets.FirstOrDefault(b => b.User.Id == user.Id) ?? new Basket();

                basket.OrderLines = basket.OrderLines ?? new List<OrderLine>();
            }

            return user;
        }
    }
}
