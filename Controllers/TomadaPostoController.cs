using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using e_Recarga.Models;
using e_Recarga.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace e_Recarga.Controllers
{
    [Authorize]
    public class TomadaPostoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TomadaPosto
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Index()
        {
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");


            var tomadaPostos = db.TomadaPostoes.Include(t => t.PostoTomadaPosto).Include(t => t.TomadaTomadaPosto).Include(t => t.PotenciaTomadaPosto).Where(tp => tp.PostoTomadaPosto.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin);
            return View(tomadaPostos.ToList());
        }



        // GET: TomadaPosto/Details/5
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TomadaPosto tomadaPosto = db.TomadaPostoes.Find(id);
            if (tomadaPosto == null)
            {
                return HttpNotFound();
            }
            return View(tomadaPosto);
        }

        // POST: TomadaPosto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Create(TomadaPostoViewModel tomadaPostoView)
        {
            if (ModelState.IsValid)
            {
                TomadaPosto tomadaPosto = new TomadaPosto() { 
                    PostoID = tomadaPostoView.codeDoPosto,
                    PotenciaID = tomadaPostoView.PotenciaID.Value,
                    TomadaID = tomadaPostoView.TomadaID.Value,
                    PrecoMinuto = tomadaPostoView.PrecoMinuto,
                };

                db.TomadaPostoes.Add(tomadaPosto);
                db.SaveChanges();
                return RedirectToAction("Details", "Posto", new { id = tomadaPostoView.codeDoPosto });
            }

            return RedirectToAction("Details", "Posto", new { id = tomadaPostoView.codeDoPosto} );
        }

        // GET: TomadaPosto/Edit/5
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Edit(int? id)
        {
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");
            bool isRP = User.IsInRole("RedeProprietaria");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TomadaPosto tomadaPosto = db.TomadaPostoes.Find(id);
            if (tomadaPosto == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostoID = new SelectList(db.Postoes.Where(p => p.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin), "ID", "ID", tomadaPosto.PostoID);
            ViewBag.TomadaID = new SelectList(db.Tomadas, "ID", "TipoTomada", tomadaPosto.TomadaID);
            ViewBag.PotenciaID = new SelectList(db.Potencias, "ID", "PotenciaNominalKw", tomadaPosto.PotenciaID);

            return View(tomadaPosto);
        }

        // POST: TomadaPosto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Edit([Bind(Include = "ID,TomadaID,PostoID,PrecoMinuto,PotenciaID")] TomadaPosto tomadaPosto)
        {
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");
            bool isRP = User.IsInRole("RedeProprietaria");

            if (ModelState.IsValid)
            {
                db.Entry(tomadaPosto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostoID = new SelectList(db.Postoes.Where(p => p.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin), "ID", "ID", tomadaPosto.PostoID);
            ViewBag.TomadaID = new SelectList(db.Tomadas, "ID", "TipoTomada", tomadaPosto.TomadaID);
            ViewBag.PotenciaID = new SelectList(db.Potencias, "ID", "PotenciaNominalKw", tomadaPosto.PotenciaID);

            return View(tomadaPosto);
        }

        // GET: TomadaPosto/Delete/5
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TomadaPosto tomadaPosto = db.TomadaPostoes.Find(id);
            if (tomadaPosto == null)
            {
                return HttpNotFound();
            }
            return View(tomadaPosto);
        }

        // POST: TomadaPosto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult DeleteConfirmed(int id)
        {

            TomadaPosto tomadaPosto = db.TomadaPostoes.Find(id);
            db.TomadaPostoes.Remove(tomadaPosto);
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
