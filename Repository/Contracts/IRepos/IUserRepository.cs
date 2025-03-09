using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts.IRepos
{
    public interface IUserRepository : IRepositoryBase<Users>
    {
        void CreateUser(Users users);
        void UpdateUser(Users users);
        void DeleteUser(Users users);
        Users GetUserById(int id);
    }
}
