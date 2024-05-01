namespace _NET.Dtos.movieDto
{
    public class getMovieDto
    {
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

        public List<string>? categories { get; set; }

        public List<string>? languages { get; set; }

    }
}
