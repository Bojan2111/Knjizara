using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Knjizara.Models;
using Knjizara.Repository.Interfaces;
using Knjizara.Repository;
using Knjizara.ViewModels;

namespace Knjizara.Controllers
{
    public class ZanrController : Controller
    {
        public IZanrRepository ZanrRepository;
        public ZanrController(IConfiguration Configuration)
        {
            ZanrRepository = new ZanrRepository(Configuration);
        }
        public IActionResult Index()
        {
            return View(ZanrRepository.GetAll());
        }

        public IActionResult Izmeni(int id)
        {
            ZanrModel zanr = ZanrRepository.GetOne(id);
            return View(zanr);
        }

        [HttpPost]
        public IActionResult Izmeni(ZanrModel zanr)
        {
            ZanrRepository.Update(zanr);
            return RedirectToAction("Index");
        }

        public IActionResult Dodaj()
        {
            return View(new ZanrModel());
        }

        [HttpPost]
        public IActionResult Dodaj(ZanrModel zanr)
        {
            if (!ModelState.IsValid)
            {
                ZanrModel z = zanr;
                return View(z);
            }
            ZanrRepository.Create(zanr);
            return RedirectToAction("Index");
        }
    }
}
