﻿using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovieService.BusinessLayer.Constants.Hangfire;
using TmdbMovieService.BusinessLayer.Services;
using TmdbMovieService.BusinessLayer.Services.IServices;

namespace TmdbMovie.BackgroundService.Jobs
{
    public class GetMoviesJob
    {
        private readonly ILogger<GetMoviesJob> _logger;
        private readonly IMovieService _movieService;

        public GetMoviesJob(ILogger<GetMoviesJob> logger, IMovieService movieService)
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
            for (int i = 1; i <= 500; i++)
            {
                _movieService.GetMovies(i).GetAwaiter().GetResult();
            }
        }

    }
}
