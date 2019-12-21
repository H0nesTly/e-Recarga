using e_Recarga.Models;
using e_Recarga.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Recarga.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dashboard
        public ActionResult Index()
        {
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isRedeProprietaria = User.IsInRole("RedeProprietaria");
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");
            DashboardViewModel dashboardViewModel = new DashboardViewModel();

            if (isRedeProprietaria)
            {

                dashboardViewModel.Top5PostosMesCorrente = db.Reservas.Where(r => r.DataPrevInicioCarregamento.Month == DateTime.Now.Month && r.TomadaPostoReserva.PostoTomadaPosto.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID).Select(r => r.TomadaPostoReserva).OrderByDescending(tp => tp.Reservas.Count()).Select(tp => tp.PostoTomadaPosto).Distinct().Take(5).ToList();

                dashboardViewModel.Top5PostosAnoCorrente = db.Reservas.Where(r => r.DataPrevInicioCarregamento.Year == DateTime.Now.Year && r.TomadaPostoReserva.PostoTomadaPosto.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID).Select(r => r.TomadaPostoReserva).OrderByDescending(tp => tp.Reservas.Count()).Select(tp => tp.PostoTomadaPosto).Distinct().Take(5).ToList();

                dashboardViewModel.ReservasMesCorrente = db.Reservas.Where(r => r.DataPrevInicioCarregamento.Month == DateTime.Now.Month && r.TomadaPostoReserva.PostoTomadaPosto.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID).Count();

                dashboardViewModel.ReservasAnoCorrente = db.Reservas.Where(r => r.DataPrevInicioCarregamento.Year == DateTime.Now.Year && r.TomadaPostoReserva.PostoTomadaPosto.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID).Count();

                dashboardViewModel.ReservasDiaCorrente = db.Reservas.Where(r => r.DataPrevInicioCarregamento.Day == DateTime.Now.Day && r.TomadaPostoReserva.PostoTomadaPosto.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID).Count();


            }










            return View(dashboardViewModel);
        }
    }
}