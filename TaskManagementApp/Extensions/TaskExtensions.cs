using TaskManagementApp.DTOs;
using TaskManagementApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagementApp.Extensions
{
    public static class TaskExtensions
    {
        public static IEnumerable<TaskDto> Search(this IEnumerable<TaskDto> tasks, string searchTerm) {
            if(string.IsNullOrEmpty(searchTerm)) return tasks;

            var searchTermToLower = searchTerm.Trim().ToLower();
            var filteredTasks = tasks.Where(task =>
                   (task.Description !=null && task.Description.ToLower().Contains(searchTermToLower)) ||
                   task.Name.ToLower().Contains(searchTermToLower) ||
                   task.DaysLeft.ToString().Contains(searchTermToLower) ||
                   (task.Deadline?.ToString("yyyy-MM-dd").Contains(searchTermToLower) ?? false) ||
                   task.Priority.ToString() == searchTermToLower);

            return filteredTasks;
        }

        public static IEnumerable<TaskDto> FilterPriority(this IEnumerable<TaskDto> tasks, string priorityFilterValue, string inequalitySign) {
            if (string.IsNullOrEmpty(priorityFilterValue)) return tasks;

            bool isPriorityNumeric = int.TryParse(priorityFilterValue, out int priorityNumeric);

            if (isPriorityNumeric && priorityNumeric >= 0) {
                var filteredTasks = tasks.Where(task =>
                    (inequalitySign == ">" && task.Priority > priorityNumeric) ||
                    (inequalitySign == "<" && task.Priority < priorityNumeric));

                return filteredTasks;
            }
            return tasks;
        }

        public static IEnumerable<TaskDto> FilterStatus(this IEnumerable<TaskDto> tasks, IEnumerable<StatusEntryViewModel> statusItems) {
            IEnumerable<TaskDto> filteredTasks = new List<TaskDto>();
            foreach (var status in statusItems)
            {
                filteredTasks = filteredTasks.Concat( tasks.Where(task =>
                    (status.IsChecked && task.Status == status.Status)
                    ));
            }

            return filteredTasks;
        }

        public static IEnumerable<TaskDto> FilterVisibility(this IEnumerable<TaskDto> tasks, bool visible, bool hidden) {
            var filteredTasks = tasks.Where(task => 
                (visible && !task.IsHidden) ||
                (hidden && task.IsHidden)
            );
            return filteredTasks;
        }
    }
}
