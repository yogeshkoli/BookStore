namespace BookStore.Controllers;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookStore.Models;
using BookStore.DataAccess;
using BookStore.DataAccess.Repository.IRepository;

public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;
    // private readonly StoreContext _store_context;
    private readonly IUnitOfWork _iUnitOfWork;

    public CategoryController(ILogger<CategoryController> logger, IUnitOfWork iUnitOfWork)
    {
        _logger = logger;
        // _store_context = store_context;
        _iUnitOfWork = iUnitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Category> categoryList = _iUnitOfWork.iCategoryRepository.GetAll();
        return View(categoryList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _iUnitOfWork.iCategoryRepository.Add(category);
            _iUnitOfWork.Save();
            TempData["success"] = "Category Created successfully!";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        // var category = _store_context.Categories.Find(id);
        var category = _iUnitOfWork.iCategoryRepository.GetFirstOrDefault(u=>u.Id==id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _iUnitOfWork.iCategoryRepository.Update(category);
            _iUnitOfWork.Save();
            TempData["success"] = "Category Updated successfully!";
            return RedirectToAction("Index");
        }
        return View();
    }

    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var category = _iUnitOfWork.iCategoryRepository.GetFirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteHandler(int? id)
    {
        var category = _iUnitOfWork.iCategoryRepository.GetFirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        _iUnitOfWork.iCategoryRepository.Remove(category);
        _iUnitOfWork.Save();
        TempData["success"] = "Category Deleted successfully!";
        return RedirectToAction("Index");

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}