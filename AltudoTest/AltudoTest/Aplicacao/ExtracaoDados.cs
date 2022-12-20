using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AltudoTest.Aplicacao
{
    public class ExtracaoDados
    {
        private string _url { get; set; }
        private Uri _uri { get; set; }
        private HtmlDocument _doc { get; set; }
        private string _htmlResult { get; set; }
        public string mensagemErro { get; private set; }

        public ExtracaoDados(string url)
        {
            _url = url?.Trim() ?? string.Empty;
        }

        private void AjustaUriParaRequisicao()
        {
            if (!_url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                _url = String.Format("{0}://{1}", "http", _url);
            }

            _uri = new Uri(_url);
        }

        public bool ValidaRequisicao()
        {
            if (String.IsNullOrEmpty(_url))
            {
                mensagemErro = "Endereço para extração não informado";
                return false;
            } else
            {
                try
                {
                    AjustaUriParaRequisicao();
                    _doc = FazRequisicao().Result;
                }
                catch (Exception)
                {
                    mensagemErro = String.Format("Endereço para extração inválido [{0}]", _url);
                    return false;
                }
            }
            return true;
        }

        private async Task<HtmlDocument> FazRequisicao()
        {
            HtmlWeb web = new HtmlWeb();
            //Juan teste
            return await web.LoadFromWebAsync(_uri, null, null);
        }

        private void RemoveNodesDesnecessarios()
        {
            //Removendo script e styles
            _doc.DocumentNode.SelectNodes("//noscript|//link|//style|//script")
                ?.ToList()
                ?.ForEach(n => n.Remove());
        }

        private void CarregarDocumento()
        {
            RemoveNodesDesnecessarios();
        }

        public List<string> ObterImagens()
        {
            CarregarDocumento();

            return _doc.DocumentNode.SelectNodes("//img")
                ?.Select(e => new Uri(_uri, e.GetAttributeValue("src", null)).AbsoluteUri)
                ?.Where(s => !String.IsNullOrEmpty(s)).ToList();
        }

        public Dictionary<string,int> ObterPalavrasMaisUsadas(int qtdCaracteresConsideraPalavra)
        {
            CarregarDocumento();

            try
            {
                var conteudo = _doc.DocumentNode.SelectNodes("//body//text()")
                    ?.Select(x => HtmlEntity.DeEntitize(x.InnerText).Trim())
                    ?.Where(x => !String.IsNullOrEmpty(x));

                char[] separadores = new[] { ' ', '.', ',', '\'', '-', '_', '"', '(', ')', '{', '}', '[', ']', '^' };
                
                return conteudo
                        ?.SelectMany(c => c.Split(separadores))
                        ?.Where(x => !String.IsNullOrEmpty(x) && x.Length >= qtdCaracteresConsideraPalavra)
                        ?.GroupBy(x => x)
                        ?.Select(x => new
                        {
                            chave = x.Key,
                            valor = x.Count()
                        })
                        ?.OrderByDescending(x => x.valor)
                        ?.Take(10)
                        ?.ToDictionary(x => x.chave, x => x.valor);
            }
            catch (Exception)
            {
                mensagemErro = "Erro no processanto para extração dos dados";
                return null;
            }            
        }
    }
}
