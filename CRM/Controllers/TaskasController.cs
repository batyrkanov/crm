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
using CRM.Defence;
using PagedList;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace CRM.Controllers
{
    [ProtectSqlIAndXssAttack]
    public class TaskasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // метод для отдельной пагинации задач принимающая номер страницы page
        [Authorize(Roles = "admin, manager")]
        public ActionResult AjaxPositionList(int? page)
        {
            int pageSize = 10; // размер страницы (сколько записей отображать)
            int pageNumber = (page ?? 1); //номер страницы
            return PartialView(db.Tasks.OrderByDescending(x => x.TaskDate).ToPagedList(pageNumber, pageSize));
        }

        // GET: Taskas
        // просмотр всех задач
        [Authorize(Roles = "admin, manager")]
        public ActionResult Index(int? page, string searching, string SelectedCategory, string SelectedStatus, string SelectedUser)
        {
            
            int pageSize = 10;// размер страницы (сколько записей отображать)
            int pageNumber = (page ?? 1); //номер страницы
            
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
                taskas = task.Where(s => s.StatusName.Trim().Equals(SelectedStatus.Trim())).ToPagedList(pageNumber, pageSize); ;
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
                                 group s by s.StatusName into newGroup
                                 where newGroup.Key != null
                                 orderby newGroup.Key
                                 select new { TaskStatus = newGroup.Key };
            ViewBag.UniqStatusesList = StatusesList.Select(c => new SelectListItem { Value = c.TaskStatus, Text = c.TaskStatus }).ToPagedList(pageNumber, pageSize); ;

            ViewBag.SelectedCategory = SelectedCategory;
            ViewBag.SelectedUser = SelectedUser;
            ViewBag.SelectedStatus = SelectedStatus;
            #endregion

            // собираем список менеджеров
            string[] managers = db.Tasks.Select(x => x.ManagerName).ToArray();
            foreach(var manager in managers)
            {
                // проверяем содержится ли в именах менеджеров логин нынешнего пользователя или админа
                // если да, то передаем во вьюшку true
                if (manager.Contains(User.Identity.Name))
                {
                    ViewBag.User = manager;
                }
            }
                
            return View(taskas);
        }

        [Authorize(Roles = "admin, manager")]
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
                taskas = task.Where(s => s.StatusName.Trim().Equals(SelectedStatus.Trim())).ToPagedList(pageNumber, pageSize); ;
            }


            var CategoriesList = from s in task
                                 group s by s.CategoryName into newGroup
                                 where newGroup.Key != null
                                 orderby newGroup.Key
                                 select new { CategoryName = newGroup.Key };
            ViewBag.UniqCategoriesList = CategoriesList.Select(c => new SelectListItem { Value = c.CategoryName, Text = c.CategoryName }).ToPagedList(pageNumber, pageSize); ;
            
            var StatusesList = from s in task
                               group s by s.StatusName into newGroup
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
        [Authorize(Roles = "admin, manager")]
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
            // собираем список менеджеров
            // проверяем содержится ли в именах менеджеров логин нынешнего пользователя или админа
            // если да, то передаем во вьюшку true
            if (taska.ManagerName.Contains(User.Identity.Name))
                ViewBag.User = taska.ManagerName;
            
            return View(taska);
        }

        // GET: Taskas/Create
        [Authorize(Roles = "admin, manager")]
        public ActionResult Create()
        {
            List<Category> category = db.Categories.ToList();
            ViewBag.CategoriesList = new SelectList(category, "CategoryName", "CategoryName");

            List<Company> companies = db.Companies.ToList();
            ViewBag.CompaniesList = new SelectList(companies, "CompanyName", "CompanyName");

            List<Models.TaskStatus> statuses = db.TaskStatuses.ToList();
            ViewBag.StatusesList = new SelectList(statuses, "StatusName", "StatusName");
            return View();
        }

        // POST: Taskas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Create(Taska taska)
        {
            var DateTimeNow = DateTime.Now;
            List<Category> category = db.Categories.ToList();
            ViewBag.CategoriesList = new SelectList(category, "CategoryName", "CategoryName");

            List<Company> companies = db.Companies.ToList();
            ViewBag.CompaniesList = new SelectList(companies, "CompanyName", "CompanyName");

            List<Models.TaskStatus> statuses = db.TaskStatuses.ToList();
            ViewBag.StatusesList = new SelectList(statuses, "StatusName", "StatusName");

            if(taska.LastMeetingDate > DateTimeNow)
            {
                ViewBag.LastDateError = "Дата последней встречи не может превышать нынешнюю дату";
                return View(taska);
            }

            if (taska.TaskDate < DateTimeNow)
            {
                ViewBag.DateError = "Дата намеченной встречи не может быть меньше нынешней даты";
                return View(taska);
            }

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
        [Authorize(Roles = "admin, manager")]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Edit(Taska taska)
        {
            var DateTimeNow = DateTime.Now;

            List<Category> category = db.Categories.ToList();
            ViewBag.CategoriesList = new SelectList(category, "CategoryName", "CategoryName");

            List<Company> companies = db.Companies.ToList();
            ViewBag.CompaniesList = new SelectList(companies, "CompanyName", "CompanyName");

            List<Models.TaskStatus> statuses = db.TaskStatuses.ToList();
            ViewBag.StatusesList = new SelectList(statuses, "StatusName", "StatusName");

            if (taska.LastMeetingDate > DateTimeNow)
            {
                ViewBag.LastDateError = "Дата последней встречи не может превышать нынешнюю дату";
                return View(taska);
            }

            if (taska.TaskDate < DateTimeNow)
            {
                ViewBag.DateError = "Дата намеченной встречи не может быть меньше нынешней даты";
                return View(taska);
            }
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
        [Authorize(Roles = "admin, manager")]
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
        [Authorize(Roles = "admin, manager")]
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
