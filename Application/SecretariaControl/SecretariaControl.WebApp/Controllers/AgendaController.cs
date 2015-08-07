using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecretariaControl.WebApp.Models;
using SecretariaControl.WebApp.Models.ViewModels;

namespace SecretariaControl.WebApp.Controllers
{
    [RoutePrefix("agenda")]
    public class AgendaController : Controller
    {
        public ActionResult Index(int Id)
        {

            var agenda = AgendaFactory.GetAgendaByIdGerente(Id);
            return View(agenda);
        }


        //
        // GET: /Agenda/Secretaria/
        [HttpGet]
        [Route("{idAgenda}/secretaria/{id?}")]
        [Authorize(Roles = "Gerente")]
        public ActionResult Secretaria(int idAgenda, int? id)
        {
            var loggedUser = User.UsuarioLogado();
            var ag = AgendaFactory.GetAgendaByIdGerente(idAgenda);
            var model = new SecretariaViewModel()
            {
                IdAgenda = idAgenda,
                Secretarias = SecretariaFactory.GetSecretarias(),
                SecretariaAssociada = ag.Secretaria,
                SecretariaSelecionada = id != null ? SecretariaFactory.GetSecretaria((int)id) : ag.Secretaria,
            };

            return View(model);
        }

        [HttpPost]
        [Route("{idAgenda}/secretaria/associar/{id}")]
        [Authorize(Roles = "Gerente")]
        public ActionResult AssociarSecretaria(int idAgenda, int id)
        {
            AgendaFactory.AssociarSecretaria(idAgenda, id);
            return RedirectToAction("Secretaria", new { idAgenda = idAgenda, id = id });
        }

        [HttpPost]
        [Route("{idAgenda}/secretaria/desassociar")]
        [Authorize(Roles = "Gerente")]
        public ActionResult AssociarSecretaria(int idAgenda)
        {
            AgendaFactory.DesassociarSecretaria(idAgenda);
            return RedirectToAction("Secretaria", new { idAgenda = idAgenda });
        }


        //
        // GET: /Agenda/Compromisso/
        [HttpGet]
        [Route("{idAgenda}/compromisso/{id?}")]
        public ActionResult Compromisso(int idAgenda, int? id)
        {
            var viewModel = new CompromissoViewModel()
            {
                Contatos = ContatoFactory.GetContato(),
                Compromisso = id != null ? CompromissoFactory.GetCompromisso((int)id) : new Compromisso(),
            };

            if (id == null)
            {
                viewModel.Compromisso.IdGerente = idAgenda;
            }

            if (!viewModel.Compromisso.Contatos.IsNullOrEmpty())
            {
                foreach (var contato in viewModel.Compromisso.Contatos)
                {
                    viewModel.ContatosSelecionados.Add(contato.Id);
                }
            }

            return View(viewModel);
        }

        //
        // POST: /Agenda/Compromisso/
        [HttpPost]
        [Route("{idAgenda}/compromisso/{id?}")]
        public ActionResult Compromisso(int idAgenda, int? id, CompromissoViewModel viewModel = null, bool salvarCompromisso = false, int? removerContato = null)
        {
            viewModel.Contatos = ContatoFactory.GetContato();
            viewModel.Compromisso.IdGerente = idAgenda;
            
            if (id != null) viewModel.Compromisso.Id = (int)id;

            if (salvarCompromisso)
            {
                if (!viewModel.ContatosSelecionados.IsNullOrEmpty())
                {
                    foreach (var contatoId in viewModel.ContatosSelecionados.GetRange(1, viewModel.ContatosSelecionados.Count - 1))
                    {
                        if (viewModel.Compromisso.Contatos == null) viewModel.Compromisso.Contatos = new List<Contato>();
                        viewModel.Compromisso.Contatos.Add(viewModel.ContatosDic[contatoId]);
                    }
                }

                if (id == null)
                {
                    CompromissoFactory.InsertCompromisso(viewModel.Compromisso);
                }
                else
                {
                    CompromissoFactory.UpdateCompromisso(viewModel.Compromisso);
                }

                return RedirectToAction("Index", new { id = idAgenda });
            }

            if (removerContato != null) // removerContato possui o id do contato a ser removido
            {
                viewModel.ContatosSelecionados = viewModel.ContatosSelecionados
                                                            .GetRange(1, viewModel.ContatosSelecionados.Count - 1)
                                                            .Where(idCnt => idCnt != (int)removerContato).ToList();
            }

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Compromisso(String descricao, int idAgenda)
        {
            if (descricao == "")
            {
                return View("Index", AgendaFactory.GetAgendaByIdGerente(idAgenda));
            }
            Agenda agendaBuscada = new Agenda();
            agendaBuscada.Compromissos = new List<Models.Compromisso>();
            agendaBuscada.Gerente = GerenteFactory.GetGerente(idAgenda);

            var listaDeCompromissos = CompromissoFactory.GetCompromissoByDesc(descricao);
            if (listaDeCompromissos != null)
            {
                foreach (var compromisso in listaDeCompromissos)
                {
                    if (compromisso.IdGerente == idAgenda)
                    {
                        agendaBuscada.Compromissos.Add(compromisso);
                    }
                }

            }

            return View("Index", agendaBuscada);

        }

        public ActionResult DeletarCompromisso(int idCompromisso, int idGerente)
        {
            CompromissoFactory.DeleteCompromisso(idCompromisso);
            return RedirectToAction("Index/" + idGerente);
        }

        public ActionResult Detalhe(int id)
        {
            var compromisso = CompromissoFactory.DetalheCompromissos((int)id);
            return View(compromisso);
        }



    }
}
