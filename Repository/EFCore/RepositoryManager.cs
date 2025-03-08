using Repository.Contracts;
using Repository.Contracts.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ReminderContext _context;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IReminderRepository> _reminderRepository;

        public RepositoryManager(ReminderContext context)
        {
            _context = context;
            _userRepository = new Lazy<IUserRepository>(() => new UsersRepository(_context));
            _reminderRepository = new Lazy<IReminderRepository>(() => new ReminderRepository(_context));
        }

        public IUserRepository userRepository => _userRepository.Value;

        public IReminderRepository reminderRepository => _reminderRepository.Value;

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
