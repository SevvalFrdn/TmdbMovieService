﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovie.DataAccessLayer.DTO_s;
using TmdbMovie.DataAccessLayer.Models;
using TmdbMovieService.BusinessLayer.Models;

namespace TmdbMovieService.BusinessLayer.Services.IServices
{
    public interface IMovieService
    {
        Task<MovieDTO> GetMovies(int pages);
    }
}
