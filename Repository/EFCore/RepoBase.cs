using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EFCore
{
    public class RepoBase<T> : IRepositoryBase<T> where T : class
    {
        protected ReminderContext ReminderContext { get; set; }

        public RepoBase(ReminderContext reminderContext) 
        {
            ReminderContext = reminderContext;    
        }

        public IQueryable<T> GetAll()
        {
            return ReminderContext.Set<T>().AsQueryable();
        }

        public T GetById(int id)
        {
            return ReminderContext.Set<T>().Find(id);
        }

        public void Create(T entity)
        {
            ReminderContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            ReminderContext?.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            ReminderContext.Set<T>().Update(entity);
        }
    }
}
