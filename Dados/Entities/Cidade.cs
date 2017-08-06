using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Entities
{
    public class Cidade
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int estado_id { get; set; }
        public string cep { get; set; }
    }
}
