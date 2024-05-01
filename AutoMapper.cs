using _NET.Dtos.Movie;
using _NET.Dtos.movieDto;
using _NET.Dtos.seriesDto;
using _NET.Dtos.User;
using _NET.models;
using AutoMapper;
using models;

namespace _NET
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Movie, GetMovieDto>();
            CreateMap<AddMovieDto, Movie>();
            CreateMap<UpdateMovieDto, Movie>();
            CreateMap<AuthUserDto,User>();
            CreateMap<User, RegisterUserDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, GetUserDto>();



            CreateMap<movieModel, getMovieDto>();
            CreateMap<addMovieDto, movieModel>();
            CreateMap<updateMovieDto, movieModel>();

            CreateMap<seriesModel, getSeriesDto>();
            CreateMap<addSeriesDto, seriesModel>();
            CreateMap<updateSeriesDto, seriesModel>();

            CreateMap<episodesModel, getEpisodeDto> ();
        }
    }
}
 