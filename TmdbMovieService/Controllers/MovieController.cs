using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TmdbMovie.DataAccessLayer.Context;
using TmdbMovieService.BusinessLayer.Services.IServices;
using TmdbMovieService.BusinessLayer.Services;

namespace TmdbMovieService.Controllers
{
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
        public async Task<IActionResult> GetMovies()
        {
            await _movieService.GetMovies();
            return Ok();
        }
    }
}
