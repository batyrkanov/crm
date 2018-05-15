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

namespace CRM.Controllers
{
    
    public class CompaniesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Companies
        [Authorize(Roles = "admin, manager")]
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            // возвращаем список компаний, сортируем по наименованию
            IPagedList<Company> company = db.Companies.OrderByDescending(x => x.CompanyName).ToPagedList(pageNumber, pageSize);
            return View(company);
        }

        // GET: Companies/Details/5
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        [Authorize(Roles = "admin, manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Create([Bind(Include = "CompanyId,CompanyName")] Company company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    company.CompanyId = Guid.NewGuid();
                    db.Companies.Add(company);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(company);
            }
            catch (Exception)
            {
                ViewBag.Message = "Такая запись уже существует!";
                return View(company);
            }

        }

        // GET: Companies/Edit/5
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Edit([Bind(Include = "CompanyId,CompanyName")] Company company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(company).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(company);

            }
            catch (Exception)
            {
                ViewBag.Message = "Такая запись уже существует!";
                return View(company);
            }
        }

        // GET: Companies/Delete/5
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [Authorize(Roles = "admin, manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Company company = await db.Companies.FindAsync(id);
            db.Companies.Remove(company);
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
