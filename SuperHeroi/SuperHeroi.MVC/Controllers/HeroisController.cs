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
        private readonly IPoderAppService _poderAppService;

        public HeroisController(
            IHeroiAppService heroiAppService,
            IPoderAppService poderAppService)
        {
            _heroiAppService = heroiAppService;
            _poderAppService = poderAppService;
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
            var poderAll = _poderAppService.GetAll();

            var heroiViewModel = new HeroiViewModel()
            {
                PoderAssignedList = new List<PoderAssigned>()
            };

            foreach (var itemPoder in poderAll)
            {
                var obj = new PoderAssigned()
                {
                    PoderId = itemPoder.PoderId,
                    Descricao = itemPoder.Descricao,
                    Assigned = false
                };

                heroiViewModel.PoderAssignedList.Add(obj);
            }

            return View(heroiViewModel);
        }

        // POST: Herois/Create
        [HttpPost]
        public ActionResult Create(HeroiViewModel heroiViewModel)
        {
            if (ModelState.IsValid)
            {
                foreach (var itemPoderAssigned in heroiViewModel.PoderAssignedList.Where(x => x.Assigned))
                {
                    heroiViewModel.Poderes.Add(_poderAppService.GetById(itemPoderAssigned.PoderId));
                }

                _heroiAppService.Add(heroiViewModel);

                return RedirectToAction("Index");
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
            _poderAppService.Dispose();
        }
    }
}
