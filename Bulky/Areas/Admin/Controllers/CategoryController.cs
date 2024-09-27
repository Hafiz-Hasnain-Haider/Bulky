using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;

namespace Bulky.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitofwork _unitofwork;

        public CategoryController(IUnitofwork db)
        {
            _unitofwork = db;
        }

        public IActionResult Xyz()
        {
            List<Category> categories = _unitofwork.Category.GetAll().ToList();

            return View(categories);
        }
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot Same as Name");
            }

            if (ModelState.IsValid)
            {
                _unitofwork.Category.Add(obj);
                _unitofwork.Save();
                TempData["Success"] = "Category Create Successfully";

                return RedirectToAction("Xyz");
            }
            return View();

        }
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {


                return NotFound();
            }
            Category category = _unitofwork.Category.Get(u => u.ID == Id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {


            if (ModelState.IsValid)
            {
                _unitofwork.Category.Update(obj);
                _unitofwork.Save();
                TempData["Success"] = "Category Edit Successfully";

                return RedirectToAction("Xyz");
            }
            return View();

        }
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {


                return NotFound();
            }
            Category category = _unitofwork.Category.Get(u => u.ID == Id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category category = _unitofwork.Category.Get(u => u.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            _unitofwork.Category.Remove(category);
            _unitofwork.Save();
            TempData["Success"] = "Category Delete Successfully";
            return RedirectToAction("Xyz");

        }
    }
}
