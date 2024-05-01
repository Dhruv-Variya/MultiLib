using _NET.Dtos.Movie;
using _NET.Dtos.movieDto;
using _NET.models;

namespace _NET.Services.movieManagementService
{
    public interface IMovieManagementService
    {
        Task<ServiceResponse<List<getMovieDto>>> GetAllMovies();
        Task<ServiceResponse<getMovieDto>> GetMovieById(int id);
        Task<ServiceResponse<List<getMovieDto>>> AddMovies(addMovieDto newMovie);
        Task<ServiceResponse<getMovieDto>> UpdateMovie(updateMovieDto updatedMovie);
        Task<ServiceResponse<getMovieDto>> DeleteMovieById(int id);
        Task<ServiceResponse<IEnumerable<object>>> GetMovieAnalysisData();
        Task<ServiceResponse<IEnumerable<object>>> GetMovieAnalysisDataById(int id);

        

    }
}
