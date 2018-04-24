using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Controllers
{
    public class CompaniesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Companies
        public async Task<ActionResult> Index()
        {
            return View(await db.Companies.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company companies = await db.Companies.FindAsync(id);
            if (companies == null)
            {
                return HttpNotFound();
            }
            return View(companies);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompanyId,CompanyName")] Company companies)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(companies);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companies);
        }

        // GET: Companies/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company companies = await db.Companies.FindAsync(id);
            if (companies == null)
            {
                return HttpNotFound();
            }
            return View(companies);
        }

        // POST: Companies/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompanyId,CompanyName")] Company companies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companies).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companies);
        }

        // GET: Companies/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company companies = await db.Companies.FindAsync(id);
            if (companies == null)
            {
                return HttpNotFound();
            }
            return View(companies);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Company companies = await db.Companies.FindAsync(id);
            db.Companies.Remove(companies);
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
