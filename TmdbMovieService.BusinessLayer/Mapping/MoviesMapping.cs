using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovie.DataAccessLayer.DTO_s;
using TmdbMovie.DataAccessLayer.Models;
using TmdbMovieService.EntityLayer.Models;

namespace TmdbMovieService.BusinessLayer.Mapping
{
    public class MoviesMapping : Profile
    {
        public MoviesMapping()
        {
            CreateMap<MovieDTO.Result, Movie>()
                .ForMember(i => i.genre_ids, opt => opt.MapFrom(i => i.genre_id))
                .ReverseMap();
            CreateMap<MovieResponseModel.Result, Movie>()
                .ReverseMap();
        }

    }
}
