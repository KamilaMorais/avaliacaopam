using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaHAS.Models
{
    public class JogoSelecao //"Entidade Associativa"
    {
        public int JogoId { get; set; }
        public int SelecaoId { get; set; }
        public int Gols { get; set; }
        public int GolsProrrogacao { get; set; }
        public int GolsDecisaoPenaltis { get; set; }

        public Jogo JogoIdNavegacao { get; set; }
        public Selecao SelecaoIdNavegacao { get; set; }
    }
}