using BulkyBooks.DataAccess;
using BulkyBooks.DataAccess.Repository;
using BulkyBooks.DataAccess.Repository.IRepository;
using BulkyBooks.Models;
using BulkyBooks.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;

namespace BulkyBooksWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _UnitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
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
                productVM.Product = _UnitOfWork.Product.GetFirstOrDefailt(u=>u.Id == id);
                return View(productVM);
                //Update Product
            }

            
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Product.ImgUrl!= null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath,obj.Product.ImgUrl.Trim('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var filestreams = new FileStream(Path.Combine(uploads,fileName+extension),FileMode.Create))
                    {
                        file.CopyTo(filestreams);
                    }

                    obj.Product.ImgUrl = @"\images\products\" + fileName + extension;
                }

                if (obj.Product.Id == 0)
                {
                    _UnitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    _UnitOfWork.Product.Update(obj.Product);
                }
                _UnitOfWork.Save();
                TempData["success"] = "Product Created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        #region APY CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _UnitOfWork.Product.GetAll("Category,CoverType");
            return Json (new {Data= productList});
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _UnitOfWork.Product.GetFirstOrDefailt(u => u.Id == id);

            if (obj == null)
            {
                return Json(new {success =false, message = "Error While Deleting"});
            }


            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImgUrl.Trim('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _UnitOfWork.Product.Remove(obj);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }



}
