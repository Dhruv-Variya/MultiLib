namespace MultiLib.TaskManagementAPI.Dto.TaskDto
{
    public class updateTaskDto
    {
        public int TaskId { get; set; }
        public int? UserId { get; set; }
        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }
        public string? DueDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
