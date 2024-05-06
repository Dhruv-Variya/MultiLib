namespace MultiLib.TaskManagementAPI.Dto.TaskDto
{
    public class addTaskDto
    {
        public int? UserId { get; set; }
        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }
        public string? DueDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
