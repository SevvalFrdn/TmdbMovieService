using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovie.DataAccessLayer.DTO_s;
using TmdbMovieService.EntityLayer.Models;

namespace TmdbMovieService.BusinessLayer.Mapping
{
    public class MoviesMapping : Profile
    {
        public MoviesMapping()
        {
            //CreateMap<MovieDTO, Movie>()
            //    .ReverseMap();
            CreateMap<MovieDTO.Result, Movie>()
                .ReverseMap();
        }

    }
}
