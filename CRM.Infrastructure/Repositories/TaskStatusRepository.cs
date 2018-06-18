using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using CRM.Data.Data;

namespace CRM.Infrastructure.Repositories
{
    public class TaskStatusRepository : IRepository<Core.Entities.TaskStatus>
    {
        private DataContext db;
        public TaskStatusRepository(DataContext context) => db = context;

        public void Create(Core.Entities.TaskStatus item) => db.TaskStatuses.Add(item);

        public void Delete(Guid id)
        {
            Core.Entities.TaskStatus status = db.TaskStatuses.Find(id);
            if (status != null)
                db.TaskStatuses.Remove(status);
        }

        public IEnumerable<Core.Entities.TaskStatus> GetAll() => db.TaskStatuses.OrderByDescending(x => x.StatusName);

        public Core.Entities.TaskStatus GetById(Guid id) => db.TaskStatuses.Find(id);

        public void Update(Core.Entities.TaskStatus item) => db.Entry(item).State = EntityState.Modified;
    }
}
