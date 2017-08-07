using Dados.Entities;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.DAO
{
    public class DaoBairro : IDao<Bairro>
    {
        private Iconnection _conexion;

        public DaoBairro(Iconnection Conexion)
        {
            this._conexion = Conexion;
        }

        public IEnumerable<Bairro> All()
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "SELECT id, nome, estado_id, cidade_id";
                query += " FROM bairro ORDER BY nome";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new Bairro()
                            {
                                id = reader.GetInt32(0),
                                nome = reader.IsDBNull(1) == false ? reader.GetString(1).ToString() : "",
                                estado_id = reader.GetInt32(2),
                                cidade_id = reader.GetInt32(3),
                            };
                        }
                    }
                }
            }
        }

        public bool Delete(Bairro model)
        {
            bool _ret = false;
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM bairro WHERE id=@id";
                cmd.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = model.id;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    _ret = true;
                }
            }
            return _ret;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Bairro FindOrDefault(params object[] keys)
        {
            Bairro entity = null;
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "SELECT id, nome, estado_id, cidade_id";
                query += " FROM bairro WHERE id=@id";

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = keys[0];
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        entity = new Bairro();
                        reader.Read();
                        entity.id = reader.GetInt32(0);
                        entity.nome = reader.IsDBNull(1) == false ? reader.GetString(1).ToString() : "";
                        entity.estado_id = reader.GetInt32(2);
                        entity.cidade_id = reader.GetInt32(3);
                    }
                }
            }
            return entity;
        }

        public Bairro FindOrDefaultParam(string param1, string param2)
        {
            throw new NotImplementedException();
        }

        public Bairro Insert(Bairro model)
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "INSERT INTO bairro(nome, estado_id, cidade_id) ";
                query += "VALUES(@nome, @estado_id, @cidade_id)";
                //query += "; SELECT CURRVAL('usuarioseq');";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;

                cmd.Parameters.Add("@nome", NpgsqlDbType.Varchar).Value = model.nome;
                cmd.Parameters.Add("@estado_id", NpgsqlDbType.Integer).Value = model.estado_id;
                cmd.Parameters.Add("@cidade_id", NpgsqlDbType.Integer).Value = model.cidade_id;

                cmd.ExecuteNonQuery();
            }
            return model;
        }

        public void Update(Bairro model)
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "UPDATE bairro SET ";
                query += "nome=@nome, estado_id=@estado_id, cidade_id=@cidade_id";
                query += " WHERE id+@id";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = model.id;
                cmd.Parameters.Add("@nome", NpgsqlDbType.Varchar).Value = model.nome;
                cmd.Parameters.Add("@estado_id", NpgsqlDbType.Integer).Value = model.estado_id;
                cmd.Parameters.Add("@cidade_id", NpgsqlDbType.Integer).Value = model.cidade_id;

                cmd.ExecuteNonQuery();
            }
        }
    }
}
