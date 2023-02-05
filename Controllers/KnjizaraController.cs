using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Knjizara.Models;
using Knjizara.ViewModels;
using Knjizara.Repository;
using Knjizara.Repository.Interfaces;
using System.Runtime.CompilerServices;
using System;
using System.Reflection;

namespace Knjizara.Controllers
{
    public class KnjizaraController : Controller
    {
        public IKnjigaRepository KnjigaRepository;
        public IZanrRepository ZanrRepository;
        public KnjizaraController(IConfiguration Configuration)
        {
            KnjigaRepository = new KnjigaRepository(Configuration);
            ZanrRepository = new ZanrRepository(Configuration);
        }

        public IActionResult Index()
        {
            return View(KnjigaRepository.GetAll());
        }

        public IActionResult Izmeni(int id)
        {
            KnjigaZanrViewModel viewmodel = new KnjigaZanrViewModel();
            viewmodel.Knjiga = KnjigaRepository.GetOne(id);
            viewmodel.Zanrovi = ZanrRepository.GetAll();
            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult Izmeni(KnjigaModel knjiga)
        {
            KnjigaRepository.Update(knjiga);
            return RedirectToAction("Index");
        }

        public IActionResult Dodaj()
        {
            KnjigaZanrViewModel vm = new KnjigaZanrViewModel();
            vm.Knjiga = new KnjigaModel();
            vm.Zanrovi = ZanrRepository.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Dodaj(KnjigaModel knjiga, int idZanra)
        {
            if (KnjigaRepository.CheckIfKnjigaExists(knjiga.Naziv))
            {
                TempData["Poruka"] = $"Knjiga '{knjiga.Naziv}' vec postoji";
                KnjigaZanrViewModel viewmodel = new KnjigaZanrViewModel();
                viewmodel.Knjiga = knjiga;
                viewmodel.Zanrovi = ZanrRepository.GetAll();
                return View(viewmodel);
            }
            knjiga.Zanr = ZanrRepository.GetOne(idZanra);
            KnjigaRepository.Create(knjiga);
            return RedirectToAction("Index");
        }

        public IActionResult IzlistajObrisane()
        {
            List<KnjigaModel> model = KnjigaRepository.GetAllDeleted();
            return View(model);
        }
        public IActionResult Ukloni(int id)
        {
            KnjigaRepository.Archive(id);
            return RedirectToAction("Index");
        }
        public IActionResult Obrisi(int id)
        {
            KnjigaRepository.Delete(id);
            return RedirectToAction("IzlistajObrisane");
        }
        public IActionResult Sortiraj(int nacin, string tipListe)
        {
            int deleted = (tipListe == "sve") ? 0 : 1;
            string field = (nacin == 1 || nacin == 2) ? "naziv_knjige" : "cena";
            string sorting = (nacin == 2 || nacin == 4) ? "desc" : "asc";

            List<KnjigaModel> sortiraneKnjige = KnjigaRepository.Sort(deleted, field, sorting);

            if (tipListe == "sve")
            {
                return View("Index", sortiraneKnjige);
            }
            else
            {
                return View("IzlistajObrisane", sortiraneKnjige);
            }
        }
    }
}
