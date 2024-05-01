using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace MovieMainService.models
{
    public class movieModel
    {
        [Key]
        public int movieId { get; set; }
        public string? movieCode { get; set; }
        public string? movieTitle { get; set; } = "Name";
        public string? movieCast { get; set; } = "John Snow, David";
        public string? movieRating { get; set; } = "10";
        public string? movieReleaseDate { get; set; } = "01/01/2024";
        public string? movieTrailerURL { get; set; } = "youtube.com";
        public string? movieDescription { get; set; }
        public string? moviePoster { get; set; }
        public string? movieBackPoster { get; set; }
        public bool? isUpcoming { get; set; }


    }
}