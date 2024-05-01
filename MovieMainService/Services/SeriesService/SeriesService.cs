using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieMainService.models;
using MovieMainService.Data;
using MovieMainService.Dtos.seriesDto;

namespace MovieMainService.Services.SeriesService
{
    public class SeriesService : ISeriesService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;

        public SeriesService(IMapper mapper, DataContext context, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _context = context;
            _environment = environment;
        }

        // Example Seriese List
        private static List<getSeriesDto> serieses = new List<getSeriesDto>
        {
            new getSeriesDto { itemId = 1},
            new getSeriesDto { itemId = 2}
        };

        #region Get All Seriese
        public async Task<ServiceResponse<List<getSeriesDto>>> GetAllSeries()
        {
            var serviceResponse = new ServiceResponse<List<getSeriesDto>>();

            try
            {
                serviceResponse.Data = new List<getSeriesDto>();
                var seriesFromDb = await _context.series.ToListAsync();

                foreach (var seriesEntity in seriesFromDb)
                {
                    var seriesDto = _mapper.Map<getSeriesDto>(seriesEntity);
                    var seasonsFromDb = await _context.seasons
                        .Where(s => s.itemCode == seriesEntity.itemCode)
                        .ToListAsync();

                    seriesDto.NumberOfSeasons = seasonsFromDb.Count;
                    seriesDto.SeasonData = new List<getSeasonDto>();

                    foreach (var seasonEntity in seasonsFromDb)
                    {
                        var seasonDto = new getSeasonDto
                        {
                            SeasonNumber = seasonEntity.seasonNumber,
                            SeasonName = seasonEntity.seasonName,
                            TotalEpisodes = 0, // Placeholder value, to be updated
                            Episodes = new List<getEpisodeDto>()
                        };

                        var episodesFromDb = await _context.episodes
                            .Where(e => e.itemCode == seriesEntity.itemCode && e.seasonNumber == seasonEntity.seasonNumber)
                            .ToListAsync();

                        seasonDto.TotalEpisodes = episodesFromDb.Count;
                        seasonDto.Episodes = _mapper.Map<List<getEpisodeDto>>(episodesFromDb);


                        seriesDto.SeasonData.Add(seasonDto);
                    }

                    serviceResponse.Data.Add(seriesDto);
                }

                serviceResponse.Success = true;
                serviceResponse.Message = "Success";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
        #endregion

        #region Get Seriese By ID
        public async Task<ServiceResponse<getSeriesDto>> GetSeriesById(int id)
        {
            var serviceResponse = new ServiceResponse<getSeriesDto>();
            try
            {
                var seriesEntity = await _context.series.FirstOrDefaultAsync(c => c.itemId == id);
                if (seriesEntity == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Series with ID {id} not found";
                    return serviceResponse;
                }
                var seriesDto = _mapper.Map<getSeriesDto>(seriesEntity);
                var seasonsFromDb = await _context.seasons
                    .Where(s => s.itemCode == seriesEntity.itemCode)
                    .ToListAsync();
                seriesDto.NumberOfSeasons = seasonsFromDb.Count;
                seriesDto.SeasonData = new List<getSeasonDto>();

                foreach (var seasonEntity in seasonsFromDb)
                {
                    var seasonDto = new getSeasonDto
                    {
                        SeasonNumber = seasonEntity.seasonNumber,
                        SeasonName = seasonEntity.seasonName,
                        TotalEpisodes = 0,
                        Episodes = new List<getEpisodeDto>()
                    };


                    var episodesFromDb = await _context.episodes
                        .Where(e => e.itemCode == seriesEntity.itemCode && e.seasonNumber == seasonEntity.seasonNumber)
                        .ToListAsync();
                    seasonDto.Episodes = _mapper.Map<List<getEpisodeDto>>(episodesFromDb);
                    seasonDto.TotalEpisodes = episodesFromDb.Count;
                    seriesDto.SeasonData.Add(seasonDto);
                }

                var categoryIds = await _context.itemCatagories
                    .Where(ic => ic.itemCode == seriesEntity.itemCode)
                    .Select(ic => ic.catagoryId)
                    .ToListAsync();

                var categories = await _context.Catagory
                    .Where(c => categoryIds.Contains(c.catagoryId))
                    .Select(c => c.CatagoryName)
                    .ToListAsync();

                var languageIds = await _context.itemLanguages
                    .Where(ic => ic.itemCode == seriesEntity.itemCode)
                    .Select(ic => ic.languageId)
                    .ToListAsync();

                var languages = await _context.languages
                    .Where(l => languageIds.Contains(l.languageId))
                    .Select(l => l.languageName)
                    .ToListAsync();


                seriesDto.categories = categories;
                seriesDto.languages = languages;


                serviceResponse.Data = seriesDto;
                serviceResponse.Success = true;
                serviceResponse.Message = "Success";
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
        #endregion

        #region Add Seriese
        public async Task<ServiceResponse<List<getSeriesDto>>> AddSeries(addSeriesDto newSeries)
        {
            var serviceResponse = new ServiceResponse<List<getSeriesDto>>();
            var series = _mapper.Map<seriesModel>(newSeries);

            // Check if the movie code already exists
            if (await CheckSeriesCodeExistAsync(newSeries.itemCode))
            {
                serviceResponse.Message = "Movie Code Already Exist";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            try
            {
                _context.series.Add(series);
                await _context.SaveChangesAsync();

                // Add a record to the analysis table
                var analysisRecord = new analysis
                {
                    itemCode = series.itemCode,
                    itemType = "m",
                    timesClicked = 0,
                    upVote = 0,
                    downVote = 0,
                    trailerReach = 0,
                };

                _context.analysis.Add(analysisRecord);

                // Add records to the itemCategories table
                foreach (var categoryId in newSeries.seriesCategoryIds)
                {
                    var itemCategoryRecord = new itemCatagories
                    {
                        itemCode = series.itemCode,
                        catagoryId = categoryId
                    };
                    _context.itemCatagories.Add(itemCategoryRecord);
                }
                // Add records to the itemlanguages table
                foreach (var languageId in newSeries.seriesLanguagesIds)
                {
                    var itemLanguageRecord = new itemLanguages
                    {
                        itemCode = series.itemCode,
                        languageId = languageId
                    };
                    _context.itemLanguages.Add(itemLanguageRecord);
                }

                await _context.SaveChangesAsync();

                serviceResponse.Success = true;
                serviceResponse.Message = "Success";
                serviceResponse.Data = new List<getSeriesDto> { _mapper.Map<getSeriesDto>(series) };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
        #endregion

        #region Update Seriese Data
        public async Task<ServiceResponse<getSeriesDto>> UpdateSeries(updateSeriesDto updatedSeries)
        {
            var serviceResponse = new ServiceResponse<getSeriesDto>();

            try
            {
                var series = await _context.series.FirstOrDefaultAsync(c => c.itemCode == updatedSeries.itemCode);
                if (series == null)
                {
                    throw new Exception($"Movie with ID '{updatedSeries.itemCode}' not found");
                }

                _mapper.Map(updatedSeries, series);

                var existingCategories = await _context.itemCatagories
                    .Where(ic => ic.itemCode == updatedSeries.itemCode)
                    .ToListAsync();

                _context.itemCatagories.RemoveRange(existingCategories);

                var existingLanguages = await _context.itemLanguages
                    .Where(ic => ic.itemCode == updatedSeries.itemCode)
                    .ToListAsync();

                _context.itemLanguages.RemoveRange(existingLanguages);

                foreach (var categoryId in updatedSeries.seriesCategoryIds)
                {
                    var itemCategory = new itemCatagories
                    {
                        itemCode = updatedSeries.itemCode,
                        catagoryId = categoryId
                    };
                    _context.itemCatagories.Add(itemCategory);
                }
                foreach (var languageId in updatedSeries.seriesLanguagesIds)
                {
                    var itemLanguageRecord = new itemLanguages
                    {
                        itemCode = updatedSeries.itemCode,
                        languageId = languageId
                    };
                    _context.itemLanguages.Add(itemLanguageRecord);
                }
                serviceResponse.Success = true;
                serviceResponse.Data = _mapper.Map<getSeriesDto>(series);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            await _context.SaveChangesAsync();

            return serviceResponse;
        }
        #endregion

        #region Delete Seriese By Id
        public async Task<ServiceResponse<getSeriesDto>> DeleteSeriesById(int id)
        {
            var serviceResponse = new ServiceResponse<getSeriesDto>();
            try
            {
                var dbSeries = await _context.series.FirstOrDefaultAsync(c => c.itemId == id);
                if (dbSeries is null)
                {
                    throw new Exception($"Movie with ID '{id}' not found");
                }
                _context.series.Remove(dbSeries);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            await _context.SaveChangesAsync();
            return serviceResponse;
        }
        #endregion

        #region Add Season And Episodes
        public async Task<ServiceResponse<string>> AddSeasonsAndEpisodes(List<addSeasonDto> seasonsList)
        {
            var serviceResponse = new ServiceResponse<string>();

            try
            {
                foreach (var seasonDto in seasonsList)
                {
                    // Check if the series exists
                    var existingSeries = await _context.series.FirstOrDefaultAsync(s => s.itemCode == seasonDto.itemCode);

                    if (existingSeries == null)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = $"Series with item code {seasonDto.itemCode} does not exist.";
                        return serviceResponse;
                    }

                    // Add the season
                    var seasonEntity = new seasonsModel
                    {
                        itemCode = seasonDto.itemCode,
                        seasonNumber = seasonDto.seasonNumber,
                        seasonName = seasonDto.seasonName,
                        // Map other properties from seasonDto to seasonEntity as needed
                    };
                    _context.seasons.Add(seasonEntity);

                    // Add episodes for the season
                    foreach (var episodeDto in seasonDto.Episodes)
                    {
                        var episodeEntity = new episodesModel
                        {
                            itemCode = seasonDto.itemCode,
                            seasonNumber = seasonDto.seasonNumber,
                            episodeNumber = episodeDto.episodeNumber,
                            episodeName = episodeDto.episodeName,
                            episodeDescription = episodeDto.episodeDescription,
                            episodeRating = episodeDto.episodeRating,
                            episodeReleaseDate = episodeDto.episodeReleaseDate,
                            poster = episodeDto.poster,
                        };
                        _context.episodes.Add(episodeEntity);
                    }
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                serviceResponse.Success = true;
                serviceResponse.Message = "Seasons and episodes added successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"An error occurred: {ex.Message}";
            }

            return serviceResponse;
        }
        #endregion

        #region update Season
        public Task<ServiceResponse<seasonsModel>> UpdateSeason(updateSeasonDto updatedSeason)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region update Episode
        public Task<ServiceResponse<episodesModel>> UpdateEpisode(updateEpisodeDto updatedEpisode)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Other Functions
        private async Task<bool> CheckSeriesCodeExistAsync(string itemCode)
        {
            return await _context.series.AnyAsync(u => u.itemCode == itemCode);

        }


        #endregion
    }
}
