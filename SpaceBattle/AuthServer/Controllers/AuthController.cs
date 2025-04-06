using AuthServer.Models;
using AuthServer.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        #region Private fields

        private readonly JwtTokenProvider _tokenProvider;
        private readonly UserManager<User> _userManager;

        private static List<User> users = new()
        {
            new User { UserName = "Petya", Password = "123", IsAdmin = true, IsPrivileged = true },
            new User { UserName = "Vasya", Password = "12345", IsPrivileged = true },
            new User { UserName = "Gosha", Password = "qwerty123" },
        };

        private static List<Game> games = new()
        {
            new Game { Id = Guid.Parse("29a425d1-84a7-45c4-96c6-7d154c13aa94"), Users = new List<User> { } },
            new Game { Id = Guid.Parse("75edd8af-e261-4694-be39-bd3c81b22a2b"), Users = users },
            new Game { Id = Guid.Parse("1aeaf07c-2272-4c41-865c-175c0687d375"), Users = new List<User> { new User { UserName = "Fedya", Password = "qwerty" } } },
        };

        #endregion

        #region Constructor

        public AccountController(JwtTokenProvider tokenProvider, UserManager<User> userManager)
        {
            _tokenProvider = tokenProvider;
            _userManager = userManager;
        }

        #endregion

        #region Public methods

        [HttpPost]
        [Route("WebClient/CreateBattle")]
        public ActionResult<Guid> CreateBattle(IEnumerable<User> players)
        {
            Game game = new()
            {
                Id = Guid.NewGuid(),
                Users = players
            };
            games.Add(game);
            return Ok(game.Id);
        }

        [HttpPost]
        [Route("WebClient/Login")]
        public async Task<ActionResult<string>> Authenticate(Guid gameId, string userName, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(userName))
                {
                    throw new Exception("Имя пользователя или пароль не указаны");
                }

                var trimmedLogin = userName.Trim();

                User? appUser = users.Find(u => u.UserName == userName && u.Password == password);

                if (appUser == null)
                {
                    throw new Exception("Неправильное имя пользователя или пароль");
                }

                Game? game = games.Find(g => g.Id == gameId);

                if (game == null)
                {
                    throw new Exception($"Игра с идентификатором {gameId} не найдена");
                }

                string token = await _tokenProvider.GenerateAsync("Game", _userManager, appUser, gameId);

                return Ok(token);
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }

        #endregion
    }
}
