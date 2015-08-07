using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretariaControl.WebApp.Models.ViewModels
{
    public class SecretariaViewModel
    {

        public int IdAgenda { get; set; }
        public List<Secretaria> Secretarias { get; set; }
        public Secretaria SecretariaSelecionada { get; set; }
        public Secretaria SecretariaAssociada { get; set; }

        public SecretariaViewModel()
        {
            this.Secretarias = new List<Secretaria>();
        }
    }
}