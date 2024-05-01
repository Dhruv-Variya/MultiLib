using _NET.Data;
using _NET.Dtos.seriesDto;
using _NET.models;
using _NET.Services.movieManagementService;
using _NET.Services.SeriesService;
using Microsoft.AspNetCore.Mvc;

namespace _NET.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SeriesController : Controller
    {
        private readonly DataContext _context;
        private readonly ISeriesService _serieseService;
        private readonly IWebHostEnvironment _environment;


        public SeriesController(ISeriesService seriesService, IWebHostEnvironment environment, DataContext context)
        {
            _serieseService = seriesService;
            _environment = environment;
            _context = context;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<getSeriesDto>>>> GetAllSeries()
        {
            return Ok(await _serieseService.GetAllSeries());
        }

        [HttpGet("GetseriesById/{id}")]
        public async Task<ActionResult<ServiceResponse<getSeriesDto>>> GetSeriesById(int id)
        {
            return Ok(await _serieseService.GetSeriesById(id));
        }

        //Add Movie
        [HttpPost("AddSeries")]
        public async Task<ActionResult<ServiceResponse<List<getSeriesDto>>>> AddSeriese(addSeriesDto newSeries)
        {
            return Ok(await _serieseService.AddSeries(newSeries));
        }


        //update details of Seriese
        [HttpPut("UpdateSeries")]
        public async Task<ActionResult<ServiceResponse<List<getSeriesDto>>>> UpdateSeriese(updateSeriesDto updatedSeries)
        {
            var response = await _serieseService.UpdateSeries(updatedSeries);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        //update details of movie
        [HttpPut("UpdateSeason")]
        public async Task<ActionResult<ServiceResponse<List<seasonsModel>>>> UpdateSeason(updateSeasonDto updatedSeason)
        {
            var response = await _serieseService.UpdateSeason(updatedSeason);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        //update details of movie
        [HttpPut("UpdateEpisode")]
        public async Task<ActionResult<ServiceResponse<List<episodesModel>>>> UpdateEpisode(updateEpisodeDto updatedEpisode)
        {
            var response = await _serieseService.UpdateEpisode(updatedEpisode);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        //delete spacific movie
        [HttpDelete("DleteSeries/{id}")]
        public async Task<ActionResult<ServiceResponse<getSeriesDto>>> DeleteSeries(int id)
        {
            var response = await _serieseService.DeleteSeriesById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost("AddSeriesAndEpisodes")]
        public async Task<ActionResult<ServiceResponse<string>>> AddSeasonsAndEpisodes(List<addSeasonDto> seasonsList)
        {
            return Ok(await _serieseService.AddSeasonsAndEpisodes(seasonsList));
        }
        

    }
}
