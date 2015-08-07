using SecretariaControl.DataAccess;
using SecretariaControl.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SecretariaControl.WebApp.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : Controller
    {

        [Route("login")]
        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl = null)
        {
            return View(new UsuarioLogin());
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public ActionResult Login(UsuarioLogin usuario, string ReturnUrl = null)
        {
            if (usuario.Validate())
            {
                //TODO: colocar usuario na sessão
                FormsAuthentication.RedirectFromLoginPage(usuario.Email, true);
            }
            return View(usuario);
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }

        [AllowAnonymous]
        [Route("unauthorized")]
        public ActionResult Unauthorized(string ReturnUrl = null)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/account/login"+ReturnUrl);
            }
            return View();
        }
        

    }
}
