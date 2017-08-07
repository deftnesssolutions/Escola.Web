using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Entities
{
    public class Aluno
    {
        public int codigo { get; set; }
        public string nome { get; set; }
        public string rg { get; set; }
        public string cpf { get; set; }
        public DateTime data_nacimento { get; set; }
        public int endereco_id { get; set; }
        public string matricula { get; set; }
        public int idade { get; set; }
        public string sexo { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public DateTime data_cadastro { get; set; }
        public int pai_id { get; set; }
        public int mae_id { get; set; }
    }
}
