using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TmdbMovie.DataAccessLayer.Context;
using TmdbMovieService.BusinessLayer.Services.IServices;
using TmdbMovieService.BusinessLayer.Services;
using TmdbMovie.DataAccessLayer.DTO_s;

namespace TmdbMovieService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;
        private readonly IMapper _mapper;
        private AppDbContext _appDbContext;

        public MovieController(IMovieService movieService, ILogger<MovieController> logger, IMapper mapper, AppDbContext appDbContext)
        {
            _movieService = movieService;
            _logger = logger;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }
        [HttpGet("GetPopular")]
        public async Task<MovieDTO> GetMovies()
        {
            var MoviesPages1 = await _movieService.GetMovies(1);

            return MoviesPages1;
        }
    }
}
