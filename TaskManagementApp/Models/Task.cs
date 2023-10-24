
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementApp.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public bool IsHidden { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.NotStarted;
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
