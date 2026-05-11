using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaHAS.Models
{
    public class Tecnico
    {
        //Propriedades
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int SelecaoId { get; set; } //relacionamento 1:1 - FK
        public Selecao SelecaoIdNavegacao { get; set; } //Navegação 

    }
}