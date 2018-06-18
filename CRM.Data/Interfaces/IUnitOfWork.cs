using CRM.Data.Repositories;
using System;

namespace CRM.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CompanyRepository Companies { get; }
        CategoryRepository Categories { get; }
        TaskasRepository Tasks { get; }
        TaskStatusRepository TaskStatuses { get; }
        void Save();
    }
}
