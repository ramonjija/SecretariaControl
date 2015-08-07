using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecretariaControl.WebApp.Models
{
    [DataContract]
    public class Contato
    {
        [DataMember(Name = "IdContato")]
        public int Id { get; set; }
        [DataMember(Name = "descricao")]
        public string Descricao { get; set; }
        [DataMember(Name = "telefone")]
        public string Telefone { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "IdGerente")]
        public int IdGerente { get; set; }
    }
    public static class ContatoFactory
    {
        private static SecretariaControl.DataAccess.SecretariaControlEntities Context
        {
            get
            {
                return SecretariaControl.WebApp.Core.Context.ApplicationContext.DataBaseContext;
            }
        }

        public static List<Contato> GetContato()
        {
            List<Contato> ListaDeContatos = new List<Contato>();
            var listaDeObjetosContatos = Context.Contato.ToArray();
            foreach (var objeto in listaDeObjetosContatos)
            {
                ListaDeContatos.Add(Build(objeto));
            }
            return ListaDeContatos;
        }
        public static Contato GetContato(int Id)
        {
            var contato = Context.Contato.Where(u => u.IdContato == Id).FirstOrDefault();
            return Build(contato);
        }

        public static List<Contato> GetContatosByIdCompromisso(int idCompromisso)
        {
            var list = Context.ContatoCompromisso
                              .Where(cc => cc.IdCompromisso == idCompromisso)
                              .Select(cc => cc.Contato)
                              .ToList();

            if (!list.IsNullOrEmpty())
            {
                return list.Select(c => Build(c)).ToList();
            }
            else
            {
                return new List<Contato>();
            }
        }

        public static List<Contato> GetContatosByIdGerente(int idGerente)
        {
            var list = Context.Contato.Where(c => c.IdGerente == idGerente).ToList();

            if (!list.IsNullOrEmpty())
            {
                return list.Select(c => Build(c)).ToList();
            }
            else
            {
                return new List<Contato>();
            }
        }

        public static void InsertContato(int idContato, String descricao, String telefone, String email, int idGerente)
        {

            var contatoBuscado = Context.Contato.Find(idContato);
            if (contatoBuscado != null)
            {
                contatoBuscado.Descricao = descricao;
                contatoBuscado.Telefone = telefone;
                contatoBuscado.Email = email;
            }
            else
            {
                DataAccess.Contato contato = new DataAccess.Contato()
                {
                    Descricao = descricao,
                    Telefone = telefone,
                    Email = email,
                    IdGerente = idGerente
                };
                Context.Contato.Add(contato);
            }
            Context.SaveChanges();

        }

        public static void DeleteContato(int idContato)
        {
            var contatoRemovido = Context.Contato.SingleOrDefault(i => i.IdContato == idContato);
            if (contatoRemovido != null)
            {
                Context.Contato.Remove(contatoRemovido);
                Context.SaveChanges();
            }
        }

        internal static Contato Build(DataAccess.Contato contatoDb)
        {
            if (contatoDb == null) return null;

            return new Contato()
            {
                Id = contatoDb.IdContato,
                Descricao = contatoDb.Descricao,
                Telefone = contatoDb.Telefone,
                Email = contatoDb.Email,
                IdGerente = contatoDb.IdGerente
            };
        }
    }
}
