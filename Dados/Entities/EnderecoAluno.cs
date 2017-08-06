using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Entities
{
    public class EnderecoAluno
    {
        public int id { get; set; }
        public int aluno_codigo { get; set; }
        public int estado_id { get; set; }
        public int cidade_id { get; set; }
        public int bairro_id { get; set; }
        public string rua { get; set; }
        public string rua_nro { get; set; }
        public string cep { get; set; }
        
    }
}
