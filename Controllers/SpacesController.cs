using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreTaskMVC.Interfaces;
using StoreTaskMVC.Models;
using StoreTaskMVC.Services;

namespace StoreTaskMVC.Controllers
{
    public class SpacesController : Controller
    {
        private readonly ISpaceService _spaceService;
        private readonly IStoreService _storeService;

        public SpacesController(ISpaceService spaceService, IStoreService storeService)
        {
            _spaceService = spaceService;
            _storeService = storeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        

        public IActionResult Split()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Split(Space space, int numberOfSplits)
        {
            if(ModelState.IsValid)
            {
                _spaceService.Split(space.Id, numberOfSplits);
                return RedirectToRoute(new { controller = "Stores", action = "Details", id = space.StoreId });

            }

            return View();
        }

        public IActionResult Merge()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Merge(Space space, int spaceId2)
        {
            if(ModelState.IsValid)
            {
                _spaceService.Merge(space.Id, spaceId2);

                return RedirectToRoute(new { controller = "Stores", action = "Details", id = space.StoreId });

            }

            return View();
        }

        public IActionResult Details(int id)
        {

            var model = _spaceService.GetById(id);

            return View(model);
        }

        public IActionResult Edit(int id)
        {

            var model = _spaceService.GetById(id);
            var storeModel = _storeService.Get();
            ViewBag.StoreList = new SelectList(storeModel, "Id", "StoreName");


            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Space space)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _spaceService.Update(space);
                    var storeModel = _storeService.Get();
                    ViewBag.StoreList = new SelectList(storeModel, "Id", "StoreName");

                    return RedirectToRoute(new { controller = "Stores", action = "Details", id = space.StoreId });
                }
            }catch(Exception ex)
            {
                var storeModel = _storeService.Get();
                ViewBag.StoreList = new SelectList(storeModel, "Id", "StoreName");

                return View();
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            var model = _spaceService.GetById(id);

            var storeModel = _storeService.Get();
            ViewBag.StoreList = new SelectList(storeModel, "Id", "StoreName");

            return View(model);
        }


        [HttpPost,ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                var model = _spaceService.GetById(id);

                _spaceService.Delete(model);
                return RedirectToRoute(new { controller = "Stores", action = "Details", id = model.StoreId });
            }
            catch(Exception ex)
            {
                return View();
            }
        }



    }
}
