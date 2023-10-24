using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApp.Models
{
    public enum TaskStatus
    {
       [Description("Not started")]
       NotStarted = 0,
       [Description("In progress")]
       InProgress = 1,
       [Description("Finished")]
       Finished = 2
    }
}
