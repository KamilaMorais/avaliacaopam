using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaApi.Models;

namespace CopaHAS.Models
{
    public class Jogo
    {
        //Propriedades
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public int EstadioId { get; set; } //FK
        public Estadio EstadioIdNavegacao { get; set; }

        //N:N (N Jogos podem ter N Selecoes e N Selecoes podem ter N Jogos)
        public List<JogoSelecao> JogoSelecoes { get; set; }
            = new List<JogoSelecao>();
    }
}