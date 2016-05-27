using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradeBook.BLL;

namespace GradeBook.App.Controllers
{
    public class SkolaController : Controller
    {
        // GET: Skola
        public ActionResult Index()
        {
            return View(SkolaInfoList.Get());
        }

        public ActionResult Details(int id)
        {
            return View(Skola.Get(id));
        }

        public ActionResult Edit(int id)
        {
            Skola skola = Skola.Get(id);
            return View(skola);
        }

        [HttpPost]
        public ActionResult Edit(int id, string NazivSkole, string Adresa, string Email, string MbrSkole, string OibSkole, string Telefon)
        {
            Skola skola = null;

            skola = Skola.Get(id);
            skola.NazivSkole = NazivSkole;
            skola.Adresa = Adresa;
            skola.Email = Email;
            skola.MbrSkole = MbrSkole;
            skola.OibSkole = OibSkole;
            skola.Telefon = Telefon;
            skola = skola.Save();

            return RedirectToAction("Details", new { id = skola.IdSkole });
        }

        public ActionResult Create()
        {
            return View(Skola.New());
        }

        [HttpPost]
        public ActionResult Create(string NazivSkole, string Adresa, string Email, string MbrSkole, string OibSkole, string Telefon)
        {
            Skola skola = Skola.New();
            skola.NazivSkole = NazivSkole;
            skola.Adresa = Adresa;
            skola.Email = Email;
            skola.MbrSkole = MbrSkole;
            skola.OibSkole = OibSkole;
            skola.Telefon = Telefon;
            skola = skola.Save();

            return RedirectToAction("Details", new { id = skola.IdSkole });
        }

        [HttpPost]
        public string PromijeniTipZaposlenika(int IdZaposlenika, int IdTipa)
        {
            try
            {
                Zaposlenik zaposlenik = Zaposlenik.Get(IdZaposlenika);
                zaposlenik.IdTipa = IdTipa;
                zaposlenik.Save();
                return "OK";
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
        }
    }
}