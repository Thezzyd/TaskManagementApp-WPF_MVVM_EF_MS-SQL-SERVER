using TaskManagementApp.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagementApp.Services
{
    public interface ITaskService
    {
        Task<ServiceResopnse<IEnumerable<TaskDto>>> GetAllUserTasks(Guid userId);
        Task<ServiceResopnse<TaskDto>> UpdateTaskStatusAndVisibility(Guid taskId, Models.TaskStatus status, bool isHidden);
        Task<ServiceResopnse<TaskDto>> CreateTask(CreateTaskDto newTaskDto);
        Task<ServiceResopnse<TaskDto>> UpdateTask(TaskDto updatedTaskDto);
        Task<ServiceResopnse<bool>> RemoveTask(Guid id);
    }
}
