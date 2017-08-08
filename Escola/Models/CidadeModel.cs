using Dados;
using Dados.DAO;
using Dados.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Escola.Models
{
    public  class CidadeModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EstadoId { get; set; }
        public string Cep { get; set; }

        public  List<CidadeModel> recuperarLista()
        {
            var ret = new List<CidadeModel>();

            using (Iconnection conexion = new Conexion())
            {
                IDao<Cidade> dao = new DaoCidade(conexion);
                Usuario usuario = new Usuario();

                foreach (Cidade entity in dao.All())
                {
                    ret.Add(new CidadeModel
                    {
                        Id = entity.id,
                        Nome = entity.nome,
                        EstadoId = entity.estado_id,
                        Cep = entity.cep,
                    });
                }
            }

            return ret;
        }

    }
}