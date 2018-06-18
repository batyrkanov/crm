using CRM.Models.Repositories;
using System;

namespace CRM.Models.Interfaces
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
