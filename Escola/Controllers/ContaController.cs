using Escola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Escola.Controllers
{
    public class ContaController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.CidadeId = new SelectList
           (
               new CidadeModel().recuperarLista(),
               "Id",
               "Nome"
               );
            ViewBag.DropSexoValues = new SelectList(
                new[] { "Masculino", "Feminino" });

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public ActionResult Login(LoginViewModel login, string returnUrl, string CidadeId)
        {
            ViewBag.ClienteId = new SelectList
                (
                    new CidadeModel().recuperarLista(),
                    "Id",
                    "Nome",
                    CidadeId
                );


            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var usuario = UsuarioModel.validarUsuario(login.Email, login.Senha);
            if (usuario != null)
            {
                FormsAuthentication.SetAuthCookie(usuario.Nome,false);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Login Invalido");
            }
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Conta");
        }
    }
}
