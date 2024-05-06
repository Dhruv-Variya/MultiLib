using Microsoft.AspNetCore.Mvc;
using MultiLib.TaskManagementAPI.Dto.TaskDto;
using MultiLib.TaskManagementAPI.models;

namespace MultiLib.TaskManagementAPI.Service.TaskManagementService
{
    public interface ITaskService
    {
        Task<ServiceResponse<List<getTaskDto>>> GetAll(ReqTaskModel reqTaskModel);
        Task<ServiceResponse<getTaskDto>> AddTask(addTaskDto addTaskDto);
        Task<ServiceResponse<getTaskDto>> UpdateTask(updateTaskDto updatedTask);
        Task<ServiceResponse<getTaskDto>> DeleteTask(ReqTaskModel reqTaskModel);
    }
}
