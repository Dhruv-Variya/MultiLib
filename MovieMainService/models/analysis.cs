namespace MovieMainService.models
{
    public class analysis
    {
        public int id { get; set; }
        public string? itemCode { get; set; }
        public string? itemType { get; set; }
        public int? timesClicked { get; set; }
        public int? upVote { get; set; }
        public int? downVote { get; set; }
        public int? trailerReach { get; set; }

    }
}
