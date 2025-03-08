using Repository.Contracts.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository userRepository { get; }
        IReminderRepository reminderRepository { get; }

        Task Save();
    }
}
