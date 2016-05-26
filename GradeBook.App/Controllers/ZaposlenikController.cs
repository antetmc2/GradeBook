using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradeBook.BLL;

namespace GradeBook.App.Controllers
{
    public class ZaposlenikController : Controller
    {
        // GET: Zaposlenik
        public ActionResult Index()
        {
            return View(ZaposlenikInfoList.Get());
        }

        public ActionResult Details(int id)
        {
            return View(Zaposlenik.Get(id));
        }

        public ActionResult Create()
        {
            return View(Zaposlenik.New());
        }

        [HttpPost]
        public ActionResult Create(string ImeZaposlenika, string PrezimeZaposlenika, DateTime DatumPocetkaRada, string Oib, string Email, int IdSkole, int IdTipa)
        {
            Zaposlenik zaposlenik = Zaposlenik.New();
            zaposlenik.ImeZaposlenika = ImeZaposlenika;
            zaposlenik.PrezimeZaposlenika = PrezimeZaposlenika;
            zaposlenik.DatumPocetkaRada = DatumPocetkaRada;
            zaposlenik.Oib = Oib;
            zaposlenik.Email = Email;
            zaposlenik.IdSkole = IdSkole;
            zaposlenik.IdTipa = IdTipa;
            zaposlenik = zaposlenik.Save();
            return RedirectToAction("Details", new { id = zaposlenik.IdZaposlenika });
        }

        public ActionResult Edit(int id)
        {
            Zaposlenik zaposlenik = Zaposlenik.Get(id);
            return View(zaposlenik);
        }

        [HttpPost]
        public ActionResult Edit(int id, string ImeZaposlenika, string PrezimeZaposlenika, DateTime DatumPocetkaRada, string Oib, string Email, int IdSkole, int IdTipa)
        {
            Zaposlenik zaposlenik = null;

            zaposlenik = Zaposlenik.Get(id);
            zaposlenik.ImeZaposlenika = ImeZaposlenika;
            zaposlenik.PrezimeZaposlenika = PrezimeZaposlenika;
            zaposlenik.DatumPocetkaRada = DatumPocetkaRada;
            zaposlenik.Oib = Oib;
            zaposlenik.Email = Email;
            zaposlenik.IdSkole = IdSkole;
            zaposlenik.IdTipa = IdTipa;
            zaposlenik = zaposlenik.Save();
            return RedirectToAction("Details", new { id = zaposlenik.IdZaposlenika });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Zaposlenik.Delete(id);
            return RedirectToAction("Index");
        }
    }
}