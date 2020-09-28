using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using TodoApp.Data.DTOs;
using TodoApp.Data.Models;
using TodoApp.Services.Contracts;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoTaskController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TodoTaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<TodoTask>> GetAllTasks(string userId)
        {
            var todoTasks = this.taskService.GetAllTasks(userId);

            return this.Ok(todoTasks);
        }

        [HttpPost]
        public IActionResult AddTask(TaskInputModel model)
        {
            this.taskService.AddTask(model);
            return Ok();
        }


        [HttpPut]
        public IActionResult EditTask(TodoTask todoTask)
        {
            this.taskService.EditTask(todoTask);

            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteTask(int id)
        {
            this.taskService.DeleteTask(id);

            return Ok();
        }
    }
}
