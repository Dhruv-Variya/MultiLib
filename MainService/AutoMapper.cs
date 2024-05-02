using AutoMapper;
using MainService.Dtos.movieDto;
using MainService.Dtos.seriesDto;
using MainService.Dtos.User;
using MainService.models;

namespace MainService
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
           
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
