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
    public class DaoEstado : IDao<Estado>
    {
        private Iconnection _conexion;

        public DaoEstado(Iconnection Conexion)
        {
            this._conexion = Conexion;
        }

        public IEnumerable<Estado> All()
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "SELECT id, nome, uf";
                query += " FROM estado ORDER BY nome";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new Estado()
                            {
                                id = reader.GetInt32(0),
                                nome = reader.IsDBNull(1) == false ? reader.GetString(1).ToString() : "",
                                uf = reader.IsDBNull(2) == false ? reader.GetString(2).ToString() : "",
                            };
                        }
                    }
                }
            }
        }

        public bool Delete(Estado model)
        {
            bool _ret = false;
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM estado WHERE id=@id";
                cmd.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = model.id;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    _ret = true;
                }
            }
            return _ret;
        }

        public Estado FindOrDefault(params object[] keys)
        {
            Estado entity = null;
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "SELECT id, nome, uf";
                query += " FROM estado WHERE id=@id";

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = keys[0];
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        entity = new Estado();
                        reader.Read();
                        entity.id = reader.GetInt32(0);
                        entity.nome = reader.IsDBNull(1) == false ? reader.GetString(1).ToString() : "";
                        entity.uf = reader.IsDBNull(2) == false ? reader.GetString(2).ToString() : "";
                        
                    }
                }
            }
            return entity;
        }

        public Estado FindOrDefaultParam(string param1, string param2)
        {
            throw new NotImplementedException();
        }

        public Estado Insert(Estado model)
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "INSERT INTO estado(nome, uf) ";
                query += "VALUES(@nome, @uf)";
                //query += "; SELECT CURRVAL('usuarioseq');";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;

                cmd.Parameters.Add("@nome", NpgsqlDbType.Varchar).Value = model.nome;
                cmd.Parameters.Add("@uf", NpgsqlDbType.Varchar).Value = model.uf;
                
                cmd.ExecuteNonQuery();
            }
            return model;
        }

        public void Update(Estado model)
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "UPDATE estado SET ";
                query += "nome=@nome, uf=@uf";
                query += " WHERE id+@id";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = model.id;
                cmd.Parameters.Add("@nome", NpgsqlDbType.Varchar).Value = model.nome;
                cmd.Parameters.Add("@uf", NpgsqlDbType.Varchar).Value = model.uf;
                
                cmd.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
