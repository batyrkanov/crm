using CRM.Core.Entities;
using CRM.Data.Data;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CRM.Infrastructure.Repositories
{
    public class TaskasRepository : IRepository<Taska>
    {
        private DataContext db;
        public TaskasRepository(DataContext context) => db = context;


        public IEnumerable<Taska> GetAll() => db.Tasks.OrderByDescending(x => x.TaskDate);

        public Taska GetById(Guid id) => db.Tasks.Find(id);

        public void Create(Taska item) => db.Tasks.Add(item);

        public void Update(Taska item) => db.Entry(item).State = EntityState.Modified;

        public void Delete(Guid id)
        {
            Taska task = db.Tasks.Find(id);
            if (task != null)
                db.Tasks.Remove(task);
        }
    }
}
