using AutoMapper;
using Entities.Auth_Models;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository.Contracts;
using Services.Contract;
using System;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IReminderServices> _reminderServices;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager manager, UserManager<User> userManager, IMapper mapper, IConfiguration configuration)
        {
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager), "UserManager dependency is not resolved.");

            _reminderServices = new Lazy<IReminderServices>(() => new ReminderServices(manager));
            _userService = new Lazy<IUserService>(() => new UserService(manager));
            _authenticationService = new Lazy<IAuthenticationService>(() =>
                new AuthencationManager(userManager, configuration, mapper)
            );
        }


        public IReminderServices reminderServices => _reminderServices.Value;
        public IUserService userService => _userService.Value;
        public IAuthenticationService authenticationService => _authenticationService.Value;
    }
}
