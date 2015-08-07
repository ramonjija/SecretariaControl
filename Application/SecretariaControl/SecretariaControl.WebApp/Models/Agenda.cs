using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretariaControl.WebApp.Models
{
    public class Agenda
    {
        public int Id { get; set; }
        
        public Gerente Gerente { get; set; }
        
        public Secretaria Secretaria { get; set; }

        public List<Compromisso> Compromissos { get; set; }
        
        public List<Contato> Contatos { get; set; }
    }

    public static class AgendaFactory
    {
        private static SecretariaControl.DataAccess.SecretariaControlEntities Context
        {
            get
            {
                return SecretariaControl.WebApp.Core.Context.ApplicationContext.DataBaseContext;
            }
        }

        public static List<Agenda> GetAgendasByIdSecretaria(int idSecretaria)
        {
            var gerentes = Context.Usuario.Where(gerente => gerente.IdSecretaria != null && (int)gerente.IdSecretaria == idSecretaria).ToList();

            return gerentes.Select(gerente => Build(gerente)).ToList();
        }

        public static Agenda GetAgendaByIdGerente(int idGerente)
        {
            var gerente = Context.Usuario.Where(u => u.IdUsuario == idGerente && u.Perfil == (int)Perfil.Gerente).FirstOrDefault();            

            return Build(gerente);
        }
        public static void AssociarSecretaria(int agendaId, int id)
        {
            var agenda = Context.Usuario.Where(u => u.IdUsuario == agendaId && u.Perfil == (int)Perfil.Gerente).FirstOrDefault();

            if (agenda != null)
            {
                agenda.IdSecretaria = id;
                Context.SaveChanges();
            }

        }

        public static void DesassociarSecretaria(int agendaId)
        {
            var agenda = Context.Usuario.Where(u => u.IdUsuario == agendaId && u.Perfil == (int)Perfil.Gerente).FirstOrDefault();

            if (agenda != null && agenda.IdSecretaria != null)
            {
                agenda.IdSecretaria = null;
                Context.SaveChanges();
            }

        }

        private static Agenda Build(DataAccess.Usuario gerente)
        {
            return new Agenda()
            {
                Id = gerente.IdUsuario,
                Gerente = GerenteFactory.Build(gerente),
                Secretaria = gerente.IdSecretaria != null ? SecretariaFactory.GetSecretaria((int)gerente.IdSecretaria) : null,
                Compromissos = gerente.Compromisso.Select(c => CompromissoFactory.Build(c)).ToList(),
                Contatos = gerente.Contato.Select(c => ContatoFactory.Build(c)).ToList(),
            };
        }
    }
}