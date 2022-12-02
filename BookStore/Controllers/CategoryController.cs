namespace BookStore.Controllers;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookStore.Models;
using BookStore.DataAccess;

public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;
    private readonly StoreContext _store_context;

    public CategoryController(ILogger<CategoryController> logger, StoreContext store_context)
    {
        _logger = logger;
        _store_context = store_context;
    }

    public IActionResult Index()
    {
        IEnumerable<Category> categoryList = _store_context.Categories;
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
            _store_context.Categories.Add(category);
            _store_context.SaveChanges();
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

        var category = _store_context.Categories.Find(id);
        // var category = _store_context.Categories.FirstOrDefault(u=>u.Id==id);

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
            _store_context.Categories.Update(category);
            _store_context.SaveChanges();
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

        var category = _store_context.Categories.Find(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteHandler(int? id)
    {
        var category = _store_context.Categories.Find(id);

        if (category == null)
        {
            return NotFound();
        }

        _store_context.Categories.Remove(category);
        _store_context.SaveChanges();
        TempData["success"] = "Category Deleted successfully!";
        return RedirectToAction("Index");

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}