using TaskManagementApp.DTOs;
using System;

namespace TaskManagementApp.Services
{
    public class SessionService : ISessionService
    {
        private CurrentUserDto _currentUser;

        public Guid GetCurrentUserId()
        {
            return _currentUser.Id;
        }

        public string GetCurrentUserUsername()
        {
            return _currentUser.Username;
        }

        public void SetCurrentUser(CurrentUserDto user)
        {
            _currentUser = user;
        }
    }
}
