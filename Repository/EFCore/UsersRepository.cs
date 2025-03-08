using Entities.Models;
using Repository.Contracts.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EFCore
{
    public class UsersRepository : RepoBase<Users>, IUserRepository
    {
        private readonly ReminderContext _context;
        public UsersRepository(ReminderContext reminderContext) : base(reminderContext)
        {
            _context = reminderContext;
        }

        /*public bool Any(Func<Users, bool> prdicate)
        {
            return _context.Users.Any(prdicate);
        }*/

        public void CreateUser(Users users)
        {
            Create(users);
        }

        public void DeleteUser(Users users)
        {
            Delete(users);
        }

        public Users GetUserById(int id)
        {
            return GetById(id);
        }

        public void UpdateUser(Users users)
        {
            Update(users);
        }
    }
}
