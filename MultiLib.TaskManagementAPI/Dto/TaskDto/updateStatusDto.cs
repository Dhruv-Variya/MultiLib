namespace MultiLib.TaskManagementAPI.Dto.TaskDto
{
    public class updateStatusDto
    {
        public int TaskId { get; set; }
        public int? UserId { get; set; }
        public int? StatusId { get; set; }
    }
}
