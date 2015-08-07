using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretariaControl.WebApp.Models
{
    public class Secretaria : AbstractUsuario
    {
        public List<Agenda> Agendas { get; set; }

    }

    public static class SecretariaFactory
    {
        private static SecretariaControl.DataAccess.SecretariaControlEntities Context
        {
            get
            {
                return SecretariaControl.WebApp.Core.Context.ApplicationContext.DataBaseContext;
            }
        }

        public static List<Secretaria> GetSecretarias(bool buscarAgenda = false)
        {
            var list = Context.Usuario.Where(u => u.Perfil == (int)Perfil.Secretaria).ToList();

            if (!list.IsNullOrEmpty())
            {
                return list.Select(secr => Build(secr)).ToList(); 
            }
            else
            {
                return new List<Secretaria>();
            }
        }

        public static Secretaria GetSecretaria(int id, bool buscarAgenda = false)
        {
            var secr = (Secretaria) UsuarioFactory.GetUsuario(id);
            return secr;
        }

        public static Secretaria GetSecretaria(string email, bool buscarAgenda = false)
        {
            var secr = (Secretaria) UsuarioFactory.GetUsuario(email);
            return secr;
        }
        

        private static Secretaria Build(DataAccess.Usuario usuarioDb, bool buscarAgenda = false)
        {
            if (usuarioDb == null) return null;
            
            return new Secretaria()
            {
                Id = usuarioDb.IdUsuario,
                Email = usuarioDb.Email,
                Nome = usuarioDb.Nome,
                Perfil = (Perfil)usuarioDb.Perfil,
                Telefone = usuarioDb.Telefone,
                Foto = usuarioDb.Foto,
                               
            };
        }
    }
}