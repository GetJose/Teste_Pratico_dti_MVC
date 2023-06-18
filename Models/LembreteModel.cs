namespace Teste_Pratico_dti_MVC.Models
{
	public class LembreteModel
	{
		//atributos do lembrete
		private int Id { get; set; }
		private string nome { get; set; }
		private DateTime data { get; set; }
		private static int cont;


		// Construtor padrão
		public LembreteModel()
		{
			cont = cont + 1;
			this.Id = cont;
		}

		// construtor para os lembretes com os campos nome e data 
		public LembreteModel(string nome, DateTime data)
		{
			cont = cont + 1;
			//verifica se os campos estão preenchidos corretamente, e só assim cria o lembrete
			if (!string.IsNullOrEmpty(nome)  && data > DateTime.Now)
			{
				this.nome = nome;
				this.data = data;
				this.Id = cont;
			}
			else
			{
				throw new ArgumentException("Os campos do lembrete não estão preenchidos corretamente.");
			}
		}
	}
}
