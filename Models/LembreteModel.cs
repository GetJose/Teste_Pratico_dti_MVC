namespace Teste_Pratico_dti_MVC.Models
{
    public class LembreteModel
    {
            //atributos do lembrete
            private string nome;
            private DateTime data;

            public string Nome
            {
                get { return nome; }
                set
                {
                    if (!string.IsNullOrEmpty(value))
                        nome = value;
                    else
                        throw new ArgumentException("O campo do nome não pode estar vazio.");
                }
            }

            public DateTime Data
            {
                get { return data; }
                set
                {
                    if (TesteDataFutura(value))
                        data = value;
                    else
                        throw new ArgumentException("A data informada já passou.");
                }
            }

            // Verifica se a data está no futuro
            private bool TesteDataFutura(DateTime data)
            {
                return data > DateTime.Now;
            }

            // Construtor padrão
            public LembreteModel() { }
            
            // construtor para os lembretes com os campos nome e data 
            public LembreteModel(string nome, DateTime data)
            {   
                //verifica se os campos estão preenchidos corretamente, e só assim cria o lembrete
                if (!string.IsNullOrEmpty(nome) && TesteDataFutura(data))
                {
                    this.nome = nome;
                    this.data = data;
                }
                else
                {
                    throw new ArgumentException("Os campos do lembrete não estão preenchidos corretamente.");
                }
            }
        }
    }
