using SecretariaControl.WebApp.Core.Authentication;
using SecretariaControl.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace System.Web
{
    public static class IPrincipalExtensions
    {
        public static AbstractUsuario UsuarioLogado(this IPrincipal principal)
        {
            return (principal.Identity as CustomIdentity).ModelUser;
        }
    }
}