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
    public class TaskStatusesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TaskStatuses
        public async Task<ActionResult> Index()
        {
            return View(await db.TaskStatuses.ToListAsync());
        }

        // GET: TaskStatuses/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.TaskStatus taskStatuses = await db.TaskStatuses.FindAsync(id);
            if (taskStatuses == null)
            {
                return HttpNotFound();
            }
            return View(taskStatuses);
        }

        // GET: TaskStatuses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskStatuses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StatusId,StatusName")] Models.TaskStatus taskStatuses)
        {
            if (ModelState.IsValid)
            {
                db.TaskStatuses.Add(taskStatuses);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(taskStatuses);
        }

        // GET: TaskStatuses/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.TaskStatus taskStatuses = await db.TaskStatuses.FindAsync(id);
            if (taskStatuses == null)
            {
                return HttpNotFound();
            }
            return View(taskStatuses);
        }

        // POST: TaskStatuses/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StatusId,StatusName")] Models.TaskStatus taskStatuses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskStatuses).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taskStatuses);
        }

        // GET: TaskStatuses/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.TaskStatus taskStatuses = await db.TaskStatuses.FindAsync(id);
            if (taskStatuses == null)
            {
                return HttpNotFound();
            }
            return View(taskStatuses);
        }

        // POST: TaskStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Models.TaskStatus taskStatuses = await db.TaskStatuses.FindAsync(id);
            db.TaskStatuses.Remove(taskStatuses);
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
