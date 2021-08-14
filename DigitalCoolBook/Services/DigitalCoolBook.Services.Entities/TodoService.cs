using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models.BlazorModels;
using DigitalCoolBook.Services.Contracts;

namespace DigitalCoolBook.Services
{
    public class TodoService : ITodoService
    {
        private readonly ApplicationDbContext _dbContext;

        public TodoService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddTodo(Todo todo)
        {
            _dbContext.Todos.Add(todo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTodo(int todoId)
        {
            var todo = await _dbContext
                .Todos
                .FindAsync(todoId);

            if (todo != null)
            {
                _dbContext.Todos.Remove(todo);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task EditTodo(Todo todo)
        {
            var todoDb = await _dbContext
                .Todos
                .FindAsync(todo.TodoId);

            if (todoDb != null)
            {
                todoDb.Content = todo.Content;
                todoDb.IsDone = todo.IsDone;

                await _dbContext.SaveChangesAsync();
            }
        }

        public IEnumerable<Todo> GetAllTodos()
        {
            return _dbContext.Todos.ToList();
        }
    }
}
