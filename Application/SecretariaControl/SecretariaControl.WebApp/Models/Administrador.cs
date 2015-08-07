using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretariaControl.WebApp.Models
{
    public class Administrador : AbstractUsuario
    {
    }

    public static class AdministradorFactory
    {
        public static Administrador GetAdministrador(int id)
        {
            return (Administrador) UsuarioFactory.GetUsuario(id);
        }

        public static Administrador GetAdministrador(string email)
        {
            return (Administrador) UsuarioFactory.GetUsuario(email);
        }
    }
}