using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecretariaControl.WebApp.Models;
using SecretariaControl.WebApp.Models.ViewModels;

namespace SecretariaControl.WebApp.Controllers
{
    public class ContatoController : Controller
    {
        //
        // GET: /Contato/

        //
        // GET: /CadastroContato/

        public ActionResult Insert(int? idContato)
        {
            IEnumerable<SelectListItem> listaDeGerentes = new SelectList(GerenteFactory.GetGerente(), "Id", "Nome");
            ViewData["Gerentes"] = listaDeGerentes;
            Contato contato = new Contato();
            if (idContato != null)
            {
                contato = ContatoFactory.GetContato((int)idContato);
                return View(contato);
            }
            return View(contato);
        }

        public ActionResult DeletarContato(int idContato)
        {
            ContatoFactory.DeleteContato(idContato);
            return RedirectToAction("List", "Contato");
        }

        [HttpPost]
        public ActionResult CadastrarContato(int? idContato, Models.Contato contato)
        {
            if (idContato != null || idContato > 0)
            {
                contato.Id = (int)idContato;
            }
            ContatoFactory.InsertContato(contato.Id, contato.Descricao, contato.Telefone, contato.Email, contato.IdGerente);

            return RedirectToAction("List", "Contato");
        }

       
        public ActionResult List(int? id)
        {            
            CompromissoViewModel viewModel = new CompromissoViewModel();

            List<Contato> listaDeContatos = null;

            if (id != null)
            {
                listaDeContatos = AgendaFactory.GetAgendaByIdGerente((int)id).Contatos; 
            }
            else
            {
                listaDeContatos = ContatoFactory.GetContato();
            }

            viewModel.Contatos = listaDeContatos;
            return View(viewModel);

        }
        public ActionResult ViewContact()
        {
            return View();
        }

    }
}