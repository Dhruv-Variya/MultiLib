﻿using Microsoft.AspNetCore.Mvc;
using MainService.models;
using MainService.Services.movieManagementService;
using MainService.Dtos.movieDto;
using MainService.Data;

namespace MainService.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class movieManagementController : Controller
    {
        private readonly DataContext _context;
        private readonly IMovieManagementService _movieService;
        private readonly IWebHostEnvironment _environment;


        public movieManagementController(IMovieManagementService movieService, IWebHostEnvironment environment, DataContext context)
        {
            _movieService = movieService;
            _environment = environment;
            _context = context;
        }
        //get all movies
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<getMovieDto>>>> GetAllMovies()
        {
            var response = await _movieService.GetAllMovies();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        //get all movies with analysis data
        [HttpGet("MoviesWithAnalysisData")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<object>>>> GetMovieAnalysisData()
        {
            return Ok(await _movieService.GetMovieAnalysisData());
        }

        //get spacific movie with analysis data
        [HttpGet("MoviesWithAnalysisDataById/{id}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<object>>>> GetMovieAnalysisDataById(int id)
        {
            return Ok(await _movieService.GetMovieAnalysisDataById(id));
        }


        // get movie by id
        [HttpGet("GetMovieById/{id}")]
        public async Task<ActionResult<ServiceResponse<getMovieDto>>> GetMovieById(int id)
        {
            return Ok(await _movieService.GetMovieById(id));
        }


        //Add Movie
        [HttpPost("AddMovie")]
        public async Task<ActionResult<ServiceResponse<List<getMovieDto>>>> AddMovie(addMovieDto newMovie)
        {
            return Ok(await _movieService.AddMovies(newMovie));
        }


        //update details of movie
        [HttpPut("UpdateMovie")]
        public async Task<ActionResult<ServiceResponse<List<getMovieDto>>>> UpdateMovie(updateMovieDto updatedMovie)
        {
            var response = await _movieService.UpdateMovie(updatedMovie);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        //delete spacific movie
        [HttpDelete("DleteMovie/{id}")]
        public async Task<ActionResult<ServiceResponse<getMovieDto>>> DeleteMovie(int id)
        {
            var response = await _movieService.DeleteMovieById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
