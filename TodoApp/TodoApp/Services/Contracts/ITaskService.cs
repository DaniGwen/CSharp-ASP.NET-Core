using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Data.DTOs;
using TodoApp.Data.Models;

namespace TodoApp.Services.Contracts
{
    public interface ITaskService
    {
        IEnumerable<TodoTask> GetAllTasks(string userId);
        Task AddTask(TaskInputModel task);
        Task EditTask(TodoTask todoTask);
        Task DeleteTask(int todoTaskId);
    }
}
