namespace MainService.Dtos.seriesDto
{
    public class addSeriesDto
    {
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
        public List<int> seriesCategoryIds { get; set; } = new List<int>();
        public List<int> seriesLanguagesIds { get; set; } = new List<int>();
    }
}
