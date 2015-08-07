using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretariaControl.WebApp.Models
{
    public class Gerente : AbstractUsuario
    {
        public Agenda Agenda { get; set; }
    }

    public static class GerenteFactory
    {
        private static SecretariaControl.DataAccess.SecretariaControlEntities Context
        {
            get
            {
                return SecretariaControl.WebApp.Core.Context.ApplicationContext.DataBaseContext;
            }
        }
        public static List<Gerente> GetGerente(bool buscarAgenda = false)
        {
            List<Gerente> listaDeGerentes = new List<Gerente>();
            var users = Context.Usuario.Where(u => u.Perfil == (int)Perfil.Gerente);
            foreach(var user in users)
            {
                listaDeGerentes.Add(Build(user));
            }
            return listaDeGerentes;
        }
        public static List<Gerente> GetGerenteSec(int idSecretaria, bool buscarAgenda = false)
        {
            List<Gerente> listaDeGerentes = new List<Gerente>();
            var users = Context.Usuario.Where(u => u.Perfil == (int)Perfil.Gerente && u.IdSecretaria == idSecretaria);
            foreach (var user in users)
            {
                listaDeGerentes.Add(Build(user));
            }
            return listaDeGerentes;
        }
        public static Gerente GetGerente(int id, bool buscarAgenda = false)
        {
            var user = Context.Usuario.Where(u => u.IdUsuario == id && u.Perfil == (int)Perfil.Gerente).FirstOrDefault();
            return Build(user);
        }

        public static Gerente GetGerente(string email, bool buscarAgenda = false)
        {
            var user = Context.Usuario.Where(u => u.Email == email && u.Perfil == (int)Perfil.Gerente).FirstOrDefault();
            return Build(user);
        }

        internal static Gerente Build(DataAccess.Usuario usuarioDb)
        {
            if (usuarioDb == null) return null;

            return new Gerente()
            {
                Id = usuarioDb.IdUsuario,
                Nome = usuarioDb.Nome,
                Perfil = (Perfil)usuarioDb.Perfil,
                Telefone = usuarioDb.Telefone,
                Foto = usuarioDb.Foto
            };
        }
    }
}
