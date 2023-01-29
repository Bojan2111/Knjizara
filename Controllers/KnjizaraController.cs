using Microsoft.AspNetCore.Mvc;
using Knjizara.Models;
using System.Runtime.CompilerServices;
using System;

namespace Knjizara.Controllers
{
    public class KnjizaraController : Controller
    {
        public static List<KnjigaModel> knjige = new List<KnjigaModel>();
        //public static List<KnjigaModel> obrisaneKnjige = new List<KnjigaModel>();
        //public static int brojac = 0;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Dodaj(string naziv, double cena, string zanr)
        {
            foreach (KnjigaModel k in knjige)
            {
                if (naziv == k.Naziv)
                {
                    ViewBag.naslov = naziv;
                    return RedirectToAction("KnjigaVecPostoji");
                }
            }
            KnjigaModel knjiga = new KnjigaModel();
            knjiga.Naziv = naziv;
            knjiga.Cena = cena;
            knjiga.Zanr = zanr;
            knjiga.Id = knjige.Count + 1;
            knjige.Add(knjiga);
            return RedirectToAction("IzlistajSve");
        }

        public IActionResult IzlistajSve()
        {
            return View(knjige);
        }
        public IActionResult IzlistajObrisane()
        {
            return View(knjige);
        }

        public IActionResult KnjigaVecPostoji()
        {
            return View();
        }

        public IActionResult Obrisi(int id)
        {
            foreach (KnjigaModel k in knjige)
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
                sortiraneKnjige = knjige.FindAll(k => k.Izbrisana == false);
            }
            else
            {
                sortiraneKnjige = knjige.FindAll(k => k.Izbrisana == true);
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
    }
}
