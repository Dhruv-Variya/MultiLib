using AutoMapper;
using MainService.Dtos.movieDto;
using MainService.Dtos.seriesDto;

using MainService.models;

namespace MainService
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
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
