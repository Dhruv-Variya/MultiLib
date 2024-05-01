﻿using MovieMainService.models;

namespace MovieMainService.Dtos.seriesDto
{
    public class addSeasonDto
    {
        public string? itemCode { get; set; }
        public int? seasonNumber { get; set; }
        public string? seasonName { get; set; }
        // Add other properties for series
        public List<addEpisodeDto>? Episodes { get; set; }
    }
}