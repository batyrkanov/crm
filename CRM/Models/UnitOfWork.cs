using CRM.Models;
using CRM.Models.Interfaces;
using CRM.Models.Repositories;
using System;

namespace CRM.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext db;
        private CompanyRepository companyRepository;
        private CategoryRepository categoryRepository;
        private TaskStatusRepository statusRepository;
        private TaskasRepository taskasRepository;

        public UnitOfWork() => db = new ApplicationDbContext();

        public CompanyRepository Companies
        {
            get
            {
                if (companyRepository == null)
                    companyRepository = new CompanyRepository(db);
                return companyRepository;
            }
        }

        public CategoryRepository Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(db);
                return categoryRepository;
            }
        }

        public TaskasRepository Tasks
        {
            get
            {
                if (taskasRepository == null)
                    taskasRepository = new TaskasRepository(db);
                return taskasRepository;
            }
        }

        public TaskStatusRepository TaskStatuses
        {
            get
            {
                if (statusRepository == null)
                    statusRepository = new TaskStatusRepository(db);
                return statusRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}