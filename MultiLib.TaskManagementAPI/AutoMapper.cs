using AutoMapper;
using MultiLib.TaskManagementAPI.Dto.TaskDto;
using MultiLib.TaskManagementAPI.models;

namespace MainService
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {

            CreateMap<TaskModel, getTaskDto>();
            CreateMap<addTaskDto, TaskModel>();
            CreateMap<updateTaskDto, TaskModel>();

            CreateMap<updatePriorityDto, PriorityModel>();
            CreateMap<updateStatusDto, StatusModel>();

        }
    }
}
