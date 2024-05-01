﻿namespace MultiLib.Dtos.Movie
{
    public class GetMovieDto
    {
        public int Id { get; set; }
        public string? Title { get; set; } = "Movie";

        public DateTime? ReleaseDate { get; set; } = DateTime.Now;

        public string? Genre { get; set; } = "Action";
        public string? Cast { get; set; }
        public string? Lang { get; set; }
        public float? Rating { get; set; } = 9;
        public string? TreailerURL { get; set; } = "youtube.com";
        public string? Description { get; set; }
        public byte[]? MoviePoster { get; set; }



    }
}
