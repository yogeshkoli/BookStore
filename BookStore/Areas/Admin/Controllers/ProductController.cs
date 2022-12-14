namespace BookStore.Areas.Admin.Controllers;

using System.Collections.Generic;
using BookStore.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.Models.ViewModels;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;

    private readonly IUnitOfWork _iUnitOfWork;

    public ProductController(ILogger<ProductController> logger, IUnitOfWork iUnitOfWork)
    {
        _logger = logger;
        _iUnitOfWork = iUnitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> productList = _iUnitOfWork.iProductRepository.GetAll();
        return View(productList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _iUnitOfWork.iProductRepository.Add(product);
            _iUnitOfWork.Save();
            TempData["success"] = "New Product has been successfully created";
            return RedirectToAction("Index");
        }

        return View();
    }

    [HttpGet]
    public IActionResult Upsert(int? id)
    {
        ProductViewModel productViewModel = new(){
            Product = new(),
            CategoryList = _iUnitOfWork.iCategoryRepository.GetAll().Select(
                c => new SelectListItem{
                    Text = c.Name,
                    Value = c.Id.ToString()
                }
            ),
            CoverTypeList = _iUnitOfWork.iCoverTypeRepository.GetAll().Select(
                ct => new SelectListItem{
                    Text = ct.Name,
                    Value = ct.Id.ToString()
                }
            )
        };

        if (id == null || id == 0)
        {
           return View(productViewModel);
        }

        // var product = _iUnitOfWork.iProductRepository.GetFirstOrDefault(p => p.Id == id);

        // if (product == null)
        // {
        //     return NotFound();
        // }

        productViewModel.Product = _iUnitOfWork.iProductRepository.GetFirstOrDefault(p => p.Id == id);

        return View(productViewModel);
    } 

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _iUnitOfWork.iProductRepository.Update(product);
            _iUnitOfWork.Save();
            TempData["success"] = "Product has been successfully updated.";
            return RedirectToAction("Index");
        }

        return View();
    }

    public IActionResult Delete(int? id)
    {
        var product = _iUnitOfWork.iProductRepository.GetFirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult handleDelete(int? id)
    {
        var product = _iUnitOfWork.iProductRepository.GetFirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        _iUnitOfWork.iProductRepository.Remove(product);
        _iUnitOfWork.Save();
        TempData["success"] = "Given product deleted successfully.";

        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}