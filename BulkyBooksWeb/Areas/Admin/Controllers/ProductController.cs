using BulkyBooks.DataAccess;
using BulkyBooks.DataAccess.Repository.IRepository;
using BulkyBooks.Models;
using BulkyBooks.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBooksWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objCoverTypeList = _UnitOfWork.Product.GetAll();
            return View(objCoverTypeList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.Product.Add(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Cover Type created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _UnitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
                ),
                CoverTypeList = _UnitOfWork.CoverType.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
                )
            };
            

            if (id == null || id == 0)
            {
                //Create Product
                //ViewBag.CategoryList = CategoryList;
                ////ViewBag.CoverTypeList = CoverTypeList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(productVM);
            }
            else
            {
                //Update Product
            }

            return View(productVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //_UnitOfWork.Product.Update(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Cover Type Updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var coverTypeFromDb = _UnitOfWork.Product.GetFirstOrDefailt(u => u.Id == id);

            if (coverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _UnitOfWork.Product.GetFirstOrDefailt(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _UnitOfWork.Product.Remove(obj);
            _UnitOfWork.Save();
            TempData["success"] = "CoverType Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
