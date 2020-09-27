using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Data.DTOs;
using TodoApp.Data.Models;
using TodoApp.Services.Contracts;

namespace TodoApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext dbContext;

        public TaskService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddTask(TaskInputModel task)
        {
            var newTask = new TodoTask
            {
                Title = task.Title,
                UserId = task.UserId,
                IsDone = false
            };

            await this.dbContext.TodoTasks.AddAsync(newTask);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteTask(int todoTaskId)
        {
            var todoTask = await this.dbContext.TodoTasks.FindAsync(todoTaskId);

            this.dbContext.TodoTasks.Remove(todoTask);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditTask(TodoTask todoTask)
        {
            var todoTaskDb = await this.dbContext.TodoTasks.FindAsync(todoTask.TaskId);

            todoTaskDb = todoTask;

            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<TodoTask> GetAllTasks(string userId)
        {
            return this.dbContext
                .TodoTasks
                .Where(task => task.UserId == userId)
                .ToList();
        }
    }
}
