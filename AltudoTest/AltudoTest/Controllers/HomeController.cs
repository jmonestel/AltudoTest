using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AltudoTest.Models;
using AltudoTest.Aplicacao;

namespace AltudoTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetUrlData(string url)
        {
            var retorno = new DadosExtraidosViewModel();
            try
            {
                var aplicacao = new ExtracaoDados(url);

                if (aplicacao.ValidaRequisicao())
                {
                    retorno.Imagens = aplicacao.ObterImagens();
                    retorno.Palavras = aplicacao.ObterPalavrasMaisUsadas(3);
                } else
                {
                    retorno.MensagemErro = aplicacao.mensagemErro;
                }
            }
            catch (Exception ex)
            {
                retorno.MensagemErro = ex.Message;
            }

            return Json(retorno);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
