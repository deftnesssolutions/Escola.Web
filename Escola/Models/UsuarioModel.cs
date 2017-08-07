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

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        [Display(Name = "Nome Completo:")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o Cpf")]
        [Display(Name = "CPF:")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Informe o Sexo")]
        [Display(Name = "Sexo:")]
        public string Sexo { get; set; }

        [Display(Name = "Telefone:")]
        public string Telefone { get; set; }

        /*[Display(Name = "Data Cadastro:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Editable(false)]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime data_cadastro { get; set; }*/
        private DateTime _data_cadastro = DateTime.MinValue;
        //public DateTime CreatedOn;
        [Display(Name = "Data Cadastro:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Data_cadastro
        {
            get
            {
                return (_data_cadastro == DateTime.MinValue) ? DateTime.Now : _data_cadastro;
            }
            set { _data_cadastro = value; }
        }

        [Required(ErrorMessage = "Informe a Cidade")]
        [Display(Name = "Cidade:")]
        public int Cidade_id { get; set; }

        [Required(ErrorMessage = "Informe seu e-mail como Login")]
        [Display(Name = "Login:")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha:")]
        public string Senha { get; set; }

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
                        Codigo = entity.codigo,
                        Nome = entity.nome,
                        Cpf = entity.cpf,
                        Sexo = entity.sexo,
                        Telefone = entity.telefone,
                        Data_cadastro = entity.data_cadastro,
                        Cidade_id = entity.cidade_id,
                        Email = entity.email,
                        Senha = entity.senha
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

                foreach (Usuario entity in dao.All())
                {
                    ret.Add(new UsuarioModel
                    {
                        Codigo = entity.codigo,
                        Nome = entity.nome,
                        Cpf = entity.cpf,
                        Sexo = entity.sexo,
                        Telefone = entity.telefone,
                        Data_cadastro = entity.data_cadastro,
                        Cidade_id = entity.cidade_id,
                        Email = entity.email,
                        Senha = entity.senha
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
                        Codigo = entity.codigo,
                        Nome = entity.nome,
                        Cpf = entity.cpf,
                        Sexo = entity.sexo,
                        Telefone = entity.telefone,
                        Data_cadastro = entity.data_cadastro,
                        Cidade_id = entity.cidade_id,
                        Email = entity.email,
                        Senha = entity.senha
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
            var model = recuperarPeloId(Codigo);

            if (model == null)
            {
                using (Iconnection Conexion = new Conexion())
                {
                    IDao<Usuario> dao = new DaoUsuario(Conexion);
                    Usuario entity = new Usuario();//Objeto tipo Modulos(tabela)
                    entity.nome = Nome;
                    entity.cpf = Cpf;
                    entity.sexo = Sexo;
                    entity.telefone = Telefone;
                    entity.data_cadastro = Data_cadastro;
                    entity.cidade_id = Cidade_id;
                    entity.email = Email;
                    entity.senha = CriptoHelper.HashMD5(Senha);
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
                    entity.nome = Nome;
                    entity.cpf = Cpf;
                    entity.sexo = Sexo;
                    entity.telefone = Telefone;
                    entity.data_cadastro = Data_cadastro;
                    entity.cidade_id = Cidade_id;
                    entity.email = Email;
                    if (!string.IsNullOrEmpty(this.Senha))
                    {
                        entity.senha = CriptoHelper.HashMD5(Senha);
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