using BaseBall.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBaseBall.Controllers
{
    public class JugadorController : Controller
    {
        // GET: Jugador
        public ActionResult Index(string id)
        {
            ServicioEquipos.SrvEquiposClient servicio = new ServicioEquipos.SrvEquiposClient();

            return View(servicio.GetJugador(id));
        }

        public ActionResult Modificar(string id, int year, string equipo)
        {
          
            ServicioEquipos.SrvEquiposClient servicio = new ServicioEquipos.SrvEquiposClient();

            ViewBag.Year = year;
            ViewBag.Equipo = equipo;

            return View(servicio.GetJugador(id));
        }

        [HttpPost]
        public ActionResult Modificar(Player jugador)
        {
            HttpContext.Request.Params["year"].ToString();

            ServicioEquipos.SrvEquiposClient servicio = new ServicioEquipos.SrvEquiposClient();
            servicio.ModificarJugador(jugador);

            return RedirectToAction("index","Equipo");
        }
    }
}