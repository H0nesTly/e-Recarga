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
    public class PostoController : Controller
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

        // GET: Posto
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");

          
            var postos = db.Postoes.Include(p => p.EstacaoCarregamentoPosto).Include(p => p.PotenciaPosto).Where(p => p.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin);
            return View(postos.ToList());
        }

        // GET: Posto/Details/5
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
            Posto posto = db.Postoes.Find(id);
            if (posto == null)
            {
                return HttpNotFound();
            }
            return View(posto);
        }

        // GET: Posto/Create
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

            ViewBag.EstacaoCarregamentoID = new SelectList(db.EstacaoCarregamentoes.Where(ec => ec.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin), "ID", "Designacao");
            ViewBag.PotenciaID = new SelectList(db.Potencias, "ID", "PotenciaNominalKw");
            return View();
        }

        // POST: Posto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CorrenteCarregamento,NumeroTomadas,PotenciaID,EstacaoCarregamentoID")] Posto posto)
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
                db.Postoes.Add(posto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstacaoCarregamentoID = new SelectList(db.EstacaoCarregamentoes.Where(ec => ec.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin), "ID", "Designacao", posto.EstacaoCarregamentoID);
            ViewBag.PotenciaID = new SelectList(db.Potencias, "ID", "PotenciaNominalKw", posto.PotenciaID);
            return View(posto);
        }

        // GET: Posto/Edit/5
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
            Posto posto = db.Postoes.Find(id);
            if (posto == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstacaoCarregamentoID = new SelectList(db.EstacaoCarregamentoes.Where(ec => ec.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin), "ID", "Designacao", posto.EstacaoCarregamentoID);
            ViewBag.PotenciaID = new SelectList(db.Potencias, "ID", "PotenciaNominalKw", posto.PotenciaID);
            return View(posto);
        }

        // POST: Posto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CorrenteCarregamento,NumeroTomadas,PotenciaID,EstacaoCarregamentoID")] Posto posto)
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
                db.Entry(posto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstacaoCarregamentoID = new SelectList(db.EstacaoCarregamentoes.Where(ec => ec.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin), "ID", "Designacao", posto.EstacaoCarregamentoID);
            ViewBag.PotenciaID = new SelectList(db.Potencias, "ID", "PotenciaNominalKw", posto.PotenciaID);
            return View(posto);
        }

        // GET: Posto/Delete/5
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
            Posto posto = db.Postoes.Find(id);
            if (posto == null)
            {
                return HttpNotFound();
            }
            return View(posto);
        }

        // POST: Posto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            Posto posto = db.Postoes.Find(id);
            db.Postoes.Remove(posto);
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
