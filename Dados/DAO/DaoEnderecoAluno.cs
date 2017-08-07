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
    public class DaoEnderecoAluno : IDao<EnderecoAluno>
    {
        private Iconnection _conexion;

        public DaoEnderecoAluno(Iconnection Conexion)
        {
            this._conexion = Conexion;
        }

        public IEnumerable<EnderecoAluno> All()
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "id, estado_id, cidade_id, bairro_id, rua, rua_nro, cep, aluno_codigo";
                query += " FROM endereco_aluno ORDER BY id";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new EnderecoAluno()
                            {
                                id = reader.GetInt32(0),
                                estado_id = reader.GetInt32(1),
                                cidade_id = reader.GetInt32(2),
                                bairro_id = reader.GetInt32(3),
                                rua = reader.IsDBNull(4) == false ? reader.GetString(4).ToString() : "",
                                rua_nro = reader.IsDBNull(5) == false ? reader.GetString(5).ToString() : "",
                                cep = reader.IsDBNull(6) == false ? reader.GetString(6).ToString() : "",
                                aluno_codigo = reader.GetInt32(7),
                            };
                        }
                    }
                }
            }
        }

        public bool Delete(EnderecoAluno model)
        {
            bool _ret = false;
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM endereco_aluno WHERE id=@id";
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

        public EnderecoAluno FindOrDefault(params object[] keys)
        {
            EnderecoAluno entity = null;
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "SELECT id, estado_id, cidade_id, bairro_id, rua, rua_nro, cep, aluno_codigo";
                query += " FROM endereco_aluno WHERE id=@id";

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = keys[0];
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        entity = new EnderecoAluno();
                        reader.Read();
                        entity.id = reader.GetInt32(0);
                        entity.estado_id = reader.GetInt32(1);
                        entity.cidade_id = reader.GetInt32(2);
                        entity.bairro_id = reader.GetInt32(3);
                        entity.rua = reader.IsDBNull(4) == false ? reader.GetString(4).ToString() : "";
                        entity.rua_nro = reader.IsDBNull(5) == false ? reader.GetString(5).ToString() : "";
                        entity.cep = reader.IsDBNull(6) == false ? reader.GetString(6).ToString() : "";
                        entity.aluno_codigo = reader.GetInt32(7);
                    }
                }
            }
            return entity;
        }

        public EnderecoAluno FindOrDefaultParam(string param1, string param2)
        {
            throw new NotImplementedException();
        }

        public EnderecoAluno Insert(EnderecoAluno model)
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "INSERT INTO endereco_aluno(estado_id, cidade_id, bairro_id, rua, rua_nro, cep, aluno_codigo) ";
                query += "VALUES(@estado_id, @cidade_id, @bairro_id, @rua, @rua_nro, @cep, @aluno_codigo)";
                //query += "; SELECT CURRVAL('usuarioseq');";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;

                cmd.Parameters.Add("@estado_id", NpgsqlDbType.Integer).Value = model.estado_id;
                cmd.Parameters.Add("@cidade_id", NpgsqlDbType.Integer).Value = model.cidade_id;
                cmd.Parameters.Add("@bairro_id", NpgsqlDbType.Integer).Value = model.bairro_id;
                cmd.Parameters.Add("@rua", NpgsqlDbType.Varchar).Value = model.rua;
                cmd.Parameters.Add("@rua_nro", NpgsqlDbType.Varchar).Value = model.rua_nro;
                cmd.Parameters.Add("@cep", NpgsqlDbType.Varchar).Value = model.cep;
                cmd.Parameters.Add("@aluno_codigo", NpgsqlDbType.Integer).Value = model.aluno_codigo;

                cmd.ExecuteNonQuery();
            }
            return model;
        }

        public void Update(EnderecoAluno model)
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "UPDATE endereco_aluno SET ";
                query += " estado_id=@estado_id, cidade_id=@cidade_id, bairro_id=@bairro_id, rua=@rua, rua_nro=@rua_nro, cep=@cep, aluno_codigo=@aluno_codigo";
                query += " WHERE id+@id";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add("@estado_id", NpgsqlDbType.Integer).Value = model.estado_id;
                cmd.Parameters.Add("@cidade_id", NpgsqlDbType.Integer).Value = model.cidade_id;
                cmd.Parameters.Add("@bairro_id", NpgsqlDbType.Integer).Value = model.bairro_id;
                cmd.Parameters.Add("@rua", NpgsqlDbType.Varchar).Value = model.rua;
                cmd.Parameters.Add("@rua_nro", NpgsqlDbType.Varchar).Value = model.rua_nro;
                cmd.Parameters.Add("@cep", NpgsqlDbType.Varchar).Value = model.cep;
                cmd.Parameters.Add("@aluno_codigo", NpgsqlDbType.Integer).Value = model.aluno_codigo;

                cmd.ExecuteNonQuery();
            }
        }
    }
}
