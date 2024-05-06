using System.ComponentModel.DataAnnotations;

namespace MultiLib.TaskManagementAPI.models
{
    public class PriorityModel
    {
        [Key]
        public int PriorityId { get; set; }
        public string? Priority { get; set; }
    }
}
