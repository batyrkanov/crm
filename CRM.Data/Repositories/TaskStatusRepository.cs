using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using CRM.Data.Data;
using CRM.Data.Entities;

namespace CRM.Data.Repositories
{
    public class TaskStatusRepository : IRepository<TaskStatus>
    {
        private DataContext db;
        public TaskStatusRepository(DataContext context) => db = context;

        public void Create(TaskStatus item) => db.TaskStatuses.Add(item);

        public void Delete(Guid id)
        {
            TaskStatus status = db.TaskStatuses.Find(id);
            if (status != null)
                db.TaskStatuses.Remove(status);
        }

        public IEnumerable<TaskStatus> GetAll() => db.TaskStatuses.OrderByDescending(x => x.StatusName);

        public TaskStatus GetById(Guid id) => db.TaskStatuses.Find(id);

        public void Update(TaskStatus item) => db.Entry(item).State = EntityState.Modified;
    }
}
