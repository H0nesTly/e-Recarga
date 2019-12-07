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
    [Authorize]
    public class ReservaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reserva
        public ActionResult Index(string sortOrder)
        {
            ViewBag.DataSortParam = sortOrder == "Data" ? "date_desc" : "Data";
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");

            var reservas = db.Reservas.Include(r => r.CarregamentoReserva).Include(r => r.TomadaPostoReserva).Include(r => r.UtilizadorReserva).Where(r => r.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin || r.TomadaPostoReserva.PostoTomadaPosto.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID);

            switch (sortOrder)
            {
                case "date_desc":
                    reservas = reservas.OrderByDescending(s => s.DataReserva);
                    break;
                case "Data":
                    reservas = reservas.OrderBy(s => s.DataReserva);
                    break;
            }

            return View(reservas.ToList());
        }

        // GET: Reserva/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // GET: Reserva/Create
        public ActionResult Create()
        {            
            ViewBag.TomadaPostoID = new SelectList(db.TomadaPostoes, "ID", "ID");
            
            return View();
        }

        // POST: Reserva/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DataReserva,DataPrevInicioCarregamento,DataPrevFimCarregamento,UtilizadorID,CarregamentoID,TomadaPostoID")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                db.Reservas.Add(reserva);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarregamentoID = new SelectList(db.Carregamentoes, "ID", "UtilizadorID", reserva.CarregamentoID);
            ViewBag.TomadaPostoID = new SelectList(db.TomadaPostoes, "ID", "ID", reserva.TomadaPostoID);
            ViewBag.UtilizadorID = new SelectList(db.ApplicationUsers, "Id", "Nome", reserva.UtilizadorID);
            return View(reserva);
        }

        // GET: Reserva/Edit/5
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarregamentoID = new SelectList(db.Carregamentoes, "ID", "UtilizadorID", reserva.CarregamentoID);
            ViewBag.TomadaPostoID = new SelectList(db.TomadaPostoes, "ID", "ID", reserva.TomadaPostoID);
            ViewBag.UtilizadorID = new SelectList(db.ApplicationUsers, "Id", "Nome", reserva.UtilizadorID);
            return View(reserva);
        }

        // POST: Reserva/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Edit([Bind(Include = "ID,DataReserva,DataPrevInicioCarregamento,DataPrevFimCarregamento,UtilizadorID,CarregamentoID,TomadaPostoID")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarregamentoID = new SelectList(db.Carregamentoes, "ID", "UtilizadorID", reserva.CarregamentoID);
            ViewBag.TomadaPostoID = new SelectList(db.TomadaPostoes, "ID", "ID", reserva.TomadaPostoID);
            ViewBag.UtilizadorID = new SelectList(db.ApplicationUsers, "Id", "Nome", reserva.UtilizadorID);
            return View(reserva);
        }

        // GET: Reserva/Delete/5
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reserva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserva reserva = db.Reservas.Find(id);
            db.Reservas.Remove(reserva);
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
