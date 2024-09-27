using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;

namespace Bulky.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitofwork _unitofwork;

        public ProductController(IUnitofwork db)
        {
            _unitofwork = db;
        }

        public IActionResult Xyz()
        {
            List<Product> products = _unitofwork.Product.GetAll().ToList();

            return View(products);
        }
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
           

            if (ModelState.IsValid)
            {
                _unitofwork.Product.Add(obj);
                _unitofwork.Save();
                TempData["Success"] = "Product Create Successfully";

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
            Product product = _unitofwork.Product.Get(u => u.ID == Id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {


            if (ModelState.IsValid)
            {
                _unitofwork.Product.Update(obj);
                _unitofwork.Save();
                TempData["Success"] = "Product Edit Successfully";

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
            Product product = _unitofwork.Product.Get(u => u.ID == Id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product product = _unitofwork.Product.Get(u => u.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            _unitofwork.Product.Remove(product);
            _unitofwork.Save();
            TempData["Success"] = "Product Delete Successfully";
            return RedirectToAction("Xyz");

        }
    }
}
