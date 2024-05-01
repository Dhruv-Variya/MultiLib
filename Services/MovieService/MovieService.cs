using _NET.Data;
using _NET.Dtos.Movie;
using _NET.models;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using models;

namespace _NET.Services.MovieService
{
    public class MovieService : IMovieService
    {
        private static List<Movie> movies = new List<Movie>
        {
            new Movie(),
            new Movie { Id = 1,Title = "1" }
        };
       
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;


        public MovieService(IMapper mapper, DataContext context, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _context = context;
            _environment = environment;   
        }


        public async Task<ServiceResponse<List<GetMovieDto>>> AddMovies(AddMovieDto newMovie)
        {
            var serviceResponse = new ServiceResponse<List<GetMovieDto>>();
            var movie = _mapper.Map<Movie>(newMovie);

            // movie poster add 
            byte[] imageBytes = Convert.FromBase64String(newMovie.Image);

            string Filepath = _environment.WebRootPath + "\\Upload\\Movie\\" + movie.Title;
            if (!System.IO.Directory.Exists(Filepath))
            {
                System.IO.Directory.CreateDirectory(Filepath);
            }
            string imagepath = Filepath + "\\" + movie.Title + ".png";
            if (!System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            File.WriteAllBytes(imagepath, imageBytes);
            string Imageurl = string.Empty;
            movie.MoviePoster = imageBytes;


            // return service response
            serviceResponse.Message = "Success";
            serviceResponse.Success = true;
            _context.movies.Add(movie);
            serviceResponse.Data = movies.Select(c => _mapper.Map<GetMovieDto>(c)).ToList();
            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetMovieDto>>> DeleteMovieById(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetMovieDto>>();


            try
            {
                var dbMovies = await _context.movies.FirstOrDefaultAsync(c => c.Id == id);
                //var movie = movies.First(c => c.Id == id);
                if (dbMovies is null)
                {
                    throw new Exception($"Movie with ID '{id}' not found");
                }
                
                string Filepath = _environment.WebRootPath + "\\Upload\\Movie\\" + dbMovies.Title;
                string imagepath = Filepath + "\\" + dbMovies.Title + ".png";
                _context.movies.Remove(dbMovies);

                serviceResponse.Data = movies.Select(c => _mapper.Map<GetMovieDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetMovieDto>>> GetAllMovies()
        {
            var serviceResponse = new ServiceResponse<List<GetMovieDto>>();

            var dbMovies =  await _context.movies.ToListAsync();
            
            serviceResponse.Success = true;
            serviceResponse.Message = "succsess";
            serviceResponse.Data = dbMovies.Select(c => _mapper.Map<GetMovieDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetMovieDto>> GetMovieById(int id)
        {
            var serviceResponse = new ServiceResponse<GetMovieDto>();
            var dbMovies = await _context.movies.FirstOrDefaultAsync(c => c.Id == id);
            if (dbMovies == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Movie with ID {id} not found";
            }
            else
            {
                serviceResponse.Success = true;
                serviceResponse.Message = "succsess";
                // var movie= movies.FirstOrDefault(c => c.Id == id);
                serviceResponse.Data = _mapper.Map<GetMovieDto>(dbMovies);
            }
            
            return serviceResponse;


        }

        public async Task<ServiceResponse<GetMovieDto>> UpdateMovie(UpdateMovieDto updatedMovie)
        {
            var serviceResponse = new ServiceResponse<GetMovieDto>();


            try
            {
                var dbMovies = await _context.movies.FirstOrDefaultAsync(c => c.Id == updatedMovie.Id);
                //var movie = movies.FirstOrDefault(c => c.Id == updatedMovie.Id);
                if(dbMovies == null)
                {
                    throw new Exception($"Movie with ID '{updatedMovie.Id}' not found");
                }


                // movie poster add functionality
                byte[] imageBytes = Convert.FromBase64String(updatedMovie.Image);

                string Filepath = _environment.WebRootPath + "\\Upload\\Movie\\" + dbMovies.Title;
                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }
                string imagepath = Filepath + "\\" + dbMovies.Title + ".png";
                if (!System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                File.WriteAllBytes(imagepath, imageBytes);
                string Imageurl = string.Empty;
                dbMovies.MoviePoster = imageBytes;



                //string Imageurl = string.Empty;
                //Imageurl = hosturl + "/Upload/Movie/" + updatedMovie.Title + "/" + updatedMovie.Title + ".png";

               
                serviceResponse.Success = true;
                //serviceResponse.Message = Imageurl;

                _mapper.Map(updatedMovie, dbMovies);
               // movie.Title = updatedMovie.Title;
               // movie.ReleaseDate = updatedMovie.ReleaseDate;
               // movie.Genre = updatedMovie.Genre;
               // movie.Cast = updatedMovie.Cast;
               // movie.Lang = updatedMovie.Lang;
               // movie.Rating = updatedMovie.Rating;
               // movie.TreailerURL = updatedMovie.TreailerURL;

                serviceResponse.Data = _mapper.Map<GetMovieDto>(dbMovies);
            }catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            await _context.SaveChangesAsync();
            return serviceResponse;

        }

  
    }
}
