using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovieService.BusinessLayer.Constants.Hangfire;
using TmdbMovieService.BusinessLayer.Services;

namespace TmdbMovie.BackgroundService.Jobs
{
    public class GetMoviesJob
    {
        private readonly ILogger<GetMoviesJob> _logger;
        private readonly MovieService _movieService;

        public GetMoviesJob(ILogger<GetMoviesJob> logger, MovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }
        public void Execute()
        {
            _logger.LogInformation("GetMoviesJob starting");

            BackgroundJob.Enqueue(() => Get());
        }

        [Queue(QueueNames.GetMoviesQueueName)]
        [AutomaticRetry(Attempts = 5)]
        public void Get()
        {
            _movieService.GetMovies().GetAwaiter().GetResult();
        }

    }
}
