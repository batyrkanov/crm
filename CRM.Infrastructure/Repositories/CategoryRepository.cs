using CRM.Core.Entities;
using CRM.Data.Data;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace CRM.Infrastructure.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private DataContext db;
        public CategoryRepository(DataContext context) => db = context;

        public void Create(Category item) => db.Categories.Add(item);

        public void Delete(Guid id)
        {
            Category cat = db.Categories.Find(id);
            if (cat != null)
                db.Categories.Remove(cat);
        }

        public IEnumerable<Category> GetAll() => db.Categories.OrderByDescending(x => x.CategoryName);

        public Category GetById(Guid id) => db.Categories.Find(id);

        public void Update(Category item) => db.Entry(item).State = EntityState.Modified;
    }
}
