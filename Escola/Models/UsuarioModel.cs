using Dados;
using Dados.DAO;
using Dados.Entities;
using Dados.Helpers;
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
        
        public int codigo { get; set; }
        [Required(ErrorMessage = "Informe o nome")]
        public string nome { get; set; }
        public string cpf { get; set; }
        public string sexo { get; set; }
        public string telefone { get; set; }
        public DateTime data_cadastro { get; set; }
        public int cidade_id { get; set; }
        [Required(ErrorMessage = "Informe o Login")]
        public string email { get; set; }
        [Required(ErrorMessage = "Informe a senha")]
        public string senha { get; set; }

        public static UsuarioModel validarUsuario(string login, string senha)
        {
            UsuarioModel ret = null;
            using (Iconnection Conexion = new Conexion())
            {
                IDao<Usuario> dao = new DaoUsuario(Conexion);
                Usuario entity = dao.FindOrDefaultParam(login, CriptoHelper.HashMD5(senha));
                if (entity != null)
                {
                    ret = new UsuarioModel
                    {
                        codigo = entity.codigo,
                        nome = entity.nome,
                        cpf = entity.cpf,
                        sexo = entity.sexo,
                        telefone = entity.telefone,
                        data_cadastro = entity.data_cadastro,
                        cidade_id = entity.cidade_id,
                        email = entity.email,
                        senha = entity.senha
                    };
                }
                
            }
            return ret;
        }

        public static List<UsuarioModel> recuperarLista()
        {
            var ret = new List<UsuarioModel>();

            using (Iconnection conexion = new Conexion())
            {
                IDao<Usuario> dao = new DaoUsuario(conexion);
                Usuario usuario = new Usuario();

                foreach (Usuario u in dao.All())
                {
                    ret.Add(new UsuarioModel
                    {
                        codigo = u.codigo,
                        nome = u.nome,
                        cpf = u.cpf,
                        sexo = u.sexo,
                        telefone = u.telefone,
                        data_cadastro = u.data_cadastro,
                        cidade_id = u.cidade_id,
                        email = u.email,
                        senha = u.senha
                    });
                }
            }

            return ret;
        }

        public static UsuarioModel recuperarPeloId(int id)
        {
            UsuarioModel ret = null;

            using (Iconnection Conexion = new Conexion())
            {
                IDao<Usuario> dao = new DaoUsuario(Conexion);
                Usuario entity = dao.FindOrDefault(id);
                if(entity !=null)
                {
                    ret = new UsuarioModel
                    {
                        codigo = entity.codigo,
                        nome = entity.nome,
                        cpf = entity.cpf,
                        sexo = entity.sexo,
                        telefone = entity.telefone,
                        data_cadastro = entity.data_cadastro,
                        cidade_id = entity.cidade_id,
                        email = entity.email,
                        senha = entity.senha
                    };
                }
            }
            return ret;
        }

        public static bool excluirPeloId(int id)
        {
            var ret = false;
            using (Iconnection Conexion = new Conexion())
            {
                IDao<Usuario> dao = new DaoUsuario(Conexion);
                Usuario entity = dao.FindOrDefault(id);
                 ret = dao.Delete(entity);
            }
            return ret;
        }

        public int salvar()
        {
            var ret = 0;
            var model = recuperarPeloId(codigo);

            if (model == null)
            {
                using (Iconnection Conexion = new Conexion())
                {
                    IDao<Usuario> dao = new DaoUsuario(Conexion);
                    Usuario entity = new Usuario();//Objeto tipo Modulos(tabela)
                    entity.nome = nome;
                    entity.cpf = cpf;
                    entity.sexo = sexo;
                    entity.telefone = telefone;
                    entity.data_cadastro = data_cadastro;
                    entity.cidade_id = cidade_id;
                    entity.email = email;
                    entity.senha = CriptoHelper.HashMD5(senha);
                    // gravo los datos como registro en la tabla modulos
                    dao.Insert(entity);
                    ret = entity.codigo;
                }
            }
            else
            {
                using (Iconnection Conexion = new Conexion())
                {
                    IDao<Usuario> dao = new DaoUsuario(Conexion);
                    Usuario entity = new Usuario();//Objeto tipo Modulos(tabela)
                    entity.nome = nome;
                    entity.cpf = cpf;
                    entity.sexo = sexo;
                    entity.telefone = telefone;
                    entity.data_cadastro = data_cadastro;
                    entity.cidade_id = cidade_id;
                    entity.email = email;
                    if (!string.IsNullOrEmpty(this.senha))
                    {
                        entity.senha = CriptoHelper.HashMD5(senha);
                    }
                    
                    // gravo los datos como registro en la tabla modulos
                    dao.Insert(entity);
                    ret = entity.codigo;
                }
            }

            return ret;
        }
    }
}