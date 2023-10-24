using TaskManagementApp.DTOs;
using System;

namespace TaskManagementApp.Services
{
    public interface ISessionService
    {
        Guid GetCurrentUserId();

        string GetCurrentUserUsername();

        void SetCurrentUser(CurrentUserDto user);
    }
}
