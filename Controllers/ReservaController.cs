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
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.DataSortParam = sortOrder == "Data" ? "date_desc" : "Data";
            
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
                indexReservaView.Cancelada = r.Cancelada;
                indexReservaView.Reserva = r;
                

                indexReservas.Add(indexReservaView);
            }
            
            return View(indexReservas);

        }

        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva r = db.Reservas.Find(id);
            
            if (r == null)
            {
                return HttpNotFound();
            }


            DetailReservaViewModel detailReservaView = new DetailReservaViewModel();
            detailReservaView.ReservaID = r.ID;
            detailReservaView.PostoID = r.TomadaPostoReserva.PostoID;
            detailReservaView.TomadaPostoID = r.TomadaPostoID;
            detailReservaView.DataInicioReserva = r.DataPrevInicioCarregamento;
            detailReservaView.DataFimReserva = r.DataPrevFimCarregamento;
            detailReservaView.EstacaoCarregamento = r.TomadaPostoReserva.PostoTomadaPosto.EstacaoCarregamentoPosto;
            detailReservaView.Cancelada = r.Cancelada;
            detailReservaView.UtilizadorID = r.UtilizadorID;
            detailReservaView.DataReserva = r.DataReserva;
            detailReservaView.UtilizadorReserva = r.UtilizadorReserva;
            detailReservaView.Reserva = r;
                       
            return View(detailReservaView);
        }
        
        [HttpPost]
        public ActionResult Search(ReservaViewModel reservaViewModel)
        {
            List<TomadaPosto> tomadaPostos;
            DateTime dataI, dataF;


            if (ModelState.IsValid)
            {   

                tomadaPostos = db.TomadaPostoes.ToList();


                dataI = reservaViewModel.procurarPostosViewModel.DataInicioCarregamento;
                dataF = reservaViewModel.procurarPostosViewModel.DataFimCarregamento;

                //tomadaPostos = tomadaPostos.Where(tp => tp.Reservas.Where(r => r.DataPrevInicioCarregamento >= dataI && r.DataPrevInicioCarregamento <= dataF && r.Cancelada == false).Count() == 0).ToList();


                //tomadaPostos = tomadaPostos.Where(tp => tp.Reservas.Where(r => r.DataPrevFimCarregamento >= dataI && r.DataPrevFimCarregamento <= dataF && r.Cancelada == false).Count() == 0).ToList();


                tomadaPostos = tomadaPostos.Where(tp => tp.Reservas.Where(r => ((r.DataPrevInicioCarregamento >= dataI && r.DataPrevInicioCarregamento <= dataF) || (r.DataPrevFimCarregamento >= dataI && r.DataPrevFimCarregamento <= dataF) || (r.DataPrevInicioCarregamento <= dataI && r.DataPrevFimCarregamento >= dataF)) && (r.Cancelada == false)).Count() == 0).ToList();


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
                reservaViewModel.procurarPostosViewModel.Concelhos = db.Concelhoes.Where(c => c.DistritoID == reservaViewModel.procurarPostosViewModel.DistritoID).ToList();
                reservaViewModel.procurarPostosViewModel.Potencias = db.Potencias.ToList();
                reservaViewModel.procurarPostosViewModel.Tomadas = db.Tomadas.ToList();

                if (reservaViewModel.procurarPostosViewModel.DistritoID != null && reservaViewModel.procurarPostosViewModel.ConcelhoID == null)
                    reservaViewModel.procurarPostosViewModel.EstacaoCarregamentos = db.EstacaoCarregamentoes.Where(e => e.ConcelhoEstacaoCarregamento.DistritoID == reservaViewModel.procurarPostosViewModel.DistritoID).ToList();
                else if (reservaViewModel.procurarPostosViewModel.ConcelhoID != null)
                    reservaViewModel.procurarPostosViewModel.EstacaoCarregamentos = db.EstacaoCarregamentoes.Where(e => e.ConcelhoID == reservaViewModel.procurarPostosViewModel.ConcelhoID).ToList();
                else
                    reservaViewModel.procurarPostosViewModel.EstacaoCarregamentos = db.EstacaoCarregamentoes.ToList();

                reservaViewModel.novaReservaViewModel = new NovaReservaViewModel();
                reservaViewModel.novaReservaViewModel.DataPrevInicioCarregamento = reservaViewModel.procurarPostosViewModel.DataInicioCarregamento;

                reservaViewModel.novaReservaViewModel.DataPrevFimCarregamento = reservaViewModel.procurarPostosViewModel.DataFimCarregamento;

                
                return View("Create", reservaViewModel);
            }

            reservaViewModel.procurarPostosViewModel.Distritos = db.Distritos.ToList();
            reservaViewModel.procurarPostosViewModel.Concelhos = db.Concelhoes.Where(c => c.DistritoID == reservaViewModel.procurarPostosViewModel.DistritoID).ToList();

            if (reservaViewModel.procurarPostosViewModel.DistritoID > 0 && reservaViewModel.procurarPostosViewModel.ConcelhoID == 0)
                reservaViewModel.procurarPostosViewModel.EstacaoCarregamentos = db.EstacaoCarregamentoes.Where(e => e.ConcelhoEstacaoCarregamento.DistritoID == reservaViewModel.procurarPostosViewModel.DistritoID).ToList();
            else if (reservaViewModel.procurarPostosViewModel.DistritoID > 0 && reservaViewModel.procurarPostosViewModel.ConcelhoID > 0)
                reservaViewModel.procurarPostosViewModel.EstacaoCarregamentos = db.EstacaoCarregamentoes.Where(e => e.ConcelhoID == reservaViewModel.procurarPostosViewModel.ConcelhoID).ToList();
            else
                reservaViewModel.procurarPostosViewModel.EstacaoCarregamentos = db.EstacaoCarregamentoes.ToList();

            reservaViewModel.procurarPostosViewModel.Potencias = db.Potencias.ToList();
            reservaViewModel.procurarPostosViewModel.Tomadas = db.Tomadas.ToList();
            
            reservaViewModel.novaReservaViewModel = new NovaReservaViewModel();
            reservaViewModel.novaReservaViewModel.DataPrevInicioCarregamento = DateTime.Now.AddMinutes(5);
            reservaViewModel.novaReservaViewModel.DataPrevFimCarregamento = reservaViewModel.novaReservaViewModel.DataPrevInicioCarregamento.AddHours(8);
            
            dataI = reservaViewModel.novaReservaViewModel.DataPrevInicioCarregamento;
            dataF = reservaViewModel.novaReservaViewModel.DataPrevFimCarregamento;

            tomadaPostos = db.TomadaPostoes.ToList();

            tomadaPostos = tomadaPostos.Where(tp => tp.Reservas.Where(r => r.DataPrevInicioCarregamento >= dataI && r.DataPrevInicioCarregamento <= dataF && r.Cancelada == false).Count() == 0).ToList();
            
            tomadaPostos = tomadaPostos.Where(tp => tp.Reservas.Where(r => r.DataPrevFimCarregamento >= dataI && r.DataPrevFimCarregamento <= dataF && r.Cancelada == false).Count() == 0).ToList();

            reservaViewModel.procurarPostosViewModel.TomadaPostos = tomadaPostos;

            return View("Create", reservaViewModel);
        }
       
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadEstacoesCarregamento(int? idDistrito = null, int? idConcelho = null)
        {
            List<EstacaoCarregamento> estacoesCarregamento = null;
            if (idDistrito != null && idConcelho == null)
                estacoesCarregamento = db.EstacaoCarregamentoes.Where(e => e.ConcelhoEstacaoCarregamento.DistritoID == idDistrito).ToList();
            else if (idConcelho != null)
                estacoesCarregamento = db.EstacaoCarregamentoes.Where(e => e.ConcelhoID == idConcelho).ToList();
            else
                estacoesCarregamento = db.EstacaoCarregamentoes.ToList();

            
            List<SelectListItem> EstacoesCarregamentoList = new List<SelectListItem>();
            EstacoesCarregamentoList.Clear();
            //   ProjectsubTypes.Add(new SelectListItem { Text = "--Select Project sub-Type--", Value = "0" });
            if (estacoesCarregamento != null)
            {
                foreach (var estacaoCarregamento in estacoesCarregamento)
                {
                    EstacoesCarregamentoList.Add(new SelectListItem { Text = estacaoCarregamento.Designacao, Value = estacaoCarregamento.ID.ToString() });
                }
            }
            return Json(EstacoesCarregamentoList, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadConcelhos(int idDistrito)
        {
            List<Concelho> concelhos = null;
           
                concelhos = db.Concelhoes.Where(c => c.DistritoID == idDistrito).ToList();


            List<SelectListItem> ConcelhosList = new List<SelectListItem>();
            ConcelhosList.Clear();
            //   ProjectsubTypes.Add(new SelectListItem { Text = "--Select Project sub-Type--", Value = "0" });
            if (concelhos != null)
            {
                foreach (var concelho in concelhos)
                {
                    ConcelhosList.Add(new SelectListItem { Text = concelho.Nome, Value = concelho.ID.ToString() });
                }
            }
            return Json(ConcelhosList, JsonRequestBehavior.AllowGet);
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
            reservaViewModel.procurarPostosViewModel.Concelhos = db.Concelhoes.Where(c => c.DistritoID == reservaViewModel.procurarPostosViewModel.DistritoID).ToList();
            reservaViewModel.procurarPostosViewModel.EstacaoCarregamentos = db.EstacaoCarregamentoes.ToList();
            reservaViewModel.procurarPostosViewModel.Potencias = db.Potencias.ToList();
            reservaViewModel.procurarPostosViewModel.Tomadas = db.Tomadas.ToList();

            reservaViewModel.procurarPostosViewModel.DataInicioCarregamento = DateTime.Now.AddMinutes(5);
            reservaViewModel.procurarPostosViewModel.DataFimCarregamento = reservaViewModel.procurarPostosViewModel.DataInicioCarregamento.AddHours(8);

            reservaViewModel.novaReservaViewModel.DataPrevInicioCarregamento = reservaViewModel.procurarPostosViewModel.DataInicioCarregamento;
            reservaViewModel.novaReservaViewModel.DataPrevFimCarregamento = reservaViewModel.procurarPostosViewModel.DataFimCarregamento;

            List<TomadaPosto> tomadaPostos = db.TomadaPostoes.ToList();
            
            DateTime dataI = reservaViewModel.procurarPostosViewModel.DataInicioCarregamento;
            DateTime dataF = reservaViewModel.procurarPostosViewModel.DataFimCarregamento;


            tomadaPostos = tomadaPostos.Where(tp => tp.Reservas.Where(r => ((r.DataPrevInicioCarregamento >= dataI && r.DataPrevInicioCarregamento <= dataF) || (r.DataPrevFimCarregamento >= dataI && r.DataPrevFimCarregamento <= dataF) || (r.DataPrevInicioCarregamento <= dataI && r.DataPrevFimCarregamento >= dataF)) && (r.Cancelada == false)).Count() == 0).ToList();


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
                reserva.Cancelada = false;

                
                db.Reservas.Add(reserva);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            reservaViewModel.procurarPostosViewModel = new ProcurarPostosViewModel();
            reservaViewModel.procurarPostosViewModel.DataInicioCarregamento = reservaViewModel.novaReservaViewModel.DataPrevInicioCarregamento;
            reservaViewModel.procurarPostosViewModel.DataFimCarregamento = reservaViewModel.novaReservaViewModel.DataPrevFimCarregamento;
            reservaViewModel.procurarPostosViewModel.Concelhos = db.Concelhoes.ToList();
            reservaViewModel.procurarPostosViewModel.Distritos = db.Distritos.ToList();
            reservaViewModel.procurarPostosViewModel.EstacaoCarregamentos = db.EstacaoCarregamentoes.ToList();
            reservaViewModel.procurarPostosViewModel.Potencias = db.Potencias.ToList();
            reservaViewModel.procurarPostosViewModel.Tomadas = db.Tomadas.ToList();


;
            reservaViewModel.procurarPostosViewModel = new ProcurarPostosViewModel();

            reservaViewModel.procurarPostosViewModel.Distritos = db.Distritos.ToList();
            reservaViewModel.procurarPostosViewModel.Concelhos = db.Concelhoes.Where(c => c.DistritoID == reservaViewModel.procurarPostosViewModel.DistritoID).ToList();
            reservaViewModel.procurarPostosViewModel.EstacaoCarregamentos = db.EstacaoCarregamentoes.ToList();
            reservaViewModel.procurarPostosViewModel.Potencias = db.Potencias.ToList();
            reservaViewModel.procurarPostosViewModel.Tomadas = db.Tomadas.ToList();

            reservaViewModel.procurarPostosViewModel.DataInicioCarregamento = reservaViewModel.novaReservaViewModel.DataPrevInicioCarregamento;
            reservaViewModel.procurarPostosViewModel.DataFimCarregamento = reservaViewModel.novaReservaViewModel.DataPrevFimCarregamento;
            

            List<TomadaPosto> tomadaPostos = db.TomadaPostoes.ToList();

            DateTime dataI = reservaViewModel.procurarPostosViewModel.DataInicioCarregamento;
            DateTime dataF = reservaViewModel.procurarPostosViewModel.DataFimCarregamento;


            tomadaPostos = tomadaPostos.Where(tp => tp.Reservas.Where(r => ((r.DataPrevInicioCarregamento >= dataI && r.DataPrevInicioCarregamento <= dataF) || (r.DataPrevFimCarregamento >= dataI && r.DataPrevFimCarregamento <= dataF) || (r.DataPrevInicioCarregamento <= dataI && r.DataPrevFimCarregamento >= dataF)) && (r.Cancelada == false)).Count() == 0).ToList();


            reservaViewModel.procurarPostosViewModel.TomadaPostos = tomadaPostos;




            return View(reservaViewModel);
        }

        

        
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult CancelarReserva(int id)
        {
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
                return HttpNotFound();
            
            reserva.Cancelada = true;

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
