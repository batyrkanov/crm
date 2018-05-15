using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using CRM.Models;
using CRM.Defence;

namespace CRM.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Categories
        [Authorize(Roles = "admin, manager")]
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            // возвращаем список категорий, сортируем по наименованию
            IPagedList<Category> category = db.Categories.OrderByDescending(x => x.CategoryName).ToPagedList(pageNumber, pageSize);
            return View(category);
        }

        // GET: Categories/Details/5
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        [Authorize(Roles = "admin, manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Create([Bind(Include = "CategoryId,CategoryName")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.CategoryId = Guid.NewGuid();
                    db.Categories.Add(category);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch (Exception)
            {
                ViewBag.Message = "Такая запись уже существует!";
                return View(category);
            }
        }
    
    

        // GET: Categories/Edit/5
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryId,CategoryName")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(category).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(category);
            }

            catch (Exception)
            {
                ViewBag.Message = "Такая запись уже существует!";
                return View(category);
            }
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Category category = await db.Categories.FindAsync(id);
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
