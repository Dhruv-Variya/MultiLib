using System.ComponentModel.DataAnnotations;

namespace MultiLib.models
{
    public class episodesModel
    {
        [Key]
        public int id { get; set; }
        public string? itemCode { get; set; }
        public int? seasonNumber { get; set; }
        public int? episodeNumber { get; set; }
        public string? episodeName { get; set; }
        public string? episodeDescription { get; set; }
        public string? episodeRating { get; set; }
        public string? episodeReleaseDate { get; set; }
        public string? poster { get; set; }

    }
}
