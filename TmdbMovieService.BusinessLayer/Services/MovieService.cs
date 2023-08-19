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
            int page = 1;

            string url = TmdbConstants.BaseURL + $"/movie/popular?api_key={TmdbConstants.ApiKey}&page={page}";

            var response = await _httpService.GetAsync(url);

            var movies = JsonConvert.DeserializeObject<MovieDTO>(response);

            Pages.page = movies.total_pages;

            foreach (var item in movies.results)
            {
                var moviesID = _appDbContext.Movies.FirstOrDefault(x => x.id == item.id);

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

            for (int i = 2; i < Pages.page; i++)
            {
                string url1 = TmdbConstants.BaseURL + $"/movie/popular?api_key={TmdbConstants.ApiKey}&page={i}";

                var response1 = await _httpService.GetAsync(url1);

                var movies1 = JsonConvert.DeserializeObject<MovieDTO>(response1);

                foreach (var item1 in movies1.results)
                {
                    var moviesID1 = _appDbContext.Movies.FirstOrDefault(x => x.id == item1.id);

                    if (moviesID1 is not null)
                    {
                        var movieDTO1 = _mapper.Map<Movie>(moviesID1);

                        _appDbContext.Movies.Update(movieDTO1);
                    }
                    else
                    {
                        var movieDTO1 = _mapper.Map<Movie>(item1);

                        movieDTO1.RowId = Guid.NewGuid();

                        await _appDbContext.Movies.AddAsync(movieDTO1); 
                    }
                }
            }

            await _appDbContext.SaveChangesAsync();
        }
    }
}
