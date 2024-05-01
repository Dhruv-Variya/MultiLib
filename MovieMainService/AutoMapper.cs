using AutoMapper;
using MovieMainService.Dtos.Movie;
using MovieMainService.Dtos.movieDto;
using MovieMainService.Dtos.seriesDto;
using MovieMainService.Dtos.User;
using MovieMainService.models;

namespace MovieMainService
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
