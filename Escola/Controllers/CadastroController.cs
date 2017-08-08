using Dados;
using Dados.DAO;
using Dados.Entities;
using Escola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Escola.Controllers
{
    public class CadastroController : Controller
    {
        #region Cadastro Usuario
        public ActionResult Usuario()
        {
           
            return View();
        }
            
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RecuperarUsuario(int id)
        {
            return Json(UsuarioModel.recuperarPeloId(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarUsuario(UsuarioModel model)
        {
            var resultado = "OK";
            var mensagems = new List<string>();
            var idSalvo = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagems = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    
                    var id = model.salvar();
                    if (id > 0)
                    {
                        idSalvo = id.ToString();
                    }
                    else
                    {
                        resultado = "ERRO";
                    }
                }
                catch (Exception)
                {

                    resultado = "ERRO";
                }

            }
            return Json(new { Resultado = resultado, Mensagems = mensagems, IdSalvo = idSalvo });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirUsuario(int id)
        {
            return Json(UsuarioModel.excluirPeloId(id));
        }
        #endregion


        [Authorize]
        public ActionResult Estado()
        {
            return View();
        }

        [Authorize]
        public ActionResult Cidade()
        {
            return View();
        }

        [Authorize]
        public ActionResult Bairro()
        {
            return View();
        }

        [Authorize]
        public ActionResult EnderecoAluno()
        {
            return View();
        }

        [Authorize]
        public ActionResult PaiAluno()
        {
            return View();
        }

        [Authorize]
        public ActionResult MaeAluno()
        {
            return View();
        }

        [Authorize]
        public ActionResult Aluno()
        {
            return View();
        }
    }
}