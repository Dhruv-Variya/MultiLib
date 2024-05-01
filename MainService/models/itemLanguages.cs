using System.ComponentModel.DataAnnotations;

namespace MainService.models
{
    public class itemLanguages
    {
        [Key]
        public int id { get; set; }
        public string? itemCode { get; set; }

        public int? languageId { get; set; }
    }
}
