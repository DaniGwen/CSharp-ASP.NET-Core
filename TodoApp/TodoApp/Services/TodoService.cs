using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Data.Models;
using TodoApp.Services.Contracts;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly ApplicationDbContext dbContext;

        public TodoService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddTodo(Todo todo)
        {
            this.dbContext.Todos.Add(todo);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteTodo(int todoId)
        {
            var todo = await this.dbContext
                .Todos
                .FindAsync(todoId);

            if (todo != null)
            {
                this.dbContext.Todos.Remove(todo);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task EditTodo(Todo todo)
        {
            var todoDb = await this.dbContext
                .Todos
                .FindAsync(todo.TodoId);

            if (todoDb != null)
            {
                todoDb.Content = todo.Content;
                todoDb.IsDone = todo.IsDone;

                await this.dbContext.SaveChangesAsync();
            }
        }

        public IEnumerable<Todo> GetAllTodos()
        {
            return this.dbContext.Todos.ToList();
        }
    }
}
