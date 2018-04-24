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
    public class TaskasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Taskas
        public async Task<ActionResult> Index()
        {
            return View(await db.Tasks.ToListAsync());
        }

        // GET: Taskas/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taska taska = await db.Tasks.FindAsync(id);
            if (taska == null)
            {
                return HttpNotFound();
            }
            return View(taska);
        }

        // GET: Taskas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Taskas/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TaskId,TaskName,CompanyId,CategoryId,TaskDate,ManagerName,TaskStatus,Description")] Taska taska)
        {
            if (ModelState.IsValid)
            {
                taska.TaskId = Guid.NewGuid();
                db.Tasks.Add(taska);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(taska);
        }

        // GET: Taskas/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taska taska = await db.Tasks.FindAsync(id);
            if (taska == null)
            {
                return HttpNotFound();
            }
            return View(taska);
        }

        // POST: Taskas/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TaskId,TaskName,CompanyId,CategoryId,TaskDate,ManagerName,TaskStatus,Description")] Taska taska)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taska).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taska);
        }

        // GET: Taskas/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taska taska = await db.Tasks.FindAsync(id);
            if (taska == null)
            {
                return HttpNotFound();
            }
            return View(taska);
        }

        // POST: Taskas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Taska taska = await db.Tasks.FindAsync(id);
            db.Tasks.Remove(taska);
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
