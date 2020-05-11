using ProjetBDD3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetBDD3.Controllers
{
    public class AccueilController : Controller
    {
        // GET: Accueil
        public ActionResult Accueil()
        {
            Personnel personnel = new Personnel();
            ViewBag.listePersonnel = personnel.GetAllPersonnel();
            Vol vol = new Vol();
            ViewBag.listeVols = vol.GetAllVols();
            DepartVol departVol = new DepartVol();
            ViewBag.departVol = departVol.GetAllDepartVols();
            return View();
        }

        public ActionResult ProcedureProgrammer(string noVol, string date, string heure, string dureeVol)
        {
            Vol vol = new Vol();
            vol.ProcedureDepartVol(noVol, date, heure, dureeVol);
            return Json(new { Response = "Success" });
        }

        public ActionResult ProcedureAffecterPersonnel(string noVol, string date, int matricule)
        {
            DepartVol departVol = new DepartVol();
            departVol.ProcedureAffecterPersonnel(noVol, date, matricule);
            return Json(new { Response = "Success" });
        }

        public ActionResult ProcedureMembresEquipage(string noVol, string date)
        {
            List<Personnel> listePersonnel = new List<Personnel>();
            Personnel personnel = new Personnel();
            listePersonnel = personnel.ProcedureMembresEquipage(noVol, date);
            return Json(new { Response = "Success", personne = listePersonnel });
        }
    }
}