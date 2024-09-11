
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            
            return View(objCompanyList);
        }

        public IActionResult Upsert(int? id)
        {
           
            if (id == null || id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company companyobj = _unitOfWork.Company.Get(u=>u.Id==id);
                return View(companyobj);
            }

            
        }
        [HttpPost]
        public IActionResult Upsert(Company Companyobj)
        {
            if (ModelState.IsValid)
            {
                if (Companyobj.Id == 0)
                {
                    _unitOfWork.Company.Add(Companyobj);
                    TempData["success"] = "Company created successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(Companyobj);
                    TempData["success"] = "Company updated successfully";
                }
                
                _unitOfWork.save();
                
                return RedirectToAction("Index");
            }
            else
            {
                
            }
            return View(Companyobj);

        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
        //    //Company? CompanyFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
        //    //Company? CompanyFromDb2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();

        //    if (CompanyFromDb == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(CompanyFromDb);
        //}
        //[HttpPost]
        //public IActionResult Edit(Company obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Company.update(obj);
        //        _unitOfWork.save();
        //        TempData["success"] = "Company updated successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();

        //}

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.Id == id);

        //    if (CompanyFromDb == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(CompanyFromDb);
        //}
        //[HttpPost]
        //public IActionResult Delete(Company obj)
        //{
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Company.Remove(obj);
        //    _unitOfWork.save();
        //    TempData["success"] = "Company deleted successfully";
        //    return RedirectToAction("Index");

        //}

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            
            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.save();

            return Json(new { success = true, message = "Delete Successful" });

           
        }
        #endregion

    }
}
