using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovieService.EntityLayer.Models;

namespace TmdbMovie.DataAccessLayer.Context
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }
        public DbSet<Movie> Movies { get; set; }
    }
}
