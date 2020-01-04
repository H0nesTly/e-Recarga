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
    public class PostoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posto
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Index()
        {
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");

            var postos = db.Postoes.Include(p => p.EstacaoCarregamentoPosto).Where(p => p.EstacaoCarregamentoPosto.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin);
            return View(postos.ToList());
        }

        // GET: Posto/Details/5
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posto posto = db.Postoes.Find(id);
            if (posto == null)
            {
                return HttpNotFound();
            }

            DetalhesPostoViewModel tomada = new DetalhesPostoViewModel()
            {
                correnteCarregamento = posto.CorrenteCarregamento,
                estacaoDeCarregamento = posto.EstacaoCarregamentoPosto.UtilizadorEstacao.Nome,
                numeroDeTomadas = db.TomadaPostoes.Where(x => x.PostoID == id).Count(),
                tomadaPostoViewModel = new TomadaPostoViewModel()
                {
                    codeDoPosto = id.Value,
                    PrecoMinuto = 0,
                    Potencias = db.Potencias.ToList(),
                    Tomadas = db.Tomadas.ToList()
                },
                tomadaPostoLista = db.TomadaPostoes
                        .Where(x => x.PostoID == posto.ID)
                        .Select(x => new TomadaPostoListaViewModel
                        {
                            potencia = x.PotenciaTomadaPosto.PotenciaNominalKw,
                            precoPorMinuto = x.PrecoMinuto,
                            tomadaNome = x.TomadaTomadaPosto.TipoTomada
                        })
            };

            return View(tomada);
        }

        // GET: Posto/Create
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Create()
        {
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");
            bool isRP = User.IsInRole("RedeProprietaria");

            ViewBag.EstacaoCarregamentoID = new SelectList(db.EstacaoCarregamentoes.Where(ec => ec.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin), "ID", "Designacao");
            return View();
        }

        // POST: Posto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Create([Bind(Include = "ID,CorrenteCarregamento,NumeroTomadas,EstacaoCarregamentoID")] Posto posto)

        {
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");
            bool isRP = User.IsInRole("RedeProprietaria");

            if (ModelState.IsValid)
            {
                db.Postoes.Add(posto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstacaoCarregamentoID = new SelectList(db.EstacaoCarregamentoes.Where(ec => ec.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin), "ID", "Designacao", posto.EstacaoCarregamentoID);
            return View(posto);
        }

        // GET: Posto/Edit/5
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
            Posto posto = db.Postoes.Find(id);
            if (posto == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstacaoCarregamentoID = new SelectList(db.EstacaoCarregamentoes.Where(ec => ec.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin), "ID", "Designacao", posto.EstacaoCarregamentoID);
            return View(posto);
        }

        // POST: Posto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Edit([Bind(Include = "ID,CorrenteCarregamento,NumeroTomadas,EstacaoCarregamentoID")] Posto posto)
        {
            string utilizadorSessaoID = User.Identity.GetUserId();
            bool isSAdmin = User.IsInRole("SuperAdmin");
            bool isAdmin = User.IsInRole("Admin");
            bool isRP = User.IsInRole("RedeProprietaria");

            if (ModelState.IsValid)
            {
                db.Entry(posto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstacaoCarregamentoID = new SelectList(db.EstacaoCarregamentoes.Where(ec => ec.UtilizadorID == utilizadorSessaoID || isAdmin || isSAdmin), "ID", "Designacao", posto.EstacaoCarregamentoID);
            return View(posto);
        }

        // GET: Posto/Delete/5
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult Delete(int? id)
        {

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
        [Authorize(Roles = "Admin,SuperAdmin,RedeProprietaria")]
        public ActionResult DeleteConfirmed(int id)
        {

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
