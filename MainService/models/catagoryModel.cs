using System.ComponentModel.DataAnnotations;

namespace MainService.models
{
    public class catagoryModel
    {
        [Key]
        public int catagoryId { get; set; }
        public string? CatagoryName { get; set; }
    }
}
