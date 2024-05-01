using System.ComponentModel.DataAnnotations;

namespace _NET.models
{
    public class languageModel
    {
        [Key]
        public int languageId { get; set; }
        public string? languageName { get; set; }
    }
}
