using System.ComponentModel.DataAnnotations;

namespace MainService.models
{
    public class post
    {
        [Key]
        public int Id { get; set; }
        public string? movie_name { get; set; }
        public string? movie_url { get; set; }
        public byte[]? Data { get; set; }


    }
}
