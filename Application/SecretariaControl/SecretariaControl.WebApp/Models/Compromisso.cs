using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecretariaControl.WebApp.Models
{
    [DataContract]
    public class Compromisso
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "descricao")]
        public string Descricao { get; set; }

        [DataMember(Name = "dataCompromisso")]
        public DateTime DataCompromisso { get; set; }

        [DataMember(Name = "local")]
        public string Local { get; set; }

        [IgnoreDataMember]
        public int IdGerente { get; set; }

        [IgnoreDataMember]
        public List<Contato> Contatos { get; set; }

    }

    public static class CompromissoFactory
    {
        private static SecretariaControl.DataAccess.SecretariaControlEntities Context
        {
            get
            {
                return SecretariaControl.WebApp.Core.Context.ApplicationContext.DataBaseContext;
            }
        }


        public static Compromisso GetCompromisso(int idCompromisso)
        {
            var compromisso = Context.Compromisso.FirstOrDefault(comp => comp.IdCompromisso == idCompromisso);
            return Build(compromisso);
        }

        public static List<Compromisso> GetCompromissoByDesc(String descricao)
        {
            var compromissoList = Context.Compromisso.Where(comp => comp.Descricao.Contains(descricao)).ToList();

            if (!compromissoList.IsNullOrEmpty())
            {
                return compromissoList.Select(comp => Build(comp)).ToList();
            }
            else
            {
                return new List<Compromisso>();
            }
        }

        public static List<Compromisso> GetCompromissos()
        {
            var list = Context.Compromisso.Select(comp => comp).ToList();

            if (!list.IsNullOrEmpty())
            {
                return list.Select(comp => Build(comp)).ToList();
            }
            else
            {
                return new List<Compromisso>();
            }
        }
        public static Compromisso DetalheCompromissos(int idCompromisso)
        {
            var compromisso = Context.Compromisso.Where(c => c.IdCompromisso == idCompromisso).FirstOrDefault();

            return Build(compromisso);
        }
        public static List<Compromisso> GetCompromissos(int idGerente)
        {
            var list = Context.Compromisso.Where(c => c.IdGerente == idGerente).ToList();

            if (!list.IsNullOrEmpty())
            {
                return list.Select(c => Build(c)).ToList();
            }
            else
            {
                return new List<Compromisso>();
            }

        }

        public static void InsertCompromisso(Compromisso c)
        {
            DataAccess.Compromisso compromisso = new DataAccess.Compromisso()
            {
                Descricao = c.Descricao,
                DataCompromisso = c.DataCompromisso,
                Local = c.Local,
                IdGerente = c.IdGerente
            };

            Context.Compromisso.Add(compromisso);
            Context.SaveChanges();

            if (!c.Contatos.IsNullOrEmpty())
            {
                foreach (var contato in c.Contatos)
                {
                    Context.ContatoCompromisso.Add(new DataAccess.ContatoCompromisso()
                    {
                        IdContato = contato.Id,
                        IdCompromisso = compromisso.IdCompromisso
                    });
                } 
            }

            Context.SaveChanges();
        }

        public static void UpdateCompromisso(Compromisso compromisso)
        {
            var compromissoDTO = Context.Compromisso.FirstOrDefault(c => c.IdCompromisso == compromisso.Id);

            if (compromissoDTO == null)
            {
                InsertCompromisso(compromisso);
                return;
            }

            compromissoDTO.Descricao = compromisso.Descricao;
            compromissoDTO.DataCompromisso = compromisso.DataCompromisso;
            compromissoDTO.Local = compromisso.Local;

            if (compromisso.Contatos.IsNullOrEmpty()) compromisso.Contatos = new List<Contato>();

            //remove associação com contatos antigos que já não estão na lista de contatos enviada no compromisso
            //var contactsToRemove = Context.ContatoCompromisso.Where(cc => cc.IdCompromisso == compromisso.Id)
            //                                                 .Where(cc => !compromisso.Contatos.Any(cont => cont.Id == cc.IdContato))
            //                                                 .ToList();
            Context.ContatoCompromisso.RemoveRange(Context.ContatoCompromisso.Where(cc => cc.IdCompromisso == compromisso.Id).ToList());

            foreach (var contato in compromisso.Contatos)
            {
                Context.ContatoCompromisso.Add(new DataAccess.ContatoCompromisso()
                {
                    IdContato = contato.Id,
                    IdCompromisso = compromisso.Id,
                });
            }

            Context.SaveChanges();
        }

        internal static Compromisso Build(DataAccess.Compromisso compromissoDTO)
        {
            if (compromissoDTO == null) return null;

            return new Compromisso()
            {
                Id = compromissoDTO.IdCompromisso,
                DataCompromisso = compromissoDTO.DataCompromisso,
                Descricao = compromissoDTO.Descricao,
                Local = compromissoDTO.Local,
                Contatos = ContatoFactory.GetContatosByIdCompromisso(compromissoDTO.IdCompromisso),
                IdGerente = compromissoDTO.IdGerente,
            };
        }

        internal static void SaveCompromisso(Compromisso compromisso)
        {
            DataAccess.Compromisso c = new DataAccess.Compromisso()
            {
                // preenchier atributos...
            };

            //adicionar c no banco
        }

        internal static void DeleteCompromisso(int idCompromisso)
        {
            var compromissoRemovido = Context.Compromisso.SingleOrDefault(i => i.IdCompromisso == idCompromisso);
            if (compromissoRemovido != null)
            {
                Context.ContatoCompromisso.RemoveRange(compromissoRemovido.ContatoCompromisso);
                Context.Compromisso.Remove(compromissoRemovido);
                Context.SaveChanges();
            }
        }
    }
}
