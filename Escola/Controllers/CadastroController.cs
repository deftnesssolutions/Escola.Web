using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Escola.Controllers
{
    public class CadastroController : Controller
    {
        
        public ActionResult Usuario()
        {
            ViewBag.DropSexoValues = new SelectList(new[] { "Masculino", "Feminino" });
            return View();
        }
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