using AutoMapper;
using MultiLib.Dtos.Movie;
using MultiLib.Dtos.movieDto;
using MultiLib.Dtos.seriesDto;
using MultiLib.Dtos.User;
using MultiLib.models;

namespace MultiLib
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Movie, GetMovieDto>();
            CreateMap<AddMovieDto, Movie>();
            CreateMap<UpdateMovieDto, Movie>();
            CreateMap<AuthUserDto, User>();
            CreateMap<User, RegisterUserDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, GetUserDto>();



            CreateMap<movieModel, getMovieDto>();
            CreateMap<addMovieDto, movieModel>();
            CreateMap<updateMovieDto, movieModel>();

            CreateMap<seriesModel, getSeriesDto>();
            CreateMap<addSeriesDto, seriesModel>();
            CreateMap<updateSeriesDto, seriesModel>();

            CreateMap<episodesModel, getEpisodeDto>();
        }
    }
}
