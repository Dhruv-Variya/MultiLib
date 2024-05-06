using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultiLib.TaskManagementAPI.Data;
using MultiLib.TaskManagementAPI.Dto.TaskDto;
using MultiLib.TaskManagementAPI.models;

namespace MultiLib.TaskManagementAPI.Service.TaskManagementService
{
    public class TaskService : ITaskService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;


        public TaskService(IMapper mapper, DataContext context, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _context = context;
            _environment = environment;
        }

        #region get all task for spacific user 
       
        public async Task<ServiceResponse<List<getTaskDto>>> GetAll(ReqTaskModel reqTaskModel)
        {
            var serviceResponse = new ServiceResponse<List<getTaskDto>>();
            try
            {
                serviceResponse.Data = new List<getTaskDto>();

                if (reqTaskModel.UserId == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User ID Reuired!!!";
                    return serviceResponse;
                }

                var dbTasks = await _context.task.Where(t => t.UserId == reqTaskModel.UserId).ToListAsync();

                if (dbTasks == null || dbTasks.Count == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Task not found for User with ID: '{reqTaskModel.UserId}'";
                    return serviceResponse;
                }
               
                    foreach (var task in dbTasks)
                    {
                        var Status = await _context.status.Where(s => s.StatusId == task.StatusId).FirstOrDefaultAsync();
                        var Priority = await _context.priority.Where(s => s.PriorityId == task.PriorityId).FirstOrDefaultAsync();

                        var dto = _mapper.Map<getTaskDto>(task);
                        dto.Status = Status.Status;
                        dto.Priority = Priority.Priority;

                        serviceResponse.Data.Add(dto);
                    }

                    serviceResponse.Success = true;
                    serviceResponse.Message = "Success";
               
                
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        #endregion

        #region Add Task
        
        public async Task<ServiceResponse<getTaskDto>> AddTask(addTaskDto addTaskObject)
        {
            var serviceResponse = new ServiceResponse<getTaskDto>();
            if (addTaskObject.UserId == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User ID Required!!!";
                return serviceResponse;
            }
            else if (addTaskObject.StatusId == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "StatusId Required!!!";
                return serviceResponse;
            }
            else if (addTaskObject.PriorityId == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "PriorityId Required!!!";
                return serviceResponse;
            }

            var task = _mapper.Map<TaskModel>(addTaskObject);
            try
            {
                _context.task.Add(task);
                await _context.SaveChangesAsync(); // Save Task in db

                serviceResponse.Success = true;
                serviceResponse.Message = "Task Added Successfully!";
                serviceResponse.Data = _mapper.Map<getTaskDto>(task);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
                return serviceResponse;
        }

        #endregion

        #region Update Task
        public async Task<ServiceResponse<getTaskDto>> UpdateTask(updateTaskDto updatedTask)
        {
            var serviceResponse = new ServiceResponse<getTaskDto>();
            if (updatedTask.UserId == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "UserId Required!!!";
                return serviceResponse;
            }
            else if (updatedTask.TaskId == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "TaskId Required!!!";
                return serviceResponse;
            }
            else if (updatedTask.StatusId == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "StatusId Required!!!";
                return serviceResponse;
            }
            else if (updatedTask.PriorityId == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "PriorityId Required!!!";
                return serviceResponse;
            }

            try
            {
                var dbTask = await _context.task.FirstOrDefaultAsync(c => c.TaskId == updatedTask.TaskId);
                if (dbTask != null)
                {
                    _mapper.Map(updatedTask, dbTask);

                    serviceResponse.Success = true;
                    serviceResponse.Message = "Task Updated Successfully!";
                    serviceResponse.Data = _mapper.Map<getTaskDto>(dbTask);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Task with ID: '{updatedTask.TaskId}' not found for User with ID: '{updatedTask.UserId}'";
                    return serviceResponse;
                }
                
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            await _context.SaveChangesAsync();

            return serviceResponse;
        }

        #endregion

        #region Delete task
        
        public async Task<ServiceResponse<getTaskDto>> DeleteTask(ReqTaskModel reqTaskModel)
        {
            var serviceResponse = new ServiceResponse<getTaskDto>();
            if (reqTaskModel.UserId == 0 || reqTaskModel.TaskId == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "UserId And TaskId Required!!!";
                return serviceResponse;
            }
            try
            {
                var dbtask = await _context.task.FirstOrDefaultAsync(c => c.TaskId == reqTaskModel.TaskId);
                if (dbtask is null)
                {
                    throw new Exception($"task with ID '{reqTaskModel.TaskId}' not found");
                }
                _context.task.Remove(dbtask);
                serviceResponse.Success = true;
                serviceResponse.Message = "Task Deleted Successfully!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            await _context.SaveChangesAsync();
            return serviceResponse;
        }
        #endregion

    }
}
