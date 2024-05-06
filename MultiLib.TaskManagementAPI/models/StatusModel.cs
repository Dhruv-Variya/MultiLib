using System.ComponentModel.DataAnnotations;

namespace MultiLib.TaskManagementAPI.models
{
    public class StatusModel
    {
        [Key]
        public int StatusId { get; set; }
        public string? Status { get; set; }
    }
}
