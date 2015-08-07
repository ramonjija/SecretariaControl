using SecretariaControl.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecretariaControl.WebApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        [Authorize(Roles = "Administrador, Gerente, Secretaria")]
        public ActionResult Index()
        {
            var loggedUser = User.UsuarioLogado();

            if (loggedUser == null)
            {
                return RedirectToAction("Index", "Login");

            }

            if (loggedUser.Perfil == Perfil.Gerente)
            {
                return RedirectToAction("Index", "Agenda", new { id = loggedUser.Id });
            }


            var agendas = AgendaFactory.GetAgendasByIdSecretaria(loggedUser.Id);

            return View(agendas);
        }


    }
}
