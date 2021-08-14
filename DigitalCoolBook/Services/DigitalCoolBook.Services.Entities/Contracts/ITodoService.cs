using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalCoolBook.Models.BlazorModels;

namespace DigitalCoolBook.Services.Contracts
{
    public interface ITodoService
    {
        IEnumerable<Todo> GetAllTodos();
        Task AddTodo(Todo todo);
        Task EditTodo(Todo todo);
        Task DeleteTodo(int todoId);
    }
}
