using Entities.Dto;
using Entities.Models;
using Repository.Contracts;
using Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;

        public UserService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public void CreateUser(Users user)
        {
            _repositoryManager.userRepository.CreateUser(user);
            _repositoryManager.Save();
        }

        public bool DeleteUser(int id)
        {
            var user = _repositoryManager.userRepository.GetUserById(id);
            if(user == null) 
            {
                return false;    
            }

            _repositoryManager.userRepository.DeleteUser(user);
            _repositoryManager.Save();
            return true;
        }

        public Users GetUserById(int id)
        {
            var users = _repositoryManager.userRepository.GetUserById(id);
            if (users == null)
                return null;
            
            return users;
        }

       /* public bool IsUserValid(string username, string password)
        {
            bool userExits = _repositoryManager.userRepository.Any(u => u.username == username && u.password == password);
            return userExits;
        }*/

        public async Task UpdateUser(int id, Users user)
        {
            var existingUser = _repositoryManager.userRepository.GetUserById(id);

            if (existingUser == null)
                throw new KeyNotFoundException("User not found.");

            existingUser.username = user.username;
            existingUser.password = user.password;
            existingUser.reminders = user.reminders;
            existingUser.email = user.email;
            existingUser.created_at = user.created_at;

            await _repositoryManager.Save();
        }
    }
}
