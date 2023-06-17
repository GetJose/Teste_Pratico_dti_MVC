namespace Teste_Pratico_dti_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

        public class ListaLembreteModel
        {
        public Dictionary<DateTime, List<LembreteModel>> LembretesPorData { get; set; }

        public ListaLembreteModel()
            {
            LembretesPorData = new Dictionary<DateTime, List<LembreteModel>>();
            }

            public void AdicionarLembrete(LembreteModel lembrete)
            {
                DateTime data = lembrete.Data;

                if (LembretesPorData.ContainsKey(data))
                {
                LembretesPorData[data].Add(lembrete);
                }
                else
                {
                LembretesPorData[data] = new List<LembreteModel> { lembrete };
                }
            }

            public List<LembreteModel> ExibirLembretes()
            {
                var lembretesOrdenados = new List<LembreteModel>();

                var datasOrdenadas = LembretesPorData.Keys.OrderBy(d => d);

                foreach (var data in datasOrdenadas)
                {
                    var lembretesData = LembretesPorData[data];
                    lembretesOrdenados.AddRange(lembretesData);
                }

                return lembretesOrdenados;
            }
        
        public bool RemoverLembrete(string nome)
        {
            foreach (var lembretesData in LembretesPorData.Values)
            {
                var lembreteRemover = lembretesData.FirstOrDefault(l => l.Nome == nome);
                if (lembreteRemover != null)
                {
                    lembretesData.Remove(lembreteRemover);

                    if (lembretesData.Count == 0)
                    {
                        var dataRemover = LembretesPorData.FirstOrDefault(d => d.Value == lembretesData).Key;
                        LembretesPorData.Remove(dataRemover);
                    }

                    return true; // Indica que o lembrete foi removido com sucesso
                }
            }

            return false; // Indica que o lembrete não foi encontrado
        }
    }
    }
