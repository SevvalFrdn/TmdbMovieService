using Hangfire;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovieService.BusinessLayer.Constants.Hangfire;

namespace TmdbMovie.BackgroundService.Jobs
{
    public class JobRegistration
    {
        private static readonly RecurringJobOptions _options = new RecurringJobOptions { TimeZone = TimeZoneInfo.Local };
        public static void StartJobs()
        {
            RecurringJob.RemoveIfExists(nameof(GetMoviesJob));
            RecurringJob.AddOrUpdate<GetMoviesJob>(JobIds.GetMoviesJobId, queue: QueueNames.GetMoviesQueueName, job => job.Execute(), CronExpressions.hour, _options);

        }
    }
}
