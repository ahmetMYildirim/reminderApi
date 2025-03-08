using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contract
{
    public interface IServiceManager
    {
        IReminderServices reminderServices { get; }
        IUserService userService { get; }
        IAuthenticationService authenticationService { get; }
    }
}
