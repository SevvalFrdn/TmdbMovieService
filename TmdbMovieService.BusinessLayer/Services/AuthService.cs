using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovie.DataAccessLayer.Context;
using TmdbMovie.DataAccessLayer.DTO_s;
using TmdbMovieService.BusinessLayer.Services.IServices;

namespace TmdbMovieService.BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private AppDbContext _appDbContext;
        private readonly ITokenService _tokenService;

        public AuthService(AppDbContext appDbContext, ITokenService tokenService)
        {
            _appDbContext = appDbContext;
            _tokenService = tokenService;
        }

        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request)
        {
            UserLoginResponse response = new();

            var user = await _appDbContext.Users.SingleOrDefaultAsync(u => u.UserName == request.Username && u.Password == request.Password);

            var generatedTokenInformation = await _tokenService.GenerateToken(new GenerateTokenRequest { Username = request.Username });

            if (user is not null)
            {
                response.AccessTokenExpireDate = generatedTokenInformation.TokenExpireDate;
                response.AuthenticateResult = true;
                response.AuthToken = generatedTokenInformation.Token;
            }
            return response;
        }
    }
}
