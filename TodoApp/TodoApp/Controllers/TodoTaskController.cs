﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodoApp.Data.DTOs;
using TodoApp.Data.Models;
using TodoApp.Services.Contracts;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoTaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TodoTaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<TodoTask>> GetAllTasks(string userId)
        {
            var todoTasks = _taskService.GetAllTasks(userId);

            return this.Ok(todoTasks);
        }

        [HttpPost]
        public IActionResult AddTask(TaskInputModel model)
        {
            _taskService.AddTask(model);
            return Ok();
        }


        [HttpPut]
        public IActionResult EditTask(TodoTask todoTask)
        {
            _taskService.EditTask(todoTask);

            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteTask(int id)
        {
            _taskService.DeleteTask(id);

            return Ok();
        }
    }
}
