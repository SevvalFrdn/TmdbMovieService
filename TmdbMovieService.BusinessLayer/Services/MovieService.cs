using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovie.DataAccessLayer.Context;
using TmdbMovie.DataAccessLayer.DTO_s;
using TmdbMovieService.BusinessLayer.Constants;
using TmdbMovieService.BusinessLayer.Models;
using TmdbMovieService.BusinessLayer.Services.IServices;
using TmdbMovieService.EntityLayer.Models;

namespace TmdbMovieService.BusinessLayer.Services
{
    public class MovieService : IMovieService
    {
        private readonly IHttpService _httpService;
        private readonly ILogger<MovieService> _logger;
        private readonly IMapper _mapper;
        private AppDbContext _appDbContext;

        public MovieService(IHttpService httpService, ILogger<MovieService> logger, IMapper mapper, AppDbContext appDbContext)
        {
            _httpService = httpService;
            _logger = logger;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task GetMovies()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetMovies)} was started");
                for (int i = 1; i <= 500; i++)
                {
                    string url = TmdbConstants.BaseURL + $"/movie/popular?api_key={TmdbConstants.ApiKey}&page={i}";

                    var response = await _httpService.GetAsync(url);

                    var movies = JsonConvert.DeserializeObject<MovieDTO>(response);

                    Pages.page = movies.total_pages;

                    foreach (var item in movies.results)
                    {
                        var moviesID = _appDbContext.Movies.FirstOrDefault(x => x.id == item.id);

                        _ = CheckifNullProperty(item);

                        if (moviesID is not null)
                        {
                            var movieDTO = _mapper.Map<Movie>(moviesID);

                            _appDbContext.Movies.Update(movieDTO);
                        }
                        else
                        {
                            var movieDTO = _mapper.Map<Movie>(item);

                            movieDTO.RowId = Guid.NewGuid();

                            await _appDbContext.Movies.AddAsync(movieDTO);
                        }
                    }
                }

                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, LogLevel.Error, $"{nameof(GetMovies)} is error");
                throw;
            }
        }

        public MovieDTO.Result CheckifNullProperty(MovieDTO.Result movie)
        {
            movie.backdrop_path = movie.backdrop_path ?? string.Empty;
            movie.release_date = movie.release_date ?? string.Empty;
            movie.genre_id = movie.genre_id ?? 0;
            movie.original_language = movie.original_language ?? string.Empty;
            movie.original_title = movie.original_title ?? string.Empty;
            movie.overview = movie.overview ?? string.Empty;
            movie.poster_path = movie.poster_path ?? string.Empty;
            movie.title = movie.title ?? string.Empty;
            return movie;
        }
    }
}
