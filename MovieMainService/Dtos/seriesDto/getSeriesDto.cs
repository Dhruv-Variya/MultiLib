namespace MainService.Dtos.seriesDto
{
    public class getSeriesDto
    {
        public int itemId { get; set; }
        public string? itemCode { get; set; }
        public string? itemTitle { get; set; } = "Name";
        public string? itemType { get; set; } = "Name";
        public string? itemCast { get; set; } = "John Snow, David";
        public string? itemRating { get; set; } = "10";
        public string? itemReleaseDate { get; set; } = "01/01/2024";
        public string? itemTrailerURL { get; set; } = "youtube.com";
        public string? itemDescription { get; set; }
        public string? itemPoster { get; set; }
        public string? itemBackPoster { get; set; }
        public int? itemSeasons { get; set; }
        public bool? isUpcoming { get; set; }
        public List<string>? categories { get; set; }

        public List<string>? languages { get; set; }
        public int NumberOfSeasons { get; set; }
        public List<getSeasonDto> SeasonData { get; set; }
    }
}
