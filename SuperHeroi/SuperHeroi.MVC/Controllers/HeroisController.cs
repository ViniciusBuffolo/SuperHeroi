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
        private readonly IHeroiPoderAppService _heroiPoderAppService;

        public HeroisController(
            IHeroiAppService heroiAppService,
            IPoderAppService poderAppService,
            IHeroiPoderAppService heroiPoderAppService)
        {
            _heroiAppService = heroiAppService;
            _poderAppService = poderAppService;
            _heroiPoderAppService = heroiPoderAppService;
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
                _heroiAppService.Add(heroiViewModel);

                foreach (var itemPoderAssigned in heroiViewModel.PoderAssignedList.Where(x => x.Assigned))
                {
                    //heroiViewModel.HeroisPoderes.Add(_heroiPoderAppService.GetById(itemPoderAssigned.PoderId));
                    var model = new HeroiPoderViewModel();
                    model.HeroiId = heroiViewModel.HeroiId;
                    model.PoderId = itemPoderAssigned.PoderId;

                    _heroiPoderAppService.Add(model);
                }
                return RedirectToAction("Index");
            }

            return View(heroiViewModel);
        }

        // GET: Herois/Edit/5
        public ActionResult Edit(Guid id)
        {
            var heroiViewModel = _heroiAppService.ObterHeroiCompleto(id).First();
            var poderAll = _poderAppService.GetAll();

            var poderAssigned = new List<PoderAssigned>();

            foreach (var itemPoder in poderAll)
            {
                var obj = new PoderAssigned()
                {
                    PoderId = itemPoder.PoderId,
                    Descricao = itemPoder.Descricao,
                    Assigned = false
                };
                poderAssigned.Add(obj);
            }

            foreach (var itemAssigned in poderAssigned)
            {
                foreach (var itemPoder in heroiViewModel.HeroisPoderes)
                {
                    if (itemPoder.PoderId == itemAssigned.PoderId)
                    {
                        itemAssigned.Assigned = true;
                    }
                }
            }

            heroiViewModel.PoderAssignedList.Clear();
            heroiViewModel.PoderAssignedList = poderAssigned;

            return View(heroiViewModel);
        }

        // POST: Herois/Edit/5
        [HttpPost]
        public ActionResult Edit(HeroiViewModel heroiViewModel)
        {
            if (ModelState.IsValid)
            {
                _heroiAppService.Update(heroiViewModel);

                var listPoder = _heroiPoderAppService.BuscarPoderPorIdHeroi(heroiViewModel.HeroiId);
                foreach (var itemPoder in listPoder)
                {
                    _heroiPoderAppService.Remove(itemPoder);
                }

                foreach (var itemPoderAssigned in heroiViewModel.PoderAssignedList.Where(x => x.Assigned))
                {
                    var model = new HeroiPoderViewModel
                    {
                        HeroiId = heroiViewModel.HeroiId,
                        PoderId = itemPoderAssigned.PoderId
                    };

                    _heroiPoderAppService.Add(model);
                }

                return RedirectToAction("Index");
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
