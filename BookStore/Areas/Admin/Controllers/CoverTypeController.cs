namespace BookStore.Areas.Admin.Controllers;

using System.Collections.Generic;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class CoverTypeController : Controller
{
    private readonly ILogger<CoverTypeController> _logger;

    private readonly IUnitOfWork _iUnitOfWork;

    public CoverTypeController(ILogger<CoverTypeController> logger, IUnitOfWork iUnitOfWork)
    {
        _logger = logger;
        _iUnitOfWork = iUnitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<CoverType> coverTypeList = _iUnitOfWork.iCoverTypeRepository.GetAll();
        return View(coverTypeList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType coverType)
    {
        if (ModelState.IsValid)
        {
            _iUnitOfWork.iCoverTypeRepository.Add(coverType);
            _iUnitOfWork.Save();

            TempData["success"] = "Cover Type has been successfully created.";
            return RedirectToAction("Index");
        }

        return View();
    }

    public IActionResult Edit(int? id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }

        var coverType = _iUnitOfWork.iCoverTypeRepository.GetFirstOrDefault(c => c.Id == id);

        if (coverType == null)
        {
            return NotFound();
        }

        return View(coverType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType coverType)
    {
        if (ModelState.IsValid)
        {
            _iUnitOfWork.iCoverTypeRepository.Update(coverType);
            _iUnitOfWork.Save();

            TempData["success"] = "Cover Type has been updated successfully.";
            return RedirectToAction("Index");
        }

        return View();
    }

    public IActionResult Delete(int? id)
    {
        if(id == null || id == 0)
        {
            return NotFound();
        }

        var coverType = _iUnitOfWork.iCoverTypeRepository.GetFirstOrDefault(c => c.Id == id);

        if(coverType == null)
        {
            return NotFound();
        }

        return View(coverType);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteHandler(int? id)
    {
        if(id == null || id == 0)
        {
            return NotFound();
        }

        var coverType = _iUnitOfWork.iCoverTypeRepository.GetFirstOrDefault(c => c.Id == id);

        if(coverType == null)
        {
            return NotFound();
        }

        _iUnitOfWork.iCoverTypeRepository.Remove(coverType);
        _iUnitOfWork.Save();
        TempData["success"] = "Give cover type has been deleted successfully.";
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}