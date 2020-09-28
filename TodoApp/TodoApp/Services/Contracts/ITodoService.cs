using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Data.Models;

namespace TodoApp.Services.Contracts
{
    public interface ITodoService
    {
        IEnumerable<Todo> GetAllTodos();
        Task AddTodo(Todo todo);
        Task EditTodo(Todo todo);
        Task DeleteTodo(int todoId);
    }
}
