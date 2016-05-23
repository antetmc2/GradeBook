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
        public ActionResult Create(string NazivTipa)
        {
            TipZaposlenika tip = null;
            try
            {
                TipoviZaposlenika tipovi = TipoviZaposlenika.Get();
                tip = tipovi.AddNew();
                //tip.IdTipa = IdTipa;
                tip.NazivTipa = NazivTipa;
                tipovi.Save();
                return RedirectToAction("Index");
            }
            catch (Csla.Validation.ValidationException ex)
            {
                ViewBag.Pogreska = ex.Message;
                if (tip.BrokenRulesCollection.Count > 0)
                {
                    List<string> errors = new List<string>();
                    foreach (Csla.Validation.BrokenRule rule in tip.BrokenRulesCollection)
                    {
                        errors.Add(string.Format("{0}: {1}", rule.Property, rule.Description));
                        ModelState.AddModelError(rule.Property, rule.Description);
                    }
                    ViewBag.ErrorsList = errors;
                }
                return View(tip);
            }
            catch (Csla.DataPortalException ex)
            {
                ViewBag.Pogreska = ex.BusinessException.Message;
                return View(tip);
            }
            catch (Exception ex)
            {
                ViewBag.Pogreska = ex.Message;
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            TipZaposlenika tip = TipoviZaposlenika.Get().GetTipZaposlenika(id);
            return View(tip);
        }

        [HttpPost]
        public ActionResult Edit(int id, string NazivTipa)
        {
            TipZaposlenika tip = null;
            try
            {
                TipoviZaposlenika tipovi = TipoviZaposlenika.Get();
                tip = tipovi.GetTipZaposlenika(id);
                tip.NazivTipa = NazivTipa;
                tipovi.Save();
                return RedirectToAction("Index");
            }
            catch (Csla.Validation.ValidationException ex)
            {
                ViewBag.Pogreska = ex.Message;
                if (tip.BrokenRulesCollection.Count > 0)
                {
                    List<string> errors = new List<string>();
                    foreach (Csla.Validation.BrokenRule rule in tip.BrokenRulesCollection)
                    {
                        errors.Add(string.Format("{0}: {1}", rule.Property, rule.Description));
                        ModelState.AddModelError(rule.Property, rule.Description);
                    }
                    ViewBag.ErrorsList = errors;
                }
                return View(tip);
            }
            catch (Csla.DataPortalException ex)
            {
                ViewBag.Pogreska = ex.BusinessException.Message;
                return View(tip);
            }
            catch (Exception ex)
            {
                ViewBag.Pogreska = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            TipoviZaposlenika tipovi = TipoviZaposlenika.Get();
            tipovi.RemoveTipZaposlenika(id);
            tipovi.Save();
            return RedirectToAction("Index");
        }
    }
}