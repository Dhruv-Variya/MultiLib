
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MainService.models;
using MainService.Dtos.movieDto;
using MainService.Data;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace MainService.Services.movieManagementService
{
    public class movieManagementService : IMovieManagementService
    {
        private static List<getMovieDto> movies = new List<getMovieDto>
        {
            new getMovieDto { movieId = 1},
            new getMovieDto { movieId = 2},
            new getMovieDto { movieId = 3},
            new getMovieDto { movieId = 4}
        };

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;


        public movieManagementService(IMapper mapper, DataContext context, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _context = context;
            _environment = environment;
        }


        #region get all movies

        public async Task<ServiceResponse<List<getMovieDto>>> GetAllMovies()
        {
            var serviceResponse = new ServiceResponse<List<getMovieDto>>();

            try
            {
                serviceResponse.Data = new List<getMovieDto>();
                var dbMovies = await _context.movieStorage.ToListAsync();

                // Fetch categories for each movie
                foreach (var movie in dbMovies)
                {
                    var categoryIds = await _context.itemCatagories
                        .Where(ic => ic.itemCode == movie.movieCode)
                        .Select(ic => ic.catagoryId)
                        .ToListAsync();

                    var categories = await _context.Catagory
                        .Where(c => categoryIds.Contains(c.catagoryId))
                        .Select(c => c.CatagoryName)
                        .ToListAsync();

                    var languageIds = await _context.itemLanguages
                        .Where(ic => ic.itemCode == movie.movieCode)
                        .Select(ic => ic.languageId)
                        .ToListAsync();

                    var languages = await _context.languages
                        .Where(c => languageIds.Contains(c.languageId))
                        .Select(c => c.languageName)
                        .ToListAsync();

                    var dto = _mapper.Map<getMovieDto>(movie);
                    dto.categories = categories;
                    dto.languages = languages;

                    serviceResponse.Data.Add(dto);
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

        #region get movie by id
        public async Task<ServiceResponse<getMovieDto>> GetMovieById(int id)
        {
            var serviceResponse = new ServiceResponse<getMovieDto>();
            if (id == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Movie Id Reuired!!! It can not be Zero(0)";
                return serviceResponse;
            }
            var dbMovies = await _context.movieStorage.FirstOrDefaultAsync(c => c.movieId == id);
            if (dbMovies == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Movie with ID {id} not found";
                return serviceResponse;
            }
                try
                {
                    var categoryIds = await _context.itemCatagories
                       .Where(ic => ic.itemCode == dbMovies.movieCode)
                       .Select(ic => ic.catagoryId)
                       .ToListAsync();

                    var categories = await _context.Catagory
                        .Where(c => categoryIds.Contains(c.catagoryId))
                        .Select(c => c.CatagoryName)
                        .ToListAsync();

                    var languageIds = await _context.itemLanguages
                        .Where(ic => ic.itemCode == dbMovies.movieCode)
                        .Select(ic => ic.languageId)
                        .ToListAsync();

                    var languages = await _context.languages
                        .Where(c => languageIds.Contains(c.languageId))
                        .Select(c => c.languageName)
                        .ToListAsync();

                    var dto = _mapper.Map<getMovieDto>(dbMovies);
                    dto.categories = categories;
                    dto.languages = languages;

                    serviceResponse.Data = dto;
                    serviceResponse.Success = true;
                    serviceResponse.Message = "succsess";
                }
                catch(Exception ex)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = ex.Message;
                }
            return serviceResponse;

        }
        #endregion

        #region Add Movie
        public async Task<ServiceResponse<List<getMovieDto>>> AddMovies(addMovieDto newMovie)
        {
            var serviceResponse = new ServiceResponse<List<getMovieDto>>();
            var validate = ValidationsAddMovie(newMovie);
            // Check if the movie code already exists

            if (await CheckMovieCodeExistAsync(newMovie.movieCode))
            {
                serviceResponse.Message = "Movie Already Exist Please Update Movie Code";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            // validate parameters
            else if (!string.IsNullOrEmpty(validate))
            {
                serviceResponse.Message = validate.ToString();
                serviceResponse.Success = false;
            }
            else
            {
                var movie = _mapper.Map<movieModel>(newMovie);
                try
                {
                    _context.movieStorage.Add(movie);
                    await _context.SaveChangesAsync(); // Save changes in db

                    // Add a record to the analysis table
                    var analysisRecord = new analysis
                    {
                        itemCode = movie.movieCode,
                        itemType = "m",
                        timesClicked = 0,
                        upVote = 0,
                        downVote = 0,
                        trailerReach = 0,
                    };

                    _context.analysis.Add(analysisRecord);

                    // Add records to the itemCategories table
                    foreach (var categoryId in newMovie.movieCategoryIds)
                    {
                        var itemCategoryRecord = new itemCatagories
                        {
                            itemCode = movie.movieCode,
                            catagoryId = categoryId
                        };
                        _context.itemCatagories.Add(itemCategoryRecord);
                    }
                    // Add records to the itemlanguages table
                    foreach (var languageId in newMovie.movieLanguagesIds)
                    {
                        var itemLanguageRecord = new itemLanguages
                        {
                            itemCode = movie.movieCode,
                            languageId = languageId
                        };
                        _context.itemLanguages.Add(itemLanguageRecord);
                    }

                    await _context.SaveChangesAsync();

                    serviceResponse.Success = true;
                    serviceResponse.Message = "Success";
                    serviceResponse.Data = new List<getMovieDto> { _mapper.Map<getMovieDto>(movie) };
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    serviceResponse.Success = false;
                    serviceResponse.Message = ex.Message;
                }
            }
            

            return serviceResponse;
        }


        #endregion

        #region update movie 

        public async Task<ServiceResponse<getMovieDto>> UpdateMovie(updateMovieDto updatedMovie)
        {
            var serviceResponse = new ServiceResponse<getMovieDto>();
            var validate = ValidationsUpdateMovie(updatedMovie);
            try
            {
                
                // Get the movie entity based on the provided movie ID
                var dbMovie = await _context.movieStorage.FirstOrDefaultAsync(c => c.movieCode == updatedMovie.movieCode);
                if (dbMovie == null)
                {
                    serviceResponse.Message = $"Movie with ID '{updatedMovie.movieCode}' not found";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }
                // validate parameters
                else if (!string.IsNullOrEmpty(validate))
                {
                    serviceResponse.Message = validate.ToString();
                    serviceResponse.Success = false;
                }
                else
                {

                    // Update movie details
                    _mapper.Map(updatedMovie, dbMovie);

                    // Remove existing categories associated with the updated movieCode
                    var existingCategories = await _context.itemCatagories
                        .Where(ic => ic.itemCode == updatedMovie.movieCode)
                        .ToListAsync();

                    _context.itemCatagories.RemoveRange(existingCategories);


                    // Remove existing languages associated with the updated movieCode
                    var existingLanguages = await _context.itemLanguages
                        .Where(ic => ic.itemCode == updatedMovie.movieCode)
                        .ToListAsync();

                    _context.itemLanguages.RemoveRange(existingLanguages);

                    // Add new categories to itemCatagories table
                    foreach (var categoryId in updatedMovie.movieCategoryIds)
                    {
                        var itemCategory = new itemCatagories
                        {
                            itemCode = updatedMovie.movieCode,
                            catagoryId = categoryId
                        };
                        _context.itemCatagories.Add(itemCategory);
                    }
                    // Add records to the itemlanguages table
                    foreach (var languageId in updatedMovie.movieLanguagesIds)
                    {
                        var itemLanguageRecord = new itemLanguages
                        {
                            itemCode = updatedMovie.movieCode,
                            languageId = languageId
                        };
                        _context.itemLanguages.Add(itemLanguageRecord);
                    }

                    serviceResponse.Success = true;
                    serviceResponse.Data = _mapper.Map<getMovieDto>(dbMovie);
                }

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

        #region delete Movie
        public async Task<ServiceResponse<getMovieDto>> DeleteMovieById(int id)
        {
            var serviceResponse = new ServiceResponse<getMovieDto>();


            try
            {
                var dbMovies = await _context.movieStorage.FirstOrDefaultAsync(c => c.movieId == id);
                //var movie = movies.First(c => c.Id == id);
                if (dbMovies is null)
                {
                    throw new Exception($"Movie with ID '{id}' not found");
                }
                _context.movieStorage.Remove(dbMovies);
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

        #region movies with analysis data
        public async Task<ServiceResponse<IEnumerable<object>>> GetMovieAnalysisData()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var query = from movie in _context.movieStorage
                            join analysis in _context.analysis
                            on movie.movieCode equals analysis.itemCode into analysisGroup
                            from analysisData in analysisGroup.DefaultIfEmpty()
                            select new
                            {
                                MovieId = movie.movieId,
                                movie.movieCode,
                                MovieTitle = movie.movieTitle,
                                TimesClicked = analysisData != null ? analysisData.timesClicked : null,
                                TimesTrailerWatched = analysisData != null ? analysisData.trailerReach : null,
                                upvote = analysisData != null ? analysisData.upVote : null,
                                downvote = analysisData != null ? analysisData.downVote : null,
                            };

                var result = await query.ToListAsync();
                serviceResponse.Data = result;
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

        #region movies with analysis data by movie id
        public async Task<ServiceResponse<IEnumerable<object>>> GetMovieAnalysisDataById(int Id)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var query = from movie in _context.movieStorage
                            join analysis in _context.analysis
                            on movie.movieCode equals analysis.itemCode into analysisGroup
                            from analysisData in analysisGroup.DefaultIfEmpty()
                            where movie.movieId == Id
                            select new
                            {
                                MovieId = movie.movieId,
                                movie.movieCode,
                                MovieTitle = movie.movieTitle,
                                TimesClicked = analysisData != null ? analysisData.timesClicked : null,
                                TimesTrailerWatched = analysisData != null ? analysisData.trailerReach : null,
                                upvote = analysisData != null ? analysisData.upVote : null,
                                downvote = analysisData != null ? analysisData.downVote : null,
                            };

                var result = await query.ToListAsync();
                serviceResponse.Data = result;
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

        #region other Functions
        private async Task<bool> CheckMovieCodeExistAsync(string movieCode)
        {
            return await _context.movieStorage.AnyAsync(u => u.movieCode == movieCode);

        }

        private string ValidationsAddMovie(addMovieDto newMovie)
        {
            StringBuilder sb = new StringBuilder();
            
            if (newMovie.movieTitle is null || newMovie.movieTitle.Equals("string"))
                sb.Append("movieTitle Required!!!" + Environment.NewLine);
            if (newMovie.movieCast is null || newMovie.movieCast.Equals("string"))
                sb.Append("movieCast Required!!!" + Environment.NewLine);
            if (newMovie.movieRating is null || newMovie.movieRating.Equals("string"))
                sb.Append("movieRating Required!!!" + Environment.NewLine);        
            if (newMovie.movieReleaseDate is null || newMovie.movieReleaseDate.Equals("string"))
                sb.Append("movieReleaseDate Required!!!" + Environment.NewLine);
            if (newMovie.movieTrailerURL is null || newMovie.movieTrailerURL.Equals("string"))
                sb.Append("movieTrailerURL Required!!!" + Environment.NewLine);
            if (newMovie.movieDescription is null || newMovie.movieDescription.Equals("string"))
                sb.Append("movieDescription Required!!!" + Environment.NewLine);
            if (newMovie.moviePoster is null || newMovie.moviePoster.Equals("string"))
                sb.Append("moviePoster Required!!!" + Environment.NewLine);
            if (newMovie.movieBackPoster is null || newMovie.movieBackPoster.Equals("string"))
                sb.Append("movieBackPoster Required!!!" + Environment.NewLine);
            if (newMovie.movieCategoryIds.Count == 0 || newMovie.movieCategoryIds.Contains(0))
                sb.Append("movieCategoryIds Required!!! OR it can not be ZERO(0)" + Environment.NewLine);
            if (newMovie.movieLanguagesIds.Count == 0 || newMovie.movieLanguagesIds.Contains(0))
                sb.Append("movieLanguagesIds Required!!! OR it can not be ZERO(0)" + Environment.NewLine);
            return sb.ToString();
        }

        private string ValidationsUpdateMovie(updateMovieDto updatedMovie)
        {
            StringBuilder sb = new StringBuilder();
            
            if (updatedMovie.movieTitle is null || updatedMovie.movieTitle.Equals("string"))
                sb.Append("movieTitle Required!!!" + Environment.NewLine);
            if (updatedMovie.movieCast is null || updatedMovie.movieCast.Equals("string"))
                sb.Append("movieCast Required!!!" + Environment.NewLine);
            if (updatedMovie.movieRating is null || updatedMovie.movieRating.Equals("string"))
                sb.Append("movieRating Required!!!" + Environment.NewLine);
            if (updatedMovie.movieReleaseDate is null || updatedMovie.movieReleaseDate.Equals("string"))
                sb.Append("movieReleaseDate Required!!!" + Environment.NewLine);
            if (updatedMovie.movieTrailerURL is null || updatedMovie.movieTrailerURL.Equals("string"))
                sb.Append("movieTrailerURL Required!!!" + Environment.NewLine);
            if (updatedMovie.movieDescription is null || updatedMovie.movieDescription.Equals("string"))
                sb.Append("movieDescription Required!!!" + Environment.NewLine);
            if (updatedMovie.moviePoster is null || updatedMovie.moviePoster.Equals("string"))
                sb.Append("moviePoster Required!!!" + Environment.NewLine);
            if (updatedMovie.movieBackPoster is null || updatedMovie.movieBackPoster.Equals("string"))
                sb.Append("movieBackPoster Required!!!" + Environment.NewLine);
            if (updatedMovie.movieCategoryIds.Count == 0 || updatedMovie.movieCategoryIds.Contains(0))
                sb.Append("movieCategoryIds Required!!! OR it can not be ZERO(0)" + Environment.NewLine);
            if (updatedMovie.movieLanguagesIds.Count == 0 || updatedMovie.movieLanguagesIds.Contains(0))
                sb.Append("movieLanguagesIds Required!!! OR it can not be ZERO(0)" + Environment.NewLine);
            return sb.ToString();
        }

        #endregion
    }
}

