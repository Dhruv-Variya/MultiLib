using Microsoft.AspNetCore.Mvc;
using MultiLib.TaskManagementAPI.Data;
using MultiLib.TaskManagementAPI.Dto.TaskDto;
using MultiLib.TaskManagementAPI.models;
using MultiLib.TaskManagementAPI.Service.TaskManagementService;
namespace MultiLib.TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : Controller
    {

        private readonly DataContext _context;
        private readonly ITaskService _taskService;
        private readonly IWebHostEnvironment _environment;

        public TaskController(ITaskService taskService, IWebHostEnvironment environment, DataContext context)
        {
            _taskService = taskService;
            _environment = environment;
            _context = context;
        }
        //Get All Task
        [HttpPost("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<getTaskDto>>>> GetAll(ReqTaskModel reqTaskModel)
        {
            return Ok(await _taskService.GetAll(reqTaskModel));
        }

        //Add Task
        [HttpPost("AddTask")]
        public async Task<ActionResult<ServiceResponse<getTaskDto>>> AddTask(addTaskDto addTaskDto)
        {
            return Ok(await _taskService.AddTask(addTaskDto));
        }


        //update Task
        [HttpPut("UpdateTask")]
        public async Task<ActionResult<ServiceResponse<getTaskDto>>> UpdateTask(updateTaskDto updatedTask)
        {
            var response = await _taskService.UpdateTask(updatedTask);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        //delete Task
        [HttpDelete("DleteTask")]
        public async Task<ActionResult<ServiceResponse<getTaskDto>>> DeleteTask(ReqTaskModel reqTaskModel)
        {
            var response = await _taskService.DeleteTask(reqTaskModel);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
