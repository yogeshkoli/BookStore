namespace BookStore.Controllers;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookStore.Models;
using BookStore.Data;

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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}