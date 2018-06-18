using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using PagedList;
using CRM.Models;
using CRM.Data;
using CRM.Models.Interfaces;

namespace CRM.Controllers
{
    public class CategoriesController : Controller
    {
        IUnitOfWork unitOfWork;

        public CategoriesController()
        {
            this.unitOfWork = new UnitOfWork();
        }
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public CategoriesController(UnitOfWork unit) => unitOfWork = unit;
        
        // GET: Categories
        [Authorize(Roles = "admin, manager")]
        public ViewResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            // возвращаем список категорий, сортируем по наименованию
            IPagedList<Category> category = unitOfWork.Categories.GetAll().ToPagedList(pageNumber, pageSize);
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
            Category category = unitOfWork.Categories.GetById(id);
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
                    unitOfWork.Categories.Create(category);
                    unitOfWork.Save();
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
            Category category = unitOfWork.Categories.GetById(id);
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
                    unitOfWork.Categories.Update(category);
                    unitOfWork.Save();
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
            Category category = unitOfWork.Categories.GetById(id);
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
            
            unitOfWork.Categories.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
