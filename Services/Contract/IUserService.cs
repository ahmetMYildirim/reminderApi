using Entities.Dto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contract
{
    public interface IUserService
    {
        void CreateUser(Users user); 
        Task UpdateUser(int id, Users user); 
        bool DeleteUser(int id); 
        Users GetUserById(int id);
        //bool IsUserValid(string username, string password);
    }
}
