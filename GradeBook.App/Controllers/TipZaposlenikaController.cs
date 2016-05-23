using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradeBook.BLL.Admin;

namespace GradeBook.App.Controllers
{
    public class TipZaposlenikaController : Controller
    {
        // GET: TipZaposlenika
        public ActionResult Index()
        {
            return View(TipoviZaposlenika.Get());
        }

        public ActionResult Create()
        {
            TipZaposlenika tip = TipoviZaposlenika.Get().AddNew();
            return View(tip);
        }

        [HttpPost]
        public ActionResult Create(int IdTipa, string NazivTipa)
        {
            TipZaposlenika tip = null;

            TipoviZaposlenika tipovi = TipoviZaposlenika.Get();
            tip = tipovi.AddNew();
            tip.IdTipa = IdTipa;
            tip.NazivTipa = NazivTipa;
            tipovi.Save();
            return RedirectToAction("Index");
        }
    }
}