using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Entities
{
    public class MaeAluno
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string rg { get; set; }
        public string cpf { get; set; }
        public string profissao { get; set; }
        public string celular { get; set; }
        public DateTime data_cadastro { get; set; }
    }
}