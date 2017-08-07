using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Escola.Models
{
    public class Cidade
    {
        public int id { get; set; }

        public string Nome { get; set; }

 

        public List<Cidade> ListaClientes()

        {

            return new List<Cidade>

            {

                new Cidade { id = 1, Nome = "Londrina"},

                new Cidade { id = 2, Nome = "Campo Grande"},

                new Cidade { id = 3, Nome = "Maringa"}

            };

        }

    }
}