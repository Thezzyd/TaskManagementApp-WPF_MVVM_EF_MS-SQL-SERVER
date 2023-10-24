using TaskManagementApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApp.Services
{
    public interface ISessionService
    {
        Guid GetCurrentUserId();

        string GetCurrentUserUsername();

        void SetCurrentUser(CurrentUserDto user);
    }
}
