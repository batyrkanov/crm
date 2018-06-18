using CRM.Core.Entities;
using CRM.Data.Data;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
namespace CRM.Infrastructure.Repositories
{
    public class CompanyRepository : IRepository<Company>
    {
        private DataContext db;
        public CompanyRepository(DataContext context) => db = context;

        public void Create(Company item) => db.Companies.Add(item);

        public void Delete(Guid id)
        {
            Company comp = db.Companies.Find(id);
            if (comp != null)
                db.Companies.Remove(comp);
        }

        public IEnumerable<Company> GetAll() => db.Companies.OrderByDescending(x => x.CompanyName);

        public Company GetById(Guid id) => db.Companies.Find(id);

        public void Update(Company item) => db.Entry(item).State = EntityState.Modified;
    }
}
