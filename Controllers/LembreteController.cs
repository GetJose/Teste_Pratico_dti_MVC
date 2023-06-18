using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Teste_Pratico_dti_MVC.Models;

namespace Teste_Pratico_dti_MVC.Controllers
{
	// como ainda não tem Conexão com o BD esta usando a lista de lembretes estática para salvar os lembretes
	public static class LembreteRepository
	{
		public static List<LembreteModel> ListaLembretes { get; } = new List<LembreteModel>();
	}

	public class LembreteController : Controller
	{
		// Encaminha para a view de criar o lembrete
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
					// Adicionar o lembrete à lista usando a instância de ListaLembretes compartilhada
					LembreteRepository.ListaLembretes.Add(lembrete);

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
			// Criar a lista que aparece na view de acordo com a lista de lembrete
			List<LembreteModel> lembretes = LembreteRepository.ListaLembretes;

			if (lembretes.Any())//se ela não estiver vazia é ordenada para que as datas sejam mostradas em ordem cronológica 
			{
				lembretes = lembretes.OrderBy(d => d.Data).ToList();
			}

			return View(lembretes);
		}

		public IActionResult Apagar(int id)
		{
			LembreteModel lembrete = LembreteRepository.ListaLembretes.Find(l => l.Id == id);
			if (lembrete != null)
			{
				LembreteRepository.ListaLembretes.Remove(lembrete);
			}

			return RedirectToAction("Index");
		}
	}
}
