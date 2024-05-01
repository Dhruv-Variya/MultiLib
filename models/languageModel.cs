using System.ComponentModel.DataAnnotations;

namespace MultiLib.models
{
    public class languageModel
    {
        [Key]
        public int languageId { get; set; }
        public string? languageName { get; set; }
    }
}
