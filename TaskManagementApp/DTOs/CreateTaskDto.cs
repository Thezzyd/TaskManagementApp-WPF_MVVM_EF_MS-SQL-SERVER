using System;

namespace TaskManagementApp.DTOs
{
    public class CreateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public int Priority { get; set; }
        public Models.TaskStatus Status { get; set; }
        public Guid UserId { get; set; }
    }
}
