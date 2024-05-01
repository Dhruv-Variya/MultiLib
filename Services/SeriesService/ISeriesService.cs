﻿using _NET.Dtos.seriesDto;
using _NET.models;

namespace _NET.Services.SeriesService
{
    public interface ISeriesService
    {
        Task<ServiceResponse<List<getSeriesDto>>> GetAllSeries();
        Task<ServiceResponse<getSeriesDto>> GetSeriesById(int id);

        Task<ServiceResponse<List<getSeriesDto>>> AddSeries(addSeriesDto newSeries);
        Task<ServiceResponse<getSeriesDto>> UpdateSeries(updateSeriesDto updatedSeries);
        Task<ServiceResponse<getSeriesDto>> DeleteSeriesById(int id);
        Task<ServiceResponse<string>> AddSeasonsAndEpisodes(List<addSeasonDto> seasonsList);

        Task<ServiceResponse<seasonsModel>> UpdateSeason(updateSeasonDto updatedSeason);
        Task<ServiceResponse<episodesModel>> UpdateEpisode(updateEpisodeDto updatedEpisode);








    }
}
