using AuthServer.Providers;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpaceBattleController : ControllerBase
    {
        #region Private fields

        private readonly JwtTokenProvider _tokenProvider;

        #endregion

        #region Constructor

        public SpaceBattleController(JwtTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        #endregion

        #region Public methods

        [HttpPost]
        [Route("WebClient/GameAction")]
        public async Task<ActionResult<Guid>> ExecuteGameAction(string action, string jwtToken)
        {
            bool result = await _tokenProvider.ValidateAsync(jwtToken);

            if (!result)
            {
                throw new Exception("Запрос не прошел валидацию!");
            }

            return Ok($"Действие {action} выполнено!");
        }

        #endregion
    }
}
