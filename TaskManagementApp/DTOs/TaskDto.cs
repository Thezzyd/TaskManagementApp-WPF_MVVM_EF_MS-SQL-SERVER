using TaskManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TaskManagementApp.DTOs
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? Deadline { get; set; }
        public string DaysLeft
         {
             get
             {
                 if (Deadline != null) return Deadline?.Date.Subtract(DateTime.Now.Date).Days.ToString();
                 else return "-"; 
             }
         }

        public int Priority { get; set; }
        public bool IsHidden { get; set; }
        public TaskStatus Status { get; set; }

        
    }
}
