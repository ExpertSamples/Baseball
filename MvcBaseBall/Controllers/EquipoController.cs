using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaseBall.Modelos;

namespace MvcBaseBall.Controllers
{
    public class EquipoController : Controller
    {
        // GET: Equipo
        public ActionResult Index()
        {
            ViewBag.Year = "2014";

            ServicioEquipos.SrvEquiposClient Equipos = new ServicioEquipos.SrvEquiposClient();

            return View(Equipos.GetEquiposByYear(2014));
        }



        // Post: Equipo
        [HttpPost]
        public ActionResult Index(int year)
        {

            ViewBag.Year = year.ToString();

            ServicioEquipos.SrvEquiposClient Equipos = new ServicioEquipos.SrvEquiposClient();

            return View(Equipos.GetEquiposByYear(year));
        }

        
        public ActionResult Year(string equipo, int year)
        {

            ServicioEquipos.SrvEquiposClient Equipos = new ServicioEquipos.SrvEquiposClient();

            ViewBag.Players = Equipos.GetJugadoresEquipoAño(equipo, year);

            return View("Index",Equipos.GetEquiposByYear(year));
        }


        public ActionResult Jugadores(string equipo, int year)
        {
            ServicioEquipos.SrvEquiposClient equipos = new ServicioEquipos.SrvEquiposClient();
            List<Player> jugadores = equipos.GetJugadoresEquipoAño(equipo, year).ToList();

            ViewBag.Year = year;
            ViewBag.IdEquipo = equipo;

            return PartialView("ListaJugadores", jugadores);
        }
    }
}