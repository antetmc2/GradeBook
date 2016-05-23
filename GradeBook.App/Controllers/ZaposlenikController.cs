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
        public ActionResult Create(string ImeZaposlenika, string PrezimeZaposlenika, DateTime DatumPocetkaRada, string Oib, string Email)
        {
            return View();
        }
    }
}