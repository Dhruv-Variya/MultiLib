using System.ComponentModel.DataAnnotations;

namespace MovieMainService.models
{
    public class itemLanguages
    {
        [Key]
        public int id { get; set; }
        public string? itemCode { get; set; }

        public int? languageId { get; set; }
    }
}
