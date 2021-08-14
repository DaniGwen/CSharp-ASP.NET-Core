using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models.BlazorModels;
using DigitalCoolBook.Services.Contracts;
using DigitalCoolBook.Web.Models.BlazorViewModels;

namespace DigitalCoolBook.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddTask(TaskInputModel task)
        {
            var newTask = new TodoTask
            {
                Title = task.Title,
                UserId = task.UserId,
            };

            await _dbContext.TodoTasks.AddAsync(newTask);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTask(int todoTaskId)
        {
            var todoTaskDb = _dbContext
                .TodoTasks
                .SingleOrDefault(t => t.TaskId == todoTaskId);

            _dbContext.TodoTasks.Remove(todoTaskDb);

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditTask(TodoTask todoTask)
        {
            var todoTaskDb = _dbContext
                .TodoTasks
                .SingleOrDefault(t => t.TaskId == todoTask.TaskId);

            todoTaskDb.Title = todoTask.Title;

            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<TodoTask> GetAllTasks(string userId)
        {
            return _dbContext
                .TodoTasks
                .Where(task => task.UserId == userId)
                .ToList();
        }
    }
}
