using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperHeroi.Application.Interfaces;
using SuperHeroi.Application.ViewModels;

namespace SuperHeroi.MVC.Controllers
{
    public class HeroisController : Controller, IDisposable
    {
        private readonly IHeroiAppService _heroiAppService;

        public HeroisController(IHeroiAppService heroiAppService)
        {
            _heroiAppService = heroiAppService;
        }

        // GET: Herois
        public ActionResult Index()
        {
            return View(_heroiAppService.GetAll());
        }

        // GET: Herois/Details/5
        public ActionResult Details(Guid id)
        {
            return View(_heroiAppService.GetById(id));
        }

        // GET: Herois/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Herois/Create
        [HttpPost]
        public ActionResult Create(HeroiViewModel heroiViewModel)
        {
            if (ModelState.IsValid)
            {
                _heroiAppService.Add(heroiViewModel);

                return RedirectToAction("Index;");
            }

            return View(heroiViewModel);
        }

        // GET: Herois/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(_heroiAppService.GetById(id));
        }

        // POST: Herois/Edit/5
        [HttpPost]
        public ActionResult Edit(HeroiViewModel heroiViewModel)
        {
            if (ModelState.IsValid)
            {
                _heroiAppService.Update(heroiViewModel);

                return RedirectToAction("Index;");
            }

            return View(heroiViewModel);
        }

        // GET: Herois/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(_heroiAppService.GetById(id));
        }

        // POST: Herois/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _heroiAppService.Remove(_heroiAppService.GetById(id));

            return RedirectToAction("Index");
        }

        void IDisposable.Dispose()
        {
            _heroiAppService.Dispose();
        }
    }
}
