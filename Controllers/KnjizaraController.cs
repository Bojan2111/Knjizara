using Microsoft.AspNetCore.Mvc;
using Knjizara.Models;
using System.Runtime.CompilerServices;
using System;
using Knjizara.ViewModels;
using System.Reflection;

namespace Knjizara.Controllers
{
    public class KnjizaraController : Controller
    {
        public static KnjizaraModel knjizara;

        static KnjizaraController()
        {
            knjizara = new KnjizaraModel(1, "Eci Peci Pec");
            knjizara.Zanrovi = new List<ZanrModel>() { 
                new ZanrModel(1, "Sci-Fi"),
                new ZanrModel(2, "Komedija"),
                new ZanrModel(3, "Horor")
            };
            knjizara.BrojacZanrova = 4;
        }

        public IActionResult Index()
        {
            KnjigaZanrViewModel kzvm = new KnjigaZanrViewModel
            {
                Knjiga = new KnjigaModel(),
                Zanrovi = knjizara.Zanrovi
            };
            return View(kzvm);
        }

        [HttpPost]
        public IActionResult Dodaj(KnjigaModel knjiga, int idZanra)
        {
            foreach (KnjigaModel k in knjizara.Knjige)
            {
                if (k.Naziv == knjiga.Naziv && !k.Izbrisana)
                {
                    TempData["Poruka"] = $"Knjiga '{knjiga.Naziv}' vec postoji";
                    return RedirectToAction("Index");
                }
            }
            knjiga.Zanr = knjizara.Zanrovi.FirstOrDefault(x => x.Id.Equals(idZanra));

            knjiga.Id = knjizara.BrojacKnjiga;
            knjizara.BrojacKnjiga += 1;
            knjizara.Knjige.Add(knjiga);
            return RedirectToAction("IzlistajSve");
        }

        public IActionResult IzlistajSve()
        {
            return View(knjizara.Knjige);
        }
        public IActionResult IzlistajObrisane()
        {
            return View(knjizara.Knjige);
        }

        public IActionResult Obrisi(int id)
        {
            foreach (KnjigaModel k in knjizara.Knjige)
            {
                if (k.Id == id)
                {
                    k.Izbrisana = true;
                    break;
                }
            }
            return RedirectToAction("IzlistajSve");
        }

        public IActionResult Sortiraj(int nacin, string tipListe)
        {
            List<KnjigaModel> sortiraneKnjige = new List<KnjigaModel>();

            if (tipListe == "sve")
            {
                sortiraneKnjige = knjizara.Knjige.FindAll(k => k.Izbrisana == false);
            }
            else
            {
                sortiraneKnjige = knjizara.Knjige.FindAll(k => k.Izbrisana == true);
            }

            switch (nacin)
            {
                case 1:
                    sortiraneKnjige = sortiraneKnjige.OrderBy(k => k.Naziv).ToList();
                    break;
                case 2:
                    sortiraneKnjige = sortiraneKnjige.OrderByDescending(k => k.Naziv).ToList();
                    break;
                case 3:
                    sortiraneKnjige = sortiraneKnjige.OrderBy(k => k.Cena).ToList();
                    break;
                case 4:
                    sortiraneKnjige = sortiraneKnjige.OrderByDescending(k => k.Cena).ToList();
                    break;
            }

            if (tipListe == "sve")
            {
                return View("IzlistajSve", sortiraneKnjige);
            }
            else
            {
                return View("IzlistajObrisane", sortiraneKnjige);
            }
        }
        public IActionResult Izmeni(int id)
        {
            KnjigaZanrViewModel kzvm = new KnjigaZanrViewModel
            {
                Knjiga = knjizara.Knjige.FirstOrDefault(k => k.Id == id),
                Zanrovi = knjizara.Zanrovi
            };
            return View(kzvm);
        }

        [HttpPost]
        public IActionResult Izmeni(KnjigaModel knjiga, int idZanra)
        {
            int idx = knjizara.Knjige.FindIndex(x => x.Id == knjiga.Id);
            knjiga.Zanr = knjizara.Zanrovi.FirstOrDefault(y => y.Id == idZanra);
            knjizara.Knjige[idx] = knjiga;
            return RedirectToAction("IzlistajSve");
        }
    }
}
