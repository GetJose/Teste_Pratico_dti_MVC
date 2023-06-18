namespace Teste_Pratico_dti_MVC.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class ListaLembreteModel
	{
		//Cria um dicionario para salvar os lembretes 
		public Dictionary<DateTime, List<LembreteModel>> ListaLembretes { get; set; }

		//construtor
		public ListaLembreteModel()
		{
			ListaLembretes = new Dictionary<DateTime, List<LembreteModel>>();
		}
		//Função para adicionar o lembrete Lista de lembretes
		public void AdicionarLembrete(LembreteModel lembrete)
		{
			//Salva a data do lembrete em um aux
			DateTime data = lembrete.Data;
			//se no dicionario já contem aquela data, apenas adiciona o lembrete a ela 
			if (ListaLembretes.ContainsKey(data))
			{
				ListaLembretes[data].Add(lembrete);
			}
			else
			{
				//se não uma nova lista para aquela data é adicionada
				ListaLembretes[data] = new List<LembreteModel> { lembrete };
			}
		}

		//Função para remover os lembretes pelo nome dele 
		public void RemoverLembrete(int id)
		{
			foreach (var lembretesData in ListaLembretes.Values)
			{
				var lembreteRemover = lembretesData.FirstOrDefault(l => l.Id == id);
				if (lembreteRemover != null)
				{
					lembretesData.Remove(lembreteRemover);
					if (lembretesData.Count == 0)
					{
						var dataRemover = ListaLembretes.FirstOrDefault(d => d.Value == lembretesData).Key;
						ListaLembretes.Remove(dataRemover);
					}
					break;
				}
			}
		}

	}
}
