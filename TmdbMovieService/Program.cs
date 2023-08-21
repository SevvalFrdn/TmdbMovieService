using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;
using TmdbMovie.BackgroundService.Jobs;
using TmdbMovie.DataAccessLayer.Context;
using TmdbMovieService.BusinessLayer;
using TmdbMovieService.BusinessLayer.Constants.Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbcontext"));
});
builder.Services.AddBusinessLayerServices();

string dateTime = DateTime.Now.ToString("dd-MM-yyyy", new System.Globalization.CultureInfo("tr-TR"));

Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Information()
                            .WriteTo.Debug()
                            .WriteTo.File($"logs/log_{dateTime}.txt", LogEventLevel.Information)
                            .Enrich.FromLogContext()
                            .CreateLogger();
builder.Host.UseSerilog();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnectionString"), new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(35),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(35),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    }));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer(opt =>
{
    opt.ServerName = "tmdbmovieservice";
    opt.Queues = new string[] { QueueNames.GetMoviesQueueName };
    opt.WorkerCount = 10;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

app.MapHangfireDashboard();

app.Run();

JobRegistration.StartJobs();
