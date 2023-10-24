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
       NotStarted,
       [Description("In progress")]
       InProgress,
       [Description("Finished")]
       Finished
    }
}
