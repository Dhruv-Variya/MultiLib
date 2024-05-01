using MainService.Data;
using MainService.Dtos.Movie;
using MainService.models;
using MainService.Services.MovieService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MainService.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly DataContext _context;
        //interface
        private readonly IMovieService _movieService;
        // constructor to add services in here

        private readonly IWebHostEnvironment _environment;
        public MovieController(IMovieService movieService, IWebHostEnvironment environment, DataContext context)
        {
            _movieService = movieService;
            _environment = environment;
            _context = context;
        }


        //task : asyncronous operation
        //serviceresponse : generics
        //getmoviedto,addmoviedto,updatemoviedto : dto

        // get all movies
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetMovieDto>>>> Get()
        {
            return Ok(await _movieService.GetAllMovies());
        }

        //get one movie
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetMovieDto>>> GetSingle(int id)
        {
            return Ok(await _movieService.GetMovieById(id));
        }

        //add new movie
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetMovieDto>>>> AddMovie(AddMovieDto newMovie)
        {
            return Ok(await _movieService.AddMovies(newMovie));
        }


        //update details of movie
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetMovieDto>>>> UpdateMovie(UpdateMovieDto updatedMovie)
        {
            var response = await _movieService.UpdateMovie(updatedMovie);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        //delete spacific movie
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetMovieDto>>> DeleteMovie(int id)
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
