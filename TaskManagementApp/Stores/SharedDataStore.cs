using TaskManagementApp.DTOs;
using System;
using System.Runtime.CompilerServices;

namespace TaskManagementApp.SharedData
{
    public static class SharedDataStore
    {
        public static event EventHandler<TaskDtoInFocusEventArgs> OnTaskListChanged;
        public static event EventHandler<MessageEventArgs> OnTaskMenuResponseMessageChange;
        public static event EventHandler<bool> OnModalWindowIsOpenedChange;

        public static TaskDto _currentlySelectedTaskDto;
        private static bool _isModalWindowOpened;
        public static bool IsModalWindowOpened
        {
            get { return _isModalWindowOpened; }
            set { 
                _isModalWindowOpened = value;
                InvokeOnModalWindowIsOpenedChange(null, value);
            }
        }

        public static void InvokeOnTaskListChanged(object sender, TaskDtoInFocusEventArgs taskDtoData = null)
        {
            OnTaskListChanged?.Invoke(sender, taskDtoData);
        }

        public static void InvokeOnTaskMenuErrorMessageChange(object sender, MessageEventArgs responseMessageData = null)
        {
            OnTaskMenuResponseMessageChange?.Invoke(sender, responseMessageData);
        }

        public static void InvokeOnModalWindowIsOpenedChange(object sender, bool isOpen = false)
        {
            OnModalWindowIsOpenedChange?.Invoke(sender, isOpen);
        }

    }

    public class TaskDtoInFocusEventArgs : EventArgs
    {
        private readonly TaskDto _taskDtoData;

        public TaskDtoInFocusEventArgs(TaskDto taskDtoData)
        {
            _taskDtoData = taskDtoData;
        }

        public TaskDto TaskDtoData
        {
            get { return _taskDtoData; }
        }
    }
    public class MessageEventArgs : EventArgs
    {
        private readonly string _message;
        private readonly bool _Success;

        public MessageEventArgs(string message, bool Success)
        {
            _message = message;
            _Success = Success;
        }

        public string Message
        {
            get { return _message; }
        }

        public bool Success
        {
            get { return _Success; }
        }
    }
}
