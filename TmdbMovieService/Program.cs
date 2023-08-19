using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using TmdbMovie.DataAccessLayer.Context;
using TmdbMovieService.BusinessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbcontext"));
});
builder.Services.AddBusinessLayerServices();
Log.Logger = new LoggerConfiguration()
                           .MinimumLevel.Error()
                           .WriteTo.File($"logs/log.txt", rollingInterval: RollingInterval.Day)
                           .Enrich.FromLogContext()
                           .CreateLogger();
builder.Host.UseSerilog();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
