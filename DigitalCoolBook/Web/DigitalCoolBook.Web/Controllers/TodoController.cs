using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalCoolBook.Models.BlazorModels;
using DigitalCoolBook.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalCoolBook.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetTodos()
        {
            var todos = _todoService.GetAllTodos();

            return Ok(todos);
        }

        [HttpPut]
        public async Task<IActionResult> PutTodo(Todo todo)
        {
            await _todoService.EditTodo(todo);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostTodo(Todo todo)
        {
           await _todoService.AddTodo(todo);

            return Ok();
        }

        [HttpDelete("{todoId}")]
        public async Task<IActionResult> DeleteTodo(int todoId)
        {
            await _todoService.DeleteTodo(todoId);

            return Ok();
        }
    }
}
