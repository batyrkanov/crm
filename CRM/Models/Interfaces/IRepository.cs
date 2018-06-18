using System;
using System.Collections.Generic;

namespace CRM.Models.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(Guid? id);
        void Create(T item);
        void Update(T item);
        void Delete(Guid? id);
    }
}
