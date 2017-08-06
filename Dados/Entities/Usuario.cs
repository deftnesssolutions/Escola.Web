using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Entities
{
    public class Usuario
    {
        public int codigo { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string sexo { get; set; }
        public string telefone { get; set; }
        public DateTime data_cadastro { get; set; }
        public int cidade_id { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
    }
}
