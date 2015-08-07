using SecretariaControl.WebApp.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SecretariaControl.WebApp.Models
{
    [DataContract]
    public class UsuarioLogin
    {
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "senha")]
        public string Senha { get; set; }

        [IgnoreDataMember]
        public AbstractUsuario Usuario
        {
            get
            {
                return UsuarioFactory.GetUsuario(this.Email);
            }
        }

        public bool Validate()
        {
            return ApplicationContext.DataBaseContext.Usuario.Any(u => u.Email == this.Email && u.Senha == this.Senha);
        }
    }
}