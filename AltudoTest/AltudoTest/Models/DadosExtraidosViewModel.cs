using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltudoTest.Models
{
    public class DadosExtraidosViewModel
    {
        public string MensagemErro { get; set; }
        public List<string> Imagens { get; set; }
        public Dictionary<string,int> Palavras { get; set; }
    }
}
