using System.ComponentModel.DataAnnotations;

namespace MultiLib.models
{
    public class seasonsModel
    {
        [Key]
        public int id { get; set; }
        public string? itemCode { get; set; }
        public int? seasonNumber { get; set; }
        public string? seasonName { get; set; }

    }
}
