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
    public class ReservaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reserva
        public ActionResult Index()
        {
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");

//            IndexReservaViewModel indexReservaViewModel = new IndexReservaViewModel();
            List<IndexReservaViewModel> indexReservas = new List<IndexReservaViewModel>();

           // reservas 
           
            //indexReservaViewModel.Reservas = db.Reservas.Include(r => r.CarregamentoReserva).Include(r => r.TomadaPostoReserva).Include(r => r.UtilizadorReserva).Where(r => r.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin || r.TomadaPostoReserva.PostoTomadaPosto.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID).ToList();
            
            var reservas = db.Reservas.Include(r => r.CarregamentoReserva).Include(r => r.TomadaPostoReserva).Include(r => r.UtilizadorReserva).Where(r => r.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin || r.TomadaPostoReserva.PostoTomadaPosto.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID);

            foreach(Reserva r in reservas.ToList())
            {
                IndexReservaViewModel indexReservaView = new IndexReservaViewModel();
                indexReservaView.ReservaID = r.ID;
                indexReservaView.PostoID = r.TomadaPostoReserva.PostoID;
                indexReservaView.TomadaPostoID = r.TomadaPostoID;
                indexReservaView.DataInicioReserva = r.DataPrevInicioCarregamento;
                indexReservaView.DataFimReserva = r.DataPrevFimCarregamento;
                indexReservaView.EstacaoCarregamento = r.TomadaPostoReserva.PostoTomadaPosto.EstacaoCarregamentoPosto;


                TimeSpan data = r.DataPrevFimCarregamento.Subtract(r.DataPrevInicioCarregamento);
                double minutos = data.TotalMinutes;

                indexReservaView.Total = minutos * r.TomadaPostoReserva.PrecoMinuto;

                indexReservas.Add(indexReservaView);
            }
            
            return View(indexReservas);
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
        
        [HttpPost]
        public ActionResult Search(ReservaViewModel reservaViewModel)
        {
            if (ModelState.IsValid)
            {   

                List<TomadaPosto> tomadaPostos = db.TomadaPostoes.ToList();


                DateTime dataI = reservaViewModel.procurarPostosViewModel.DataInicioCarregamento;
                DateTime dataF = reservaViewModel.procurarPostosViewModel.DataFimCarregamento;

                tomadaPostos = tomadaPostos.Where(tp => tp.Reservas.Where(r => r.DataPrevInicioCarregamento >= dataI && r.DataPrevInicioCarregamento <= dataF).Count() == 0).ToList();


                tomadaPostos = tomadaPostos.Where(tp => tp.Reservas.Where(r => r.DataPrevFimCarregamento >= dataI && r.DataPrevFimCarregamento <= dataF).Count() == 0).ToList();


                if (reservaViewModel.procurarPostosViewModel.DistritoID != null)
                    tomadaPostos = tomadaPostos.Where(tp => tp.PostoTomadaPosto.EstacaoCarregamentoPosto.ConcelhoEstacaoCarregamento.DistritoID == reservaViewModel.procurarPostosViewModel.DistritoID).ToList();
                
                if (reservaViewModel.procurarPostosViewModel.ConcelhoID != null)
                    tomadaPostos = tomadaPostos.Where(tp => tp.PostoTomadaPosto.EstacaoCarregamentoPosto.ConcelhoID == reservaViewModel.procurarPostosViewModel.ConcelhoID).ToList();
                
                if (reservaViewModel.procurarPostosViewModel.EstacaoCarregamentoID != null)
                    tomadaPostos = tomadaPostos.Where(tp => tp.PostoTomadaPosto.EstacaoCarregamentoPosto.ID == reservaViewModel.procurarPostosViewModel.EstacaoCarregamentoID).ToList();
                
                if (reservaViewModel.procurarPostosViewModel.PotenciaID != null)
                    tomadaPostos = tomadaPostos.Where(tp => tp.PotenciaID == reservaViewModel.procurarPostosViewModel.PotenciaID).ToList();
                
                if (reservaViewModel.procurarPostosViewModel.TomadaID != null)
                    tomadaPostos = tomadaPostos.Where(tp => tp.TomadaID == reservaViewModel.procurarPostosViewModel.TomadaID).ToList();



                reservaViewModel.procurarPostosViewModel.TomadaPostos = tomadaPostos;
                
                reservaViewModel.procurarPostosViewModel.Distritos = db.Distritos.ToList();
                reservaViewModel.procurarPostosViewModel.Concelhos = db.Concelhoes.ToList();
                reservaViewModel.procurarPostosViewModel.EstacaoCarregamentos = db.EstacaoCarregamentoes.ToList();
                reservaViewModel.procurarPostosViewModel.Potencias = db.Potencias.ToList();
                reservaViewModel.procurarPostosViewModel.Tomadas = db.Tomadas.ToList();

                reservaViewModel.novaReservaViewModel = new NovaReservaViewModel();
                reservaViewModel.novaReservaViewModel.DataPrevInicioCarregamento = reservaViewModel.procurarPostosViewModel.DataInicioCarregamento;

                reservaViewModel.novaReservaViewModel.DataPrevFimCarregamento = reservaViewModel.procurarPostosViewModel.DataFimCarregamento;



                return View("Create", reservaViewModel);
            }
            return View("Create", reservaViewModel);
        }

        // GET: Reserva/Create
        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            
            ReservaViewModel reservaViewModel = new ReservaViewModel();
            reservaViewModel.procurarPostosViewModel = new ProcurarPostosViewModel();
            reservaViewModel.novaReservaViewModel = new NovaReservaViewModel();

            reservaViewModel.procurarPostosViewModel.Distritos = db.Distritos.ToList();
            reservaViewModel.procurarPostosViewModel.Concelhos = db.Concelhoes.ToList();
            reservaViewModel.procurarPostosViewModel.EstacaoCarregamentos = db.EstacaoCarregamentoes.ToList();
            reservaViewModel.procurarPostosViewModel.Potencias = db.Potencias.ToList();
            reservaViewModel.procurarPostosViewModel.Tomadas = db.Tomadas.ToList();

            reservaViewModel.procurarPostosViewModel.DataInicioCarregamento = DateTime.Now;
            reservaViewModel.procurarPostosViewModel.DataFimCarregamento = reservaViewModel.procurarPostosViewModel.DataInicioCarregamento.AddHours(8);

            reservaViewModel.novaReservaViewModel.DataPrevInicioCarregamento = reservaViewModel.procurarPostosViewModel.DataInicioCarregamento;
            reservaViewModel.novaReservaViewModel.DataPrevFimCarregamento = reservaViewModel.procurarPostosViewModel.DataFimCarregamento;

            List<TomadaPosto> tomadaPostos = db.TomadaPostoes.ToList();
            
            DateTime dataI = reservaViewModel.procurarPostosViewModel.DataInicioCarregamento;
            DateTime dataF = reservaViewModel.procurarPostosViewModel.DataFimCarregamento;

            tomadaPostos = tomadaPostos.Where(tp => tp.Reservas.Where(r => r.DataPrevInicioCarregamento >= dataI && r.DataPrevInicioCarregamento <= dataF).Count() == 0).ToList();
            
           
            tomadaPostos = tomadaPostos.Where(tp => tp.Reservas.Where(r => r.DataPrevFimCarregamento >= dataI && r.DataPrevFimCarregamento <= dataF).Count() == 0).ToList();

            reservaViewModel.procurarPostosViewModel.TomadaPostos = tomadaPostos;

            
            return View(reservaViewModel);
        }

        // POST: Reserva/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservaViewModel reservaViewModel)
        {
            Reserva reserva = null;
            if (ModelState.IsValid)
            {
                reserva = new Reserva();
                reserva.DataReserva = DateTime.Now;
                reserva.DataPrevInicioCarregamento = reservaViewModel.novaReservaViewModel.DataPrevInicioCarregamento;
                reserva.DataPrevFimCarregamento = reservaViewModel.novaReservaViewModel.DataPrevFimCarregamento;
                reserva.UtilizadorID = User.Identity.GetUserId();
                reserva.TomadaPostoID = reservaViewModel.novaReservaViewModel.TomadaPostoID;
                reserva.CarregamentoID = null;


                
                db.Reservas.Add(reserva);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           //ViewBag.DistritoID = new SelectList(db.Distritos, "ID", "Nome", reserva.);
            //ViewBag.CarregamentoID = new SelectList(db.Carregamentoes, "ID", "UtilizadorID", reserva.CarregamentoID);
            //ViewBag.TomadaPostoID = new SelectList(db.TomadaPostoes, "ID", "ID", reserva.TomadaPostoID);
            //ViewBag.UtilizadorID = new SelectList(db.ApplicationUsers, "Id", "Nome", reserva.UtilizadorID);
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
