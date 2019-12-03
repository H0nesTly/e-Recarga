using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using e_Recarga.Models;
using Microsoft.AspNet.Identity;

namespace e_Recarga.Controllers
{
    public class TomadaPostoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public bool UserCanAcessActionController()
        {
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");
            bool isRP = User.IsInRole("RedeProprietaria");

            if (isAdmin || isSAdmin || isRP)
                return true;

            return false;
        }

        // GET: TomadaPosto
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");


            var tomadaPostos = db.TomadaPostos.Include(t => t.PostoTomadaPosto).Include(t => t.TomadaTomadaPosto).Where(tp => tp.PostoTomadaPosto.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin);
            return View(tomadaPostos.ToList());
        }

        // GET: TomadaPosto/Details/5
        public ActionResult Details(int? id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TomadaPosto tomadaPosto = db.TomadaPostos.Find(id);
            if (tomadaPosto == null)
            {
                return HttpNotFound();
            }
            return View(tomadaPosto);
        }

        // GET: TomadaPosto/Create
        public ActionResult Create()
        {
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");
            bool isRP = User.IsInRole("RedeProprietaria");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            ViewBag.PostoID = new SelectList(db.Postoes.Where(p => p.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin), "ID", "ID");
            ViewBag.TomadaID = new SelectList(db.Tomadas, "ID", "TipoTomada");
            return View();
        }

        // POST: TomadaPosto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TomadaID,PostoID,PrecoMinuto")] TomadaPosto tomadaPosto)
        {
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");
            bool isRP = User.IsInRole("RedeProprietaria");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                db.TomadaPostos.Add(tomadaPosto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostoID = new SelectList(db.Postoes.Where(p => p.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin), "ID", "ID", tomadaPosto.PostoID);
            ViewBag.TomadaID = new SelectList(db.Tomadas, "ID", "TipoTomada", tomadaPosto.TomadaID);
            return View(tomadaPosto);
        }

        // GET: TomadaPosto/Edit/5
        public ActionResult Edit(int? id)
        {
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");
            bool isRP = User.IsInRole("RedeProprietaria");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TomadaPosto tomadaPosto = db.TomadaPostos.Find(id);
            if (tomadaPosto == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostoID = new SelectList(db.Postoes.Where(p => p.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin), "ID", "ID", tomadaPosto.PostoID);
            ViewBag.TomadaID = new SelectList(db.Tomadas, "ID", "TipoTomada", tomadaPosto.TomadaID);
            return View(tomadaPosto);
        }

        // POST: TomadaPosto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TomadaID,PostoID,PrecoMinuto")] TomadaPosto tomadaPosto)
        {
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");
            bool isRP = User.IsInRole("RedeProprietaria");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                db.Entry(tomadaPosto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostoID = new SelectList(db.Postoes.Where(p => p.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin), "ID", "ID", tomadaPosto.PostoID);
            ViewBag.TomadaID = new SelectList(db.Tomadas, "ID", "TipoTomada", tomadaPosto.TomadaID);
            return View(tomadaPosto);
        }

        // GET: TomadaPosto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TomadaPosto tomadaPosto = db.TomadaPostos.Find(id);
            if (tomadaPosto == null)
            {
                return HttpNotFound();
            }
            return View(tomadaPosto);
        }

        // POST: TomadaPosto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            TomadaPosto tomadaPosto = db.TomadaPostos.Find(id);
            db.TomadaPostos.Remove(tomadaPosto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
