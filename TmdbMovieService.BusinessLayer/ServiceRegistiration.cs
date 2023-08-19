using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TmdbMovie.DataAccessLayer.Context;
using TmdbMovieService.BusinessLayer.Services;
using TmdbMovieService.BusinessLayer.Services.IServices;

namespace TmdbMovieService.BusinessLayer
{
    public static class ServiceRegistiration
    {
        public static void AddBusinessLayerServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();

            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddAutoMapper(typeof(ServiceRegistiration));

        }
    }
}
