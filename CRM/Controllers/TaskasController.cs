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
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace CRM.Controllers
{
    public class TaskasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AjaxPositionList(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return PartialView(db.Tasks.OrderByDescending(x => x.TaskDate).ToPagedList(pageNumber, pageSize));
        }

        // GET: Taskas
        public ActionResult Index(int? page, string searching, string SelectedCategory, string SelectedStatus, string SelectedUser)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            // возвращаем список задач, сортируем по дате
            IPagedList<Taska> taskas = db.Tasks.Where(x => x.CompanyName.Contains(searching) || searching == null).OrderByDescending(x => x.TaskDate).ToPagedList(pageNumber, pageSize);

            #region фильтрация данных
            var rawData = (from s in db.Tasks
                           select s).ToList();

            var task = from s in rawData
                       select s;
            if (!String.IsNullOrEmpty(SelectedCategory))
            {
                taskas = task.Where(s => s.CategoryName.Trim().Equals(SelectedCategory.Trim())).ToPagedList(pageNumber, pageSize); ;
            }

            if (!String.IsNullOrEmpty(SelectedStatus))
            {
                taskas = task.Where(s => s.TaskStatus.Trim().Equals(SelectedStatus.Trim())).ToPagedList(pageNumber, pageSize); ;
            }

            if (!String.IsNullOrEmpty(SelectedUser))
            {
                taskas = task.Where(s => s.ManagerName.Trim().Equals(SelectedUser.Trim())).ToPagedList(pageNumber, pageSize); ;
            }

            var CategoriesList = from s in task
                                 group s by s.CategoryName into newGroup
                                 where newGroup.Key != null
                                 orderby newGroup.Key
                                 select new { CategoryName = newGroup.Key };
            ViewBag.UniqCategoriesList = CategoriesList.Select(c => new SelectListItem { Value = c.CategoryName, Text = c.CategoryName }).ToPagedList(pageNumber, pageSize); ;


            var ManagerList = from s in task
                                 group s by s.ManagerName into newGroup
                                 where newGroup.Key != null
                                 orderby newGroup.Key
                                 select new { ManagerName = newGroup.Key };
            ViewBag.UniqManagerList = ManagerList.Select(c => new SelectListItem { Value = c.ManagerName, Text = c.ManagerName }).ToPagedList(pageNumber, pageSize); ;

           
            var StatusesList = from s in task
                                 group s by s.TaskStatus into newGroup
                                 where newGroup.Key != null
                                 orderby newGroup.Key
                                 select new { TaskStatus = newGroup.Key };
            ViewBag.UniqStatusesList = StatusesList.Select(c => new SelectListItem { Value = c.TaskStatus, Text = c.TaskStatus }).ToPagedList(pageNumber, pageSize); ;

            ViewBag.SelectedCategory = SelectedCategory;
            ViewBag.SelectedUser = SelectedUser;
            ViewBag.SelectedStatus = SelectedStatus;
            #endregion

            return View(taskas);
        }

        public ActionResult OwnIndex(int? page, string searching, string SelectedCategory, string SelectedStatus)
        {

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            IPagedList<Taska> taskas = db.Tasks.Where(x => x.ManagerName.Contains(User.Identity.Name) && (x.CompanyName.Contains(searching) || searching == null)).OrderByDescending(x => x.TaskDate).ToPagedList(pageNumber, pageSize);

            #region фильтрация данных
            var rawData = (from s in db.Tasks
                           select s).ToList();

            var task = from s in rawData
                       select s;
            if (!String.IsNullOrEmpty(SelectedCategory))
            {
                taskas = task.Where(s => s.CategoryName.Trim().Equals(SelectedCategory.Trim())).ToPagedList(pageNumber, pageSize); ;
            }

            if (!String.IsNullOrEmpty(SelectedStatus))
            {
                taskas = task.Where(s => s.TaskStatus.Trim().Equals(SelectedStatus.Trim())).ToPagedList(pageNumber, pageSize); ;
            }


            var CategoriesList = from s in task
                                 group s by s.CategoryName into newGroup
                                 where newGroup.Key != null
                                 orderby newGroup.Key
                                 select new { CategoryName = newGroup.Key };
            ViewBag.UniqCategoriesList = CategoriesList.Select(c => new SelectListItem { Value = c.CategoryName, Text = c.CategoryName }).ToPagedList(pageNumber, pageSize); ;
            
            var StatusesList = from s in task
                               group s by s.TaskStatus into newGroup
                               where newGroup.Key != null
                               orderby newGroup.Key
                               select new { TaskStatus = newGroup.Key };
            ViewBag.UniqStatusesList = StatusesList.Select(c => new SelectListItem { Value = c.TaskStatus, Text = c.TaskStatus }).ToPagedList(pageNumber, pageSize); ;

            ViewBag.SelectedCategory = SelectedCategory;
            ViewBag.SelectedStatus = SelectedStatus;
            #endregion

            // возвращаем список задач созданных авторизованным пользователем, сортиируем по дате
            return View(taskas);
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
            
            ViewBag.CategoriesList = new SelectList(db.Categories, "CategoryName", "CategoryName");
            ViewBag.CompaniesList = new SelectList(db.Companies, "CompanyName", "CompanyName");
            ViewBag.StatusesList = new SelectList(db.TaskStatuses, "StatusName", "StatusName");
            return View();
        }

        // POST: Taskas/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Taska taska)
        {
          
            if (ModelState.IsValid)
            {
                var currentUser = db.Users.Find(User.Identity.GetUserId());
                string namePlusLogin = currentUser.Name + " " + currentUser.Surname + " (" + User.Identity.Name + ")";
                taska.TaskId = Guid.NewGuid();
                taska.ManagerName = namePlusLogin;
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
            ViewBag.CategoriesList = new SelectList(db.Categories, "CategoryName", "CategoryName");
            ViewBag.CompaniesList = new SelectList(db.Companies, "CompanyName", "CompanyName");
            ViewBag.StatusesList = new SelectList(db.TaskStatuses, "StatusName", "StatusName");
            return View(taska);
        }

        // POST: Taskas/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Taska taska)
        {
            if (ModelState.IsValid)
            {
                var currentUser = db.Users.Find(User.Identity.GetUserId());
                string namePlusLogin = currentUser.Name + " " + currentUser.Surname + " (" + User.Identity.Name + ")";
                taska.ManagerName = namePlusLogin;
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
