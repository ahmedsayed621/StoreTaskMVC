using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreTaskMVC.Data;
using StoreTaskMVC.Interfaces;
using StoreTaskMVC.Models;


namespace StoreTaskMVC.Controllers
{
    public class StoresController : Controller
    {





        private readonly IStoreService _storeService;
        private readonly ISpaceService _spaceService;

        public StoresController(IStoreService storeService,  ISpaceService spaceService)
        {
            _storeService = storeService;
            _spaceService = spaceService;
        }


        [HttpGet]
        public IActionResult Index()
        {

            var model = _storeService.Get();
            
            return View(model);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Store store)
        {
            
            if (ModelState.IsValid)
            {
                _storeService.Create(store);
                return RedirectToAction("Index");
            }
            return View(store);
        }

        public IActionResult Details(int id)
        {

            var model = _storeService.GetById(id);

            ViewBag.Spaces = _spaceService.Get();
            
            return View(model);
        }


        public IActionResult Edit(int id)
        {
            var model = _storeService.GetById(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Store model) {
            try
            {
                if (ModelState.IsValid)
                {
                    _storeService.Update(model);
                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                return View();
            }

            return View(model);
            
        }

        public IActionResult Delete(int id)
        {
            var model = _storeService.GetById(id);
            
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {

            try
            {
                var model = _storeService.GetById(id);
                _storeService.Delete(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }

        }







    }
}
