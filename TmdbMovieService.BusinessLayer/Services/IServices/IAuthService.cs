using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovie.DataAccessLayer.DTO_s;

namespace TmdbMovieService.BusinessLayer.Services.IServices
{
    public interface IAuthService
    {
        Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request);
    }
}
