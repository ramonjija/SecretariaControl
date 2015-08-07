using SecretariaControl.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace SecretariaControl.WebApp.Core.Authentication
{

    public class CustomIdentity : FormsIdentity
    {
        private string _userEmail { get; set; }
        private AbstractUsuario _modelUser;

        public AbstractUsuario ModelUser
        {
            get {
                if (_modelUser == null)
                {
                    _modelUser = UsuarioFactory.GetUsuario(this._userEmail);
                }
                return _modelUser; 
            }
            private set { _modelUser = value; }
        }


        public CustomIdentity(FormsAuthenticationTicket ticket)
            :base(ticket)
        {
            this._userEmail = ticket.Name;
        }

        public CustomIdentity(FormsIdentity identity)
            :base(identity)
        {
            this._userEmail = identity.Name;

        }

    }
    
    public class CustomPrincipal : IPrincipal
    {
        private CustomIdentity userIdentity;

        public CustomPrincipal()
        {
        }

        public CustomPrincipal(CustomIdentity userIdentity)
        {
            this.userIdentity = userIdentity;
        }

        public CustomPrincipal(FormsAuthenticationTicket ticket)
        {
            this.userIdentity = new CustomIdentity(ticket);
        }
        
        public IIdentity Identity
        {
            get {
                return userIdentity;
            }
        }

        public bool IsInRole(string role)
        {
            Perfil perfil = this.userIdentity.ModelUser.Perfil;
            return string.Compare(perfil.ToString(), role, true) == 0;
        }
    }
    
    //public static class PrincipalExtensions
    //{
    //    //public static AbstractUsuario GetUserModel(this IPrincipal principal)
    //    //{
    //    //    return (principal.Identity as CustomIdentity).ModelUser;
    //    //}
    //    public static AbstractUsuario UsuarioLogado(this IPrincipal principal)
    //    {
    //        return (principal.Identity as CustomIdentity).ModelUser;
    //    }
    //}
}