namespace MultiLib.TaskManagementAPI.Dto.TaskDto
{
    public class updatePriorityDto
    {
        public int TaskId { get; set; }
        public int? UserId { get; set; }
        public int? PriorityId { get; set; }
    }
}
