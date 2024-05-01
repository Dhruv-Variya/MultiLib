using System.ComponentModel.DataAnnotations;

namespace MovieMainService.models
{
    public class catagoryModel
    {
        [Key]
        public int catagoryId { get; set; }
        public string? CatagoryName { get; set; }
    }
}
