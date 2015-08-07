using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SecretariaControl.WebApp.Models
{
    public enum Perfil
    {
        Administrador,
        Gerente,
        Secretaria
    }

    [DataContract]
    public abstract class AbstractUsuario
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "email")]
        public String Email { get; set; }

        [DataMember(Name = "nome")]
        public String Nome { get; set; }

        [DataMember(Name = "telefone")]
        public String Telefone { get; set; }

        [DataMember(Name = "perfil")]
        public Perfil Perfil { get; set; }

        [DataMember(Name = "foto")]
        public String Foto { get; set; }

    }

    public static class UsuarioFactory
    {
        private static SecretariaControl.DataAccess.SecretariaControlEntities Context
        {
            get
            {
                return SecretariaControl.WebApp.Core.Context.ApplicationContext.DataBaseContext;
            }
        }

        public static AbstractUsuario GetUsuario(string email)
        {
            var user = Context.Usuario.Where(u => u.Email == email).FirstOrDefault();
            return Build(user);
        }
        public static AbstractUsuario GetUsuario(int id)
        {
            var user = Context.Usuario.Where(u => u.IdUsuario == id).FirstOrDefault();
            return Build(user);
        }

        private static AbstractUsuario Build(DataAccess.Usuario user)
        {
            if (user == null) return null;

            switch ((Perfil)user.Perfil)
            {
                case Perfil.Administrador:
                    return new Administrador()
                    {
                        Id = user.IdUsuario,
                        Nome = user.Nome,
                        Perfil = (Perfil)user.Perfil,
                        Telefone = user.Telefone,
                        Foto = user.Foto,
                        Email = user.Email,
                    };
                case Perfil.Gerente:
                    return new Gerente()
                    {
                        Id = user.IdUsuario,
                        Nome = user.Nome,
                        Perfil = (Perfil)user.Perfil,
                        Telefone = user.Telefone,
                        Foto = user.Foto,
                        Email = user.Email,
                    };
                case Perfil.Secretaria:
                default:
                    return new Secretaria()
                    {
                        Id = user.IdUsuario,
                        Nome = user.Nome,
                        Perfil = (Perfil)user.Perfil,
                        Telefone = user.Telefone,
                        Foto = user.Foto,
                        Email = user.Email,
                    };
            }
        }
    }
}