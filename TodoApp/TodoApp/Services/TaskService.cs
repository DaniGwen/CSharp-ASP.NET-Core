using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            };

            await this.dbContext.TodoTasks.AddAsync(newTask);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteTask(int todoTaskId)
        {
            var todoTaskDb = this.dbContext
                .TodoTasks
                .SingleOrDefault(t => t.TaskId == todoTaskId);

            this.dbContext.TodoTasks.Remove(todoTaskDb);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditTask(TodoTask todoTask)
        {
            var todoTaskDb = this.dbContext
                .TodoTasks
                .SingleOrDefault(t => t.TaskId == todoTask.TaskId);

            todoTaskDb.Title = todoTask.Title;

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
