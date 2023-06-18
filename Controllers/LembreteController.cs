using Microsoft.AspNetCore.Mvc;
using Teste_Pratico_dti_MVC.Models;

namespace Teste_Pratico_dti_MVC.Controllers
{   
    //como ainda não tem Conexão com o BD esta usando a lista de lembretes estatica para salvar os lembretes
    public static class LembreteRepository
    {
        public static ListaLembreteModel ListaLembretes { get; } = new ListaLembreteModel();
    }
    public class LembreteController : Controller
    {   
        //Encaminha para a view de criar o lembrete
        public IActionResult CriarLembrete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CriarLembrete(LembreteModel lembrete)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar se o nome está vazio
                    if (string.IsNullOrWhiteSpace(lembrete.Nome))
                    {
                        ModelState.AddModelError("Nome", "O campo nome é obrigatório. Insira um nome válido.");
                        return View(lembrete);
                    }
                    // Verificar se a data informada é válida
                    if (lembrete.Data < DateTime.Now)
                    {
                        ModelState.AddModelError("Data", "A data informada já passou. Insira uma data futura.");
                        return View(lembrete);
                    }

                    // Adicionar o lembrete à lista usando a instância de ListaLembretes compartilhada
                    LembreteRepository.ListaLembretes.AdicionarLembrete(lembrete);

                    return RedirectToAction("Index"); // Redirecionar para a página inicial após a criação do lembrete
                }
                else
                {
                    return View(lembrete);
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(lembrete);
            }
        }

        public IActionResult Index()
        {   //Criar a lista que aparece na view de acordo com a lista de lembrete 
            ListaLembreteModel model = LembreteRepository.ListaLembretes;

            if (model.ListaLembretes.Any())//se ela não estiver vazia é ordenada para que as datas sejam mostradas em ordem cronologica 
            {
                model.ListaLembretes = model.ListaLembretes.OrderBy(d => d.Key).ToDictionary(d => d.Key, d => d.Value);
            }

            return View(model);
        }

        public IActionResult Apagar(string nome)
        {
             LembreteRepository.ListaLembretes.RemoverLembrete(nome);
                return RedirectToAction("Index"); 
        }

    }
}