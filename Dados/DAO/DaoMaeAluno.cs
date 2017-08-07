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
    public class DaoMaeAluno : IDao<MaeAluno>
    {
        private Iconnection _conexion;

        public DaoMaeAluno(Iconnection Conexion)
        {
            this._conexion = Conexion;
        }

        public IEnumerable<MaeAluno> All()
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "SELECT id, nome, rg, cpf, profissao, celular, data_cadastro";
                query += " FROM mae_aluno ORDER BY nome";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new MaeAluno()
                            {
                                id = reader.GetInt32(0),
                                nome = reader.IsDBNull(1) == false ? reader.GetString(1).ToString() : "",
                                rg = reader.IsDBNull(2) == false ? reader.GetString(2).ToString() : "",
                                cpf = reader.IsDBNull(3) == false ? reader.GetString(3).ToString() : "",
                                profissao = reader.IsDBNull(4) == false ? reader.GetString(4).ToString() : "",
                                celular = reader.IsDBNull(5) == false ? reader.GetString(5).ToString() : "",
                                data_cadastro = reader.IsDBNull(6) == false ? DateTime.Parse(reader.GetDateTime(6).ToString()) : DateTime.MinValue,
                            };
                        }
                    }
                }
            }
        }

        public bool Delete(MaeAluno model)
        {
            bool _ret = false;
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM mae_aluno WHERE id=@id";
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

        public MaeAluno FindOrDefault(params object[] keys)
        {
            MaeAluno entity = null;
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "SELECT id, nome, rg, cpf, profissao, celular, data_cadastro";
                query += " FROM mae_aluno WHERE id=@id";

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = keys[0];
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        entity = new MaeAluno();
                        reader.Read();
                        entity.id = reader.GetInt32(0);
                        entity.nome = reader.IsDBNull(1) == false ? reader.GetString(1).ToString() : "";
                        entity.rg = reader.IsDBNull(2) == false ? reader.GetString(2).ToString() : "";
                        entity.cpf = reader.IsDBNull(3) == false ? reader.GetString(3).ToString() : "";
                        entity.profissao = reader.IsDBNull(4) == false ? reader.GetString(4).ToString() : "";
                        entity.celular = reader.IsDBNull(5) == false ? reader.GetString(5).ToString() : "";
                        entity.data_cadastro = reader.IsDBNull(6) == false ? DateTime.Parse(reader.GetDateTime(6).ToString()) : DateTime.MinValue;
                    }
                }
            }
            return entity;
        }

        public MaeAluno FindOrDefaultParam(string param1, string param2)
        {
            throw new NotImplementedException();
        }

        public MaeAluno Insert(MaeAluno model)
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "INSERT INTO mae_aluno(nome, rg, cpf, profissao, celular, data_cadastro) ";
                query += "VALUES(@nome, @rg, @cpf, @profissao, @celular, @data_cadastro)";
                //query += "; SELECT CURRVAL('usuarioseq');";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;

                cmd.Parameters.Add("@nome", NpgsqlDbType.Varchar).Value = model.nome;
                cmd.Parameters.Add("@rg", NpgsqlDbType.Varchar).Value = model.rg;
                cmd.Parameters.Add("@cpf", NpgsqlDbType.Varchar).Value = model.cpf;
                cmd.Parameters.Add("@profissao", NpgsqlDbType.Varchar).Value = model.profissao;
                cmd.Parameters.Add("@celular", NpgsqlDbType.Varchar).Value = model.celular;
                cmd.Parameters.Add("@data_cadastro", NpgsqlDbType.Date).Value = (object)model.data_cadastro ?? DBNull.Value;
                cmd.ExecuteNonQuery();
            }
            return model;
        }

        public void Update(MaeAluno model)
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "UPDATE mae_aluno SET ";
                query += "nome=@nome, rg=@rg, cpf=@cpf, profissao=@profissao, celular=@celular, data_cadastro=@data_cadastro";
                query += " WHERE id+@id";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = model.id;
                cmd.Parameters.Add("@nome", NpgsqlDbType.Varchar).Value = model.nome;
                cmd.Parameters.Add("@rg", NpgsqlDbType.Varchar).Value = model.rg;
                cmd.Parameters.Add("@cpf", NpgsqlDbType.Varchar).Value = model.cpf;
                cmd.Parameters.Add("@profissao", NpgsqlDbType.Varchar).Value = model.profissao;
                cmd.Parameters.Add("@celular", NpgsqlDbType.Varchar).Value = model.celular;
                cmd.Parameters.Add("@data_cadastro", NpgsqlDbType.Date).Value = (object)model.data_cadastro ?? DBNull.Value;

                cmd.ExecuteNonQuery();
            }
        }
    }
}
