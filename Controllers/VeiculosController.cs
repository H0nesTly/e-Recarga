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
    public class VeiculosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Veiculos
        public ActionResult Index()
        {
            var veiculos = db.Veiculos.Include(v => v.UtilizadorVeiculo);
            return View(veiculos.ToList());
        }

        // GET: Veiculos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veiculo veiculo = db.Veiculos.Find(id);
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return View(veiculo);
        }

        //Não ira ser executado
        // GET: Veiculos/Create
        public ActionResult Create()
        {
            VeiculosViewModel viewModel = new VeiculosViewModel();
            viewModel.CodeDoUser = User.Identity.GetUserId();
            return View(viewModel);
        }

        // POST: Veiculos/_AddCarro
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VeiculosViewModel veiculo)
        {
            if (ModelState.IsValid)
            {
                Veiculo veiculoObj = new Veiculo();
                veiculoObj.UtilizadorID = veiculo.CodeDoUser;
                veiculoObj.Marca = veiculo.Marca;
                veiculoObj.Matricula = veiculo.Matricula;
                veiculoObj.Modelo = veiculo.Modelo;

                db.Veiculos.Add(veiculoObj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(veiculo);
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