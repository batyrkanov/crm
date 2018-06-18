using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CRM.Data.Data;
using CRM.Data.Repositories;
using CRM.Data.Entities;

namespace CRM.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext db;
        private CompanyRepository companyRepository;
        private CategoryRepository categoryRepository;
        private TaskStatusRepository statusRepository;
        private TaskasRepository taskasRepository;

        public UnitOfWork() => db = new DataContext();
        public UnitOfWork(string connectionString) => db = new DataContext(connectionString);

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