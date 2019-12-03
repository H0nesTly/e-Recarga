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
    public class EstacaoCarregamentoController : Controller
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

        // GET: EstacaoCarregamento
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");

            var estacaoCarregamentoes = db.EstacaoCarregamentoes.Include(e => e.ConcelhoLocalizacao).Include(e => e.UtilizadorEstacao).Where(ec => ec.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin);
            return View(estacaoCarregamentoes.ToList());
            
        }

        // GET: EstacaoCarregamento/Details/5
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
            EstacaoCarregamento estacaoCarregamento = db.EstacaoCarregamentoes.Find(id);
            if (estacaoCarregamento == null)
            {
                return HttpNotFound();
            }
            
            return View(estacaoCarregamento);
        }

        // GET: EstacaoCarregamento/Create
        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            ViewBag.ConcelhoID = new SelectList(db.Concelhoes, "ID", "Nome");
            
            return View();
        }

        // POST: EstacaoCarregamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Designacao,Latitude,Longitude,CodigoPostal,Localidade,ConcelhoID")] EstacaoCarregamento estacaoCarregamento)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                estacaoCarregamento.UtilizadorID = User.Identity.GetUserId();
                db.EstacaoCarregamentoes.Add(estacaoCarregamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConcelhoID = new SelectList(db.Concelhoes, "ID", "Nome", estacaoCarregamento.ConcelhoID);
            
            return View(estacaoCarregamento);
        }

        // GET: EstacaoCarregamento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstacaoCarregamento estacaoCarregamento = db.EstacaoCarregamentoes.Find(id);
            if (estacaoCarregamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConcelhoID = new SelectList(db.Concelhoes, "ID", "Nome", estacaoCarregamento.ConcelhoID);
           
            return View(estacaoCarregamento);
        }

        // POST: EstacaoCarregamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Designacao,Latitude,Longitude,CodigoPostal,Localidade,ConcelhoID,UtilizadorID")] EstacaoCarregamento estacaoCarregamento)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                estacaoCarregamento.UtilizadorID = User.Identity.GetUserId();
                db.Entry(estacaoCarregamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConcelhoID = new SelectList(db.Concelhoes, "ID", "Nome", estacaoCarregamento.ConcelhoID);
            return View(estacaoCarregamento);
        }

        // GET: EstacaoCarregamento/Delete/5
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
            EstacaoCarregamento estacaoCarregamento = db.EstacaoCarregamentoes.Find(id);
            if (estacaoCarregamento == null)
            {
                return HttpNotFound();
            }
            return View(estacaoCarregamento);
        }

        // POST: EstacaoCarregamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!UserCanAcessActionController())
                return RedirectToAction("Index", "Home");

            EstacaoCarregamento estacaoCarregamento = db.EstacaoCarregamentoes.Find(id);
            db.EstacaoCarregamentoes.Remove(estacaoCarregamento);
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
