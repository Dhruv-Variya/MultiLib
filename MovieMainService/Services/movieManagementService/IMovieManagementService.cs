using MovieMainService.Dtos.Movie;
using MovieMainService.Dtos.movieDto;
using MovieMainService.models;

namespace MovieMainService.Services.movieManagementService
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
