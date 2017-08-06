using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Escola.Models
{
    public class UsuarioModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Informe o nome")]
        public string nome { get; set; }
        [Required(ErrorMessage = "Informe o Login")]
        public string login { get; set; }
        [Required(ErrorMessage = "Informe a nome")]
        public string senha { get; set; }

        public static UsuarioModel validarUsuario(string login, string senha)
        {
            UsuarioModel ret = null;
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("SELECT * FROM usuario WHERE login=@login AND senha=@senha",
                        login, CriptoHelper.HashMD5(senha));
                    comando.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
                    comando.Parameters.Add("@senha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(senha);

                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new UsuarioModel
                        {
                            id = (int)reader["id"],
                            nome = (string)reader["nome"],
                            login = (string)reader["login"],
                            senha = (string)reader["senha"]
                        };
                    }
                }
            }
            return ret;
        }

        public static List<UsuarioModel> recuperarLista()
        {
            var ret = new List<UsuarioModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "SELECT * FROM usuario ORDER BY id";
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new UsuarioModel
                        {
                            id = (int)reader["id"],
                            nome = (string)reader["nome"],
                            login = (string)reader["login"],
                            senha = (string)reader["senha"]
                        });
                    }

                }
            }
            return ret;
        }

        public static UsuarioModel recuperarPeloId(int id)
        {
            UsuarioModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("SELECT * FROM usuario WHERE id=@id");
                    comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new UsuarioModel
                        {
                            id = (int)reader["id"],
                            nome = (string)reader["nome"],
                            login = (string)reader["login"],
                            senha = (string)reader["senha"]
                        };
                    }

                }
            }
            return ret;
        }

        public static bool excluirPeloId(int id)
        {
            var ret = false;
            if (recuperarPeloId(id) != null)
            {
                using (var conexao = new SqlConnection())
                {
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                    conexao.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "DELETE FROM usuario WHERE id= @id";
                        comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        ret = (comando.ExecuteNonQuery()) > 0;
                    }
                }
            }
            return ret;
        }

        public int salvar()
        {
            var ret = 0;
            var model = recuperarPeloId(id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    if (model == null)
                    {
                        comando.CommandText = "INSERT INTO usuario (nome,login,senha) VALUES(@nome,@login,@senha); SELECT convert(int, SCOPE_IDENTITY())";
                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = this.nome;
                        comando.Parameters.Add("@login", SqlDbType.VarChar).Value = this.login;
                        comando.Parameters.Add("@senha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(this.senha);
                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText =
                            "UPDATE usuario SET nome = @nome, login = @login" +
                           (!string.IsNullOrEmpty(this.senha) ? ", senha= @senha" : "") +
                           " WHERE id=@id ";
                        comando.Parameters.Add("@id", SqlDbType.Int).Value = this.id;
                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = this.nome;
                        comando.Parameters.Add("@login", SqlDbType.VarChar).Value = this.login;
                        if (!string.IsNullOrEmpty(this.senha))
                        {
                            comando.Parameters.Add("@senha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(this.senha);
                        }
                        if (comando.ExecuteNonQuery() > 0)
                        {
                            ret = this.id;
                        }
                    }
                }
            }
            return ret;
        }
    }
}