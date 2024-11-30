using AutoMapper;
using GA.Domain.Dtos.Movies;
using GA.Domain.Models.MoviesAggregation;

namespace GA.Application.Profiles
{
    internal class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieDto, Movie>().ReverseMap();
        }
    }
}
