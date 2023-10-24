using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementApp.Services
{
    internal class TaskService : ITaskService
    {
        private readonly AppDbContextFactory _appDbContextFactory;

        public TaskService(AppDbContextFactory appDbContextFactory)
        {
            _appDbContextFactory = appDbContextFactory;
        }

        public async Task<ServiceResopnse<TaskDto>> CreateTask(CreateTaskDto newTaskDto)
        {
            using (AppDbContext dbContext = _appDbContextFactory.CreateDbContext())
            { 
                var serviceResponse = new ServiceResopnse<TaskDto>();
                var newTask = new Models.Task
                {
                    Id = Guid.NewGuid(),
                    Name = newTaskDto.Title,
                    Description = newTaskDto.Description,
                    Deadline = newTaskDto.Deadline,
                    Priority = newTaskDto.Priority,
                    IsHidden = false,
                    Status = Models.TaskStatus.NotStarted,
                    UserId = newTaskDto.UserId,
                };

                dbContext.Tasks.Add(newTask);
                var result = await dbContext.SaveChangesAsync() > 0;
                if (result)
                {
                    serviceResponse.Message = "New task successfully created.";
                    serviceResponse.Data = new TaskDto
                    {
                        Id = newTask.Id,
                        Name = newTask.Name,
                        Description = newTask.Description,
                        CreatedTime = newTask.CreatedTime,
                        Deadline = newTask.Deadline,
                        Priority = newTask.Priority,
                        IsHidden = newTask.IsHidden,
                        Status = newTask.Status
                    };
                    return serviceResponse;
                }

                serviceResponse.Success = false;
                serviceResponse.Message = "Problem occured durning creation of new task.";
                return serviceResponse;
            }
        }

        public async Task<ServiceResopnse<IEnumerable<TaskDto>>> GetAllUserTasks(Guid userId)
        {
            using(AppDbContext dbContext = _appDbContextFactory.CreateDbContext())
            {
                var serviceResponse = new ServiceResopnse<IEnumerable<TaskDto>>();
                IEnumerable<Models.Task> userTasks = await dbContext.Tasks.Where(t => t.UserId == userId).ToListAsync();
                List<TaskDto> tasksDto = new List<TaskDto>();
                foreach (var _task in userTasks) 
                {
                    TaskDto taskDto = new TaskDto
                    {
                        Id = _task.Id,
                        Name = _task.Name,
                        Description = _task.Description,
                        CreatedTime = _task.CreatedTime,
                        Deadline = _task.Deadline,
                        Priority = _task.Priority,
                        IsHidden = _task.IsHidden,
                        Status = _task.Status
                    };
                    tasksDto.Add(taskDto);
                }

                serviceResponse.Data = tasksDto;
                serviceResponse.Message = "Successfully retrieved user tasks.";
                return serviceResponse;
            }
        }

        public async Task<ServiceResopnse<bool>> RemoveTask(Guid id)
        {
            using (AppDbContext dbContext = _appDbContextFactory.CreateDbContext())
            {
                var serviceResponse = new ServiceResopnse<bool>();
                var task = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
                if (task == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Task not found.";
                    return serviceResponse;
                }

                dbContext.Tasks.Remove(task);
                var result = await dbContext.SaveChangesAsync() > 0;
                if (result)
                {
                    serviceResponse.Message = "Successfully removed task.";
                    return serviceResponse;
                }

                serviceResponse.Success = false;
                serviceResponse.Message = "Problem occured durning removal of task.";
                return serviceResponse;
            }
        }

        public async Task<ServiceResopnse<TaskDto>> UpdateTask(TaskDto updatedTaskDto)
        {
            using (AppDbContext dbContext = _appDbContextFactory.CreateDbContext())
            {
                var serviceResponse = new ServiceResopnse<TaskDto>();
                var task = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == updatedTaskDto.Id);
                if (task == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Task not found.";
                    return serviceResponse;
                }

                task.Name = updatedTaskDto.Name;
                task.Description = updatedTaskDto.Description;
                task.Deadline = updatedTaskDto.Deadline;
                task.Priority = updatedTaskDto.Priority;
                task.Status = updatedTaskDto.Status;
                task.IsHidden = updatedTaskDto.IsHidden;

                var result = await dbContext.SaveChangesAsync() > 0;
                if (result)
                {
                    serviceResponse.Message = "Task successfully updated.";
                    serviceResponse.Data = new TaskDto
                    {
                        Id = task.Id,
                        Name = task.Name,
                        Description = task.Description,
                        CreatedTime = task.CreatedTime,
                        Deadline = task.Deadline,
                        Priority = task.Priority,
                        IsHidden = task.IsHidden,
                        Status = task.Status
                    };
                    return serviceResponse;
                }

                serviceResponse.Success = false;
                serviceResponse.Message = "Empty update or problem occured durning saving changes to database.";
                return serviceResponse;
            }
        }

        public async Task<ServiceResopnse<TaskDto>> UpdateTaskStatusAndVisibility(Guid taskId, Models.TaskStatus status, bool isHidden)
        {
            using (AppDbContext dbContext = _appDbContextFactory.CreateDbContext()) 
            {
                var serviceResponse = new ServiceResopnse<TaskDto>();
                var task = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
                if (task == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Task not found.";
                    return serviceResponse;
                }

                task.Status = status;
                task.IsHidden = isHidden;
                var result = await dbContext.SaveChangesAsync() > 0;
                if (result)
                {
                    var taskDto = new TaskDto
                    {
                        Id = task.Id,
                        Name = task.Name,
                        Description = task.Description,
                        CreatedTime = task.CreatedTime,
                        Deadline = task.Deadline,
                        Priority = task.Priority,
                        IsHidden = task.IsHidden,
                        Status = task.Status
                    };

                    serviceResponse.Data = taskDto;
                    serviceResponse.Message = "Successfully updated status and visibility of task.";
                    return serviceResponse;
                }

                serviceResponse.Success = false;
                serviceResponse.Message = "Empty update or problem occured durning saving changes to database.";
                return serviceResponse;
            }
        }
    }
}
