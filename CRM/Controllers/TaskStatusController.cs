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
using PagedList;

namespace CRM.Controllers
{
    public class TaskStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TaskStatus
        [Authorize(Roles = "admin, manager")]
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            // возвращаем список статусов, сортируем по наименованию
            IPagedList<Models.TaskStatus> status = db.TaskStatuses.OrderByDescending(x => x.StatusName).ToPagedList(pageNumber, pageSize);
            return View(status);
        }

        // GET: TaskStatus/Details/5
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.TaskStatus taskStatus = await db.TaskStatuses.FindAsync(id);
            if (taskStatus == null)
            {
                return HttpNotFound();
            }
            return View(taskStatus);
        }

        // GET: TaskStatus/Create
        [Authorize(Roles = "admin, manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskStatus/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Create([Bind(Include = "StatusId,StatusName")] Models.TaskStatus taskStatus)
        {
            if (ModelState.IsValid)
            {
                taskStatus.StatusId = Guid.NewGuid();
                db.TaskStatuses.Add(taskStatus);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(taskStatus);
        }

        // GET: TaskStatus/Edit/5
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.TaskStatus taskStatus = await db.TaskStatuses.FindAsync(id);
            if (taskStatus == null)
            {
                return HttpNotFound();
            }
            return View(taskStatus);
        }

        // POST: TaskStatus/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Edit([Bind(Include = "StatusId,StatusName")] Models.TaskStatus taskStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskStatus).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taskStatus);
        }

        // GET: TaskStatus/Delete/5
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.TaskStatus taskStatus = await db.TaskStatuses.FindAsync(id);
            if (taskStatus == null)
            {
                return HttpNotFound();
            }
            return View(taskStatus);
        }

        // POST: TaskStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Models.TaskStatus taskStatus = await db.TaskStatuses.FindAsync(id);
            db.TaskStatuses.Remove(taskStatus);
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
