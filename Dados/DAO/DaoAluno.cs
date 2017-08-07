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
    public class DaoAluno : IDao<Aluno>
    {
        private Iconnection _conexion;

        public DaoAluno(Iconnection Conexion)
        {
            this._conexion = Conexion;
        }

        public IEnumerable<Aluno> All()
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "codigo, nome, rg, cpf, data_nacimento, endereco_id, matricula, ";
                       query+= "idade, sexo, email, telefone, data_cadastro, pai_id, mae_id";
                query += " FROM aluno ORDER BY id";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new Aluno()
                            {
                                codigo = reader.GetInt32(0),
                                nome = reader.IsDBNull(1) == false ? reader.GetString(1).ToString() : "",
                                rg = reader.IsDBNull(2) == false ? reader.GetString(2).ToString() : "",
                                cpf = reader.IsDBNull(3) == false ? reader.GetString(3).ToString() : "",
                                data_nacimento = reader.IsDBNull(4) == false ? DateTime.Parse(reader.GetDateTime(4).ToString()) : DateTime.MinValue,
                                endereco_id = reader.GetInt32(5),
                                matricula = reader.IsDBNull(6) == false ? reader.GetString(6).ToString() : "",
                                idade = reader.GetInt32(7),
                                sexo = reader.IsDBNull(8) == false ? reader.GetString(8).ToString() : "",
                                email = reader.IsDBNull(9) == false ? reader.GetString(9).ToString() : "",
                                telefone = reader.IsDBNull(10) == false ? reader.GetString(10).ToString() : "",
                                data_cadastro = reader.IsDBNull(11) == false ? DateTime.Parse(reader.GetDateTime(11).ToString()) : DateTime.MinValue,
                                pai_id = reader.GetInt32(12),
                                mae_id = reader.GetInt32(13),
                            };
                        }
                    }
                }
            }
        }

        public bool Delete(Aluno model)
        {
            bool _ret = false;
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM aluno WHERE codigo=@codigo";
                cmd.Parameters.Add("@codigo", NpgsqlDbType.Varchar).Value = model.codigo;
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

        public Aluno FindOrDefault(params object[] keys)
        {
            Aluno entity = null;
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "codigo, nome, rg, cpf, data_nacimento, endereco_id, matricula, ";
                query += "idade, sexo, email, telefone, data_cadastro, pai_id, mae_id";
                query += " FROM aluno ORDER BY id";

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = keys[0];
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        entity = new Aluno();
                        reader.Read();
                        entity.codigo = reader.GetInt32(0);
                        entity.nome = reader.IsDBNull(1) == false ? reader.GetString(1).ToString() : "";
                        entity.rg = reader.IsDBNull(2) == false ? reader.GetString(2).ToString() : "";
                        entity.cpf = reader.IsDBNull(3) == false ? reader.GetString(3).ToString() : "";
                        entity.data_nacimento = reader.IsDBNull(4) == false ? DateTime.Parse(reader.GetDateTime(4).ToString()) : DateTime.MinValue;
                        entity.endereco_id = reader.GetInt32(5);
                        entity.matricula = reader.IsDBNull(6) == false ? reader.GetString(6).ToString() : "";
                        entity.idade = reader.GetInt32(7);
                        entity.sexo = reader.IsDBNull(8) == false ? reader.GetString(8).ToString() : "";
                        entity.email = reader.IsDBNull(9) == false ? reader.GetString(9).ToString() : "";
                        entity.telefone = reader.IsDBNull(10) == false ? reader.GetString(10).ToString() : "";
                        entity.data_cadastro = reader.IsDBNull(11) == false ? DateTime.Parse(reader.GetDateTime(11).ToString()) : DateTime.MinValue;
                        entity.pai_id = reader.GetInt32(12);
                        entity.mae_id = reader.GetInt32(13);
                    }
                }
            }
            return entity;
        }

        public Aluno FindOrDefaultParam(string param1, string param2)
        {
            throw new NotImplementedException();
        }

        public Aluno Insert(Aluno model)
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = " INSERT INTO aluno(nome, rg, cpf, data_nacimento, endereco_id, matricula, ";
                query += "idade, sexo, email, telefone, data_cadastro, pai_id, mae_id )";
                query += "VALUES(@nome, @rg, @cpf, @data_nacimento, @endereco_id, @matricula, ";
                query = "@idade, @sexo, @email, @telefone, @data_cadastro, @pai_id, @mae_id) ";
               
                //query += "; SELECT CURRVAL('usuarioseq');";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;

                cmd.Parameters.Add("@nome", NpgsqlDbType.Varchar).Value = model.nome;
                cmd.Parameters.Add("@rg", NpgsqlDbType.Varchar).Value = model.rg;
                cmd.Parameters.Add("@cpf", NpgsqlDbType.Varchar).Value = model.cpf;
                cmd.Parameters.Add("@data_nacimento", NpgsqlDbType.Date).Value = (object)model.data_cadastro ?? DBNull.Value;
                cmd.Parameters.Add("@endereco_id", NpgsqlDbType.Integer).Value = model.endereco_id;
                cmd.Parameters.Add("@matricula", NpgsqlDbType.Varchar).Value = model.matricula;
                cmd.Parameters.Add("@idade", NpgsqlDbType.Integer).Value = model.idade;
                cmd.Parameters.Add("@sexo", NpgsqlDbType.Varchar).Value = model.sexo;
                cmd.Parameters.Add("@email", NpgsqlDbType.Varchar).Value = model.email;
                cmd.Parameters.Add("@telefone", NpgsqlDbType.Varchar).Value = model.telefone;
                cmd.Parameters.Add("@data_cadastro", NpgsqlDbType.Date).Value = (object)model.data_cadastro ?? DBNull.Value;
                cmd.Parameters.Add("@pai_id", NpgsqlDbType.Integer).Value = model.pai_id;
                cmd.Parameters.Add("@mae_id", NpgsqlDbType.Integer).Value = model.mae_id;

                cmd.ExecuteNonQuery();
            }
            return model;
        }

        public void Update(Aluno model)
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "UPDATE aluno SET ";
                query += "nome=@nome, rg=@rg, cpf=@cpf, data_nacimento=@data_nacimento, endereco_id=@endereco_id, matricula=@matricula, ";
                query = "idade=@idade, sexo=@sexo, email=@email, telefone=@telefone, data_cadastro=@data_cadastro, pai_id=@pai_id, mae_id=@mae_id ";
                query += " WHERE id+@id";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;

                cmd.Parameters.Add("@nome", NpgsqlDbType.Varchar).Value = model.nome;
                cmd.Parameters.Add("@rg", NpgsqlDbType.Varchar).Value = model.rg;
                cmd.Parameters.Add("@cpf", NpgsqlDbType.Varchar).Value = model.cpf;
                cmd.Parameters.Add("@data_nacimento", NpgsqlDbType.Date).Value = (object)model.data_cadastro ?? DBNull.Value;
                cmd.Parameters.Add("@endereco_id", NpgsqlDbType.Integer).Value = model.endereco_id;
                cmd.Parameters.Add("@matricula", NpgsqlDbType.Varchar).Value = model.matricula;
                cmd.Parameters.Add("@idade", NpgsqlDbType.Integer).Value = model.idade;
                cmd.Parameters.Add("@sexo", NpgsqlDbType.Varchar).Value = model.sexo;
                cmd.Parameters.Add("@email", NpgsqlDbType.Varchar).Value = model.email;
                cmd.Parameters.Add("@telefone", NpgsqlDbType.Varchar).Value = model.telefone;
                cmd.Parameters.Add("@data_cadastro", NpgsqlDbType.Date).Value = (object)model.data_cadastro ?? DBNull.Value;
                cmd.Parameters.Add("@pai_id", NpgsqlDbType.Integer).Value = model.pai_id;
                cmd.Parameters.Add("@mae_id", NpgsqlDbType.Integer).Value = model.mae_id;

                cmd.ExecuteNonQuery();
            }
        }
    }
}
