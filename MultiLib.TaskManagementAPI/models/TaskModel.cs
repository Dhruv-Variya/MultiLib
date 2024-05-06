using System;
using System.ComponentModel.DataAnnotations;

namespace MultiLib.TaskManagementAPI.models
{
    public class ReqTaskModel
    {

        public int TaskId { get; set; }
        public int? UserId { get; set; }
    }
    public class TaskModel
    {
        [Key]
        public int TaskId { get; set; }
        public int? UserId { get; set; }
        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }
        public string? DueDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        
    }
}
