using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SecretariaControl.WebApp.Models.ViewModels
{
    [DataContract]
    public class CompromissoViewModel
    {
        [DataMember(Name = "compromisso")]
        public Compromisso Compromisso { get; set; }

        [DataMember(Name = "contatosSelecionados")]
        public List<int> ContatosSelecionados { get; set; }

        public List<Contato> Contatos { get; set; }

        public Dictionary<int, Contato> ContatosDic
        {
            get
            {
                return this.Contatos.ToDictionary(contato => contato.Id, contato => contato);
            }
        }

        public CompromissoViewModel()
        {
            this.ContatosSelecionados = new List<int>();
            this.Contatos = new List<Contato>();
        }
    }
}