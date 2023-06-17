using Microsoft.AspNetCore.Mvc;
using Teste_Pratico_dti_MVC.Models;

namespace Teste_Pratico_dti_MVC.Controllers
{
    public static class LembreteRepository
    {
        public static ListaLembreteModel ListaLembretes { get; } = new ListaLembreteModel();
    }
    public class LembreteController : Controller
    {
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

                    // Criar o lembrete com os dados recebidos do formulário
                    LembreteModel novoLembrete = new LembreteModel(lembrete.Nome, lembrete.Data);

                    // Adicionar o lembrete à lista usando a instância de ListaLembretes compartilhada
                    LembreteRepository.ListaLembretes.AdicionarLembrete(novoLembrete);

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
        {
            ListaLembreteModel model = LembreteRepository.ListaLembretes;
            return View(model);
        }
        public IActionResult Apagar(string nome)
        {
            bool lembreteRemovido = LembreteRepository.ListaLembretes.RemoverLembrete(nome);

            if (lembreteRemovido)
            {
                return RedirectToAction("Index"); // Redirecionar para a página inicial após a remoção do lembrete
            }
            else
            {
                return RedirectToAction("Index"); // Redirecionar para a página inicial caso o lembrete não seja encontrado
            }
        }

    }
}