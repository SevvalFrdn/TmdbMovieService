using Microsoft.AspNetCore.Mvc;
using TmdbMovie.DataAccessLayer.DTO_s;
using TmdbMovieService.BusinessLayer.Services.IServices;

namespace TmdbMovieService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserLoginResponse>> LoginUserAsync([FromBody]UserLoginRequest request)
        {
            return await _authService.LoginUserAsync(request);
        }
    }
}
