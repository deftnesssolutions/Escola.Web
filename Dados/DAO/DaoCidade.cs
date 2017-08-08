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
    public class DaoCidade : IDao<Cidade>
    {
        private Iconnection _conexion;

        public DaoCidade(Iconnection Conexion)
        {
            this._conexion = Conexion;
        }

        public IEnumerable<Cidade> All()
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "SELECT id, nome, estado_id, cep";
                query += " FROM cidade ORDER BY nome";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new Cidade()
                            {
                                id = reader.GetInt32(0),
                                nome = reader.IsDBNull(1) == false ? reader.GetString(1).ToString() : "",
                                estado_id = reader.GetInt32(2),
                                cep = reader.IsDBNull(3) == false ? reader.GetString(3).ToString() : "",
                            };
                        }
                    }
                }
            }
        }

        public bool Delete(Cidade model)
        {
            bool _ret = false;
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM cidade WHERE id=@id";
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

        public Cidade FindOrDefault(params object[] keys)
        {
            Cidade entity = null;
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "SELECT id, nome, estado_id, cep";
                query += " FROM cidade WHERE id=@id";

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = keys[0];
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        entity = new Cidade();
                        reader.Read();
                        entity.id = reader.GetInt32(0);
                        entity.nome = reader.IsDBNull(1) == false ? reader.GetString(1).ToString() : "";
                        entity.estado_id = reader.GetInt32(2);
                        entity.cep = reader.IsDBNull(3) == false ? reader.GetString(3).ToString() : "";

                    }
                }
            }
            return entity;
        }

        public Cidade FindOrDefaultParam(string param1, string param2)
        {
            throw new NotImplementedException();
        }

        public Cidade Insert(Cidade model)
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "INSERT INTO cidade(nome, estado_id, cep) ";
                query += "VALUES(@nome, @estado_id, @cep)";
                //query += "; SELECT CURRVAL('usuarioseq');";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;

                cmd.Parameters.Add("@nome", NpgsqlDbType.Varchar).Value = model.nome;
                cmd.Parameters.Add("@estado_id", NpgsqlDbType.Integer).Value = model.estado_id;
                cmd.Parameters.Add("@cep", NpgsqlDbType.Varchar).Value = model.nome;

                cmd.ExecuteNonQuery();
            }
            return model;
        }

        public void Update(Cidade model)
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "UPDATE cidade SET ";
                query += "nome=@nome, estado_id=@estado_id, cep=@cep";
                query += " WHERE id+@id";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add("@nome", NpgsqlDbType.Varchar).Value = model.nome;
                cmd.Parameters.Add("@estado_id", NpgsqlDbType.Integer).Value = model.estado_id;
                cmd.Parameters.Add("@cep", NpgsqlDbType.Varchar).Value = model.nome;

                cmd.ExecuteNonQuery();
            }
        }
    }
}
