using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Knjizara.Models;

namespace Knjizara.Controllers
{
    public class ZanrController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dodaj(ZanrModel zanr)
        {
            foreach (ZanrModel z in KnjizaraController.knjizara.Zanrovi)
            {
                if (z.NazivZanra == zanr.NazivZanra)
                {
                    TempData["PorukaZanr"] = $"Zanr '{zanr.NazivZanra}' vec postoji";
                    return RedirectToAction("Index");
                }
            }
            zanr.Id = KnjizaraController.knjizara.BrojacZanrova;
            KnjizaraController.knjizara.BrojacZanrova += 1;
            KnjizaraController.knjizara.Zanrovi.Add(zanr);
            return RedirectToAction("IzlistajSve");
        }
        public IActionResult IzlistajSve()
        {
            return View(KnjizaraController.knjizara.Zanrovi);
        }
        public IActionResult Izmeni(int idZanra)
        {
            ZanrModel zanr = KnjizaraController.knjizara.Zanrovi.FirstOrDefault(x => x.Id == idZanra);
            return View(zanr);
        }

        [HttpPost]
        public IActionResult Izmeni(ZanrModel zanr)
        {
            int idx = KnjizaraController.knjizara.Zanrovi.FindIndex(x => x.Id == zanr.Id);
            KnjizaraController.knjizara.Zanrovi[idx] = zanr;
            return RedirectToAction("IzlistajSve");
        }
    }
}
