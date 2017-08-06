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
    public class DaoUsuario:IDao<Usuario>
    {
        private Iconnection _conexion;

        public DaoUsuario(Iconnection Conexion)
        {
            this._conexion = Conexion;
        }

        public IEnumerable<Usuario> All()
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "SELECT codigo, nome, cpf, sexo, telefone, data_cadastro, cidade_id,";
                query += "email, senha FROM usuario ORDER BY nome";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new Usuario()
                            {
                                codigo = reader.GetInt32(0),
                                nome = reader.IsDBNull(1) == false ? reader.GetString(1).ToString() : "",
                                cpf = reader.IsDBNull(2) == false ? reader.GetString(2).ToString() : "",
                                sexo = reader.IsDBNull(3) == false ? reader.GetString(3).ToString() : "",
                                telefone = reader.IsDBNull(4) == false ? reader.GetString(4).ToString() : "",
                                data_cadastro = reader.IsDBNull(6) == false ? DateTime.Parse(reader.GetDateTime(6).ToString()) : DateTime.MinValue,
                                cidade_id = reader.GetInt32(0),
                                email = reader.IsDBNull(7) == false ? reader.GetString(7).ToString() : "",
                                senha = reader.IsDBNull(8) == false ? reader.GetString(8).ToString() : "",
                            };
                        }
                    }
                }
            }
        }

        public bool Delete(Usuario model)
        {
            bool _ret = false;
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM usuario WHERE codigo=@codigo";
                cmd.Parameters.Add("@codigo", NpgsqlDbType.Varchar).Value = model.codigo;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    _ret = true;
                }
            }
            return _ret;
        }

        public Usuario FindOrDefault(params object[] keys)
        {
            Usuario entity = null;
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "SELECT codigo, nome, cpf, sexo, telefone, data_cadastro, cidade_id,";
                        query += "email, senha FROM usuario WHERE codigo=@codigo"; 

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;     
                cmd.Parameters.Add("@codigo", NpgsqlDbType.Varchar).Value = keys[0];
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        entity = new Usuario();
                        reader.Read();
                        entity.codigo = reader.GetInt32(0);
                        entity.nome = reader.IsDBNull(1) == false ? reader.GetString(1).ToString() : "";
                        entity.cpf = reader.IsDBNull(2) == false ? reader.GetString(2).ToString() : "";
                        entity.sexo = reader.IsDBNull(3) == false ? reader.GetString(3).ToString() : "";
                        entity.telefone = reader.IsDBNull(4) == false ? reader.GetString(4).ToString() : "";
                        entity.data_cadastro = reader.IsDBNull(6) == false ? DateTime.Parse(reader.GetDateTime(6).ToString()) : DateTime.MinValue;
                        entity.cidade_id = reader.GetInt32(0);
                        entity.email = reader.IsDBNull(7) == false ? reader.GetString(7).ToString() : "";
                        entity.senha = reader.IsDBNull(8) == false ? reader.GetString(8).ToString() : "";
                    }
                }
            }
            return entity;
        }

        public Usuario Insert(Usuario model)
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string query = "INSERT INTO usuario(nome, cpf, sexo, telefone, data_cadastro, cidade_id, email, senha ) ";
                query += "VALUES(@nome, @cpf, @sexo, @telefone, @data_cadastro, @cidade_id, @email, @senha)";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                
                cmd.Parameters.Add("@nome", NpgsqlDbType.Varchar).Value = model.nome;
                cmd.Parameters.Add("@cpf", NpgsqlDbType.Varchar).Value = model.cpf;
                cmd.Parameters.Add("@sexo", NpgsqlDbType.Varchar).Value = model.sexo;
                cmd.Parameters.Add("@telefone", NpgsqlDbType.Varchar).Value = model.telefone;
                cmd.Parameters.Add("@data_cadastro", NpgsqlDbType.Date).Value =(object) model.data_cadastro ?? DBNull.Value;
                cmd.Parameters.Add("@cidade_id", NpgsqlDbType.Integer).Value = model.cidade_id;
                cmd.Parameters.Add("@email", NpgsqlDbType.Varchar).Value = model.email;
                cmd.Parameters.Add("@senha", NpgsqlDbType.Varchar).Value = model.senha;
                cmd.ExecuteNonQuery();
            }
            return model;
        }

        public void Update(Usuario model)
        {
            using (NpgsqlCommand cmd = _conexion.Get().CreateCommand())
            {
                string  query = "UPDATE usuario SET ";
                        query += "nome=@nome, cpf=@cpf, sexo=@sexo, telefone=@telefone, data_cadastro=@data_cadastro,";
                        query += "cidade_id =@cidade_id, email=@email, senha=@senha";
                        query += " WHERE codigo+@codigo";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add("@codigo", NpgsqlDbType.Varchar).Value = model.codigo;
                cmd.Parameters.Add("@nome", NpgsqlDbType.Varchar).Value = model.nome;
                cmd.Parameters.Add("@cpf", NpgsqlDbType.Varchar).Value = model.cpf;
                cmd.Parameters.Add("@sexo", NpgsqlDbType.Varchar).Value = model.sexo;
                cmd.Parameters.Add("@telefone", NpgsqlDbType.Varchar).Value = model.telefone;
                cmd.Parameters.Add("@data_cadastro", NpgsqlDbType.Date).Value = (object)model.data_cadastro ?? DBNull.Value;
                cmd.Parameters.Add("@cidade_id", NpgsqlDbType.Integer).Value = model.cidade_id;
                cmd.Parameters.Add("@email", NpgsqlDbType.Varchar).Value = model.email;
                cmd.Parameters.Add("@senha", NpgsqlDbType.Varchar).Value = model.senha;
                cmd.ExecuteNonQuery();
            }
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
