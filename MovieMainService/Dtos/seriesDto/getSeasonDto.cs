using MovieMainService.models;

namespace MovieMainService.Dtos.seriesDto
{

    public class getSeasonDto
    {
        public int? SeasonNumber { get; set; }
        public string? SeasonName { get; set; }
        public int? TotalEpisodes { get; set; }
        public List<getEpisodeDto>? Episodes { get; set; }
    }
}
