using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaseBall.Modelos;
using NLog;

namespace MvcBaseBall.Controllers
{
    
    public class EquipoController : Controller
    {

        //private static ILogger log = LogManager.GetCurrentClassLogger();

        // GET: Equipo
        public ActionResult Index()
        {
            //log.Info("Entrando en Accion Index de Equipo");

           

            ViewBag.Year = "2014";
            
            return View(ObtenerEquipos(2014));
        }

        private Equipo[] ObtenerEquipos(int year)
        {
            string clave = "Team" + year.ToString();
            Equipo[] equipos = null;

            if (HttpContext.Cache[clave]== null)
            {
                ServicioEquipos.SrvEquiposClient Equipos = new ServicioEquipos.SrvEquiposClient();
                equipos = Equipos.GetEquiposByYear(year);
                HttpContext.Cache.Insert(clave, equipos, null, DateTime.UtcNow.AddSeconds(5), System.Web.Caching.Cache.NoSlidingExpiration);
            }


            return (Equipo[])HttpContext.Cache[clave];
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

        public ActionResult Modificar(int year, string equipo)
        {
            Equipo eq = new Equipo() { teamID = "ARI", YearId = 2014 };

            return View(eq);
        }


        public ActionResult Asignar(int year, string equipo)
        {
            Equipo eq = new Equipo() { teamID = "ARI", YearId = 2014 };

            ServicioEquipos.SrvEquiposClient equipos = new ServicioEquipos.SrvEquiposClient();
            ViewBag.JugadoresEquipo = equipos.GetJugadoresEquipoAño("ARI", 2014).ToList();

            //ViewBag.JugadoresLibres = equipos.GetJugadoresLibresAño(2014).ToList(); Cambio sin importancia

            return View(eq);
        }

        [HttpPost]
        public ActionResult Asignar()
        {
            List<string> jugadoresOut = new List<string>();
            List<string> jugadoresIn = new List<string>();

            foreach (var key in HttpContext.Request.Params.AllKeys)
            {
                if (key.StartsWith("Out.") && HttpContext.Request.Params[key]!="false")
                {
                    jugadoresOut.Add(key.Substring(4));
                }
            }
            //TODO Servicio para borrar de la tabla da apariciones estos jugadores

            foreach (var key in HttpContext.Request.Params.AllKeys)
            {
                if (key.StartsWith("In.") && HttpContext.Request.Params[key] != "false")
                {
                    jugadoresIn.Add(key.Substring(3));
                }
            }

            //TODO Servicio para insertar en la tabla da apariciones estos jugadores al equipo año correspondientes

            Equipo eq = new Equipo() { teamID = "ARI", YearId = 2014 };

            ServicioEquipos.SrvEquiposClient equipos = new ServicioEquipos.SrvEquiposClient();
            ViewBag.JugadoresEquipo = equipos.GetJugadoresEquipoAño("ARI", 2014).ToList();
            //ViewBag.JugadoresLibres = equipos.GetJugadoresLibresAño(2014).ToList();

            return View(eq);
        }
    }
}