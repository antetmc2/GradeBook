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

            try
            {
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
            catch (Csla.Validation.ValidationException ex)
            {
                ViewBag.Pogreska = ex.Message;
                if (skola.BrokenRulesCollection.Count > 0)
                {
                    List<string> errors = new List<string>();
                    foreach (Csla.Validation.BrokenRule rule in skola.BrokenRulesCollection)
                    {
                        errors.Add(string.Format("{0}: {1}", rule.Property, rule.Description));
                        ModelState.AddModelError(rule.Property, rule.Description);
                    }
                    ViewBag.ErrorsList = errors;
                }
                return View(skola);
            }
            catch (Csla.DataPortalException ex)
            {
                ViewBag.Pogreska = ex.BusinessException.Message;
                return View(skola);
            }
            catch (Exception ex)
            {
                ViewBag.Pogreska = ex.Message;
                return View(skola);
            }
        }

        public ActionResult Create()
        {
            return View(Skola.New());
        }

        [HttpPost]
        public ActionResult Create(string NazivSkole, string Adresa, string Email, string MbrSkole, string OibSkole, string Telefon)
        {
            Skola skola = Skola.New();

            try
            {
                skola.NazivSkole = NazivSkole;
                skola.Adresa = Adresa;
                skola.Email = Email;
                skola.MbrSkole = MbrSkole;
                skola.OibSkole = OibSkole;
                skola.Telefon = Telefon;
                skola = skola.Save();

                return RedirectToAction("Details", new { id = skola.IdSkole });
            }
            catch (Csla.Validation.ValidationException ex)
            {
                ViewBag.Pogreska = ex.Message;
                if (skola.BrokenRulesCollection.Count > 0)
                {
                    List<string> errors = new List<string>();
                    foreach (Csla.Validation.BrokenRule rule in skola.BrokenRulesCollection)
                    {
                        errors.Add(string.Format("{0}: {1}", rule.Property, rule.Description));
                        ModelState.AddModelError(rule.Property, rule.Description);
                    }
                    ViewBag.ErrorsList = errors;
                }
                return View(skola);
            }
            catch (Csla.DataPortalException ex)
            {
                ViewBag.Pogreska = ex.BusinessException.Message;
                return View(skola);
            }
            catch (Exception ex)
            {
                ViewBag.Pogreska = ex.Message;
                return View(skola);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Skola.Delete(id);
            }
            catch (Exception ex)
            {
                TempData["Pogreska"] = ex.Message;
            }
            return RedirectToAction("Index");
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