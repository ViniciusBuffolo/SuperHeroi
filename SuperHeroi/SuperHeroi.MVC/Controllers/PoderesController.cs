using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperHeroi.Application.Interfaces;
using SuperHeroi.Application.ViewModels;

namespace SuperHeroi.MVC.Controllers
{
    public class PoderesController : Controller, IDisposable
    {
        private readonly IPoderAppService _poderAppService;

        public PoderesController(IPoderAppService poderAppService)
        {
            _poderAppService = poderAppService;
        }

        // GET: Poderes
        public ActionResult Index()
        {
            return View(_poderAppService.GetAll());
        }

        // GET: Poderes/Details/5
        public ActionResult Details(Guid id)
        {
            return View(_poderAppService.GetById(id));
        }

        // GET: Poderes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Poderes/Create
        [HttpPost]
        public ActionResult Create(PoderViewModel poderViewModel)
        {
            if (ModelState.IsValid)
            {
                _poderAppService.Add(poderViewModel);

                return RedirectToAction("Index");
            }

            return View(poderViewModel);
        }

        // GET: Poderes/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(_poderAppService.GetById(id));
        }

        // POST: Poderes/Edit/5
        [HttpPost]
        public ActionResult Edit(PoderViewModel poderePoderViewModel)
        {
            if (ModelState.IsValid)
            {
                _poderAppService.Update(poderePoderViewModel);

                return RedirectToAction("Index");
            }

            return View(poderePoderViewModel);
        }

        // GET: Poderes/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(_poderAppService.GetById(id));
        }

        // POST: Poderes/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _poderAppService.Remove(_poderAppService.GetById(id));

            return RedirectToAction("Index");
        }

        void IDisposable.Dispose()
        {
            _poderAppService.Dispose();
        }
    }
}
