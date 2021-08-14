using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalCoolBook.Models.BlazorModels;
using DigitalCoolBook.Web.Models.BlazorViewModels;

namespace DigitalCoolBook.Services.Contracts
{
    public interface ITaskService
    {
        IEnumerable<TodoTask> GetAllTasks(string userId);
        Task AddTask(TaskInputModel task);
        Task EditTask(TodoTask todoTask);
        Task DeleteTask(int todoTaskId);
    }
}
