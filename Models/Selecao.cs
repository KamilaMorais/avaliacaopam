using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace CopaHAS.Models
{
    public class Selecao
    {
        //Propriedades
        public int Id { get; set; }
        public string Pais { get; set; } = string.Empty;

        //1:N (1 Selecao tem N Jogares!)
        public List<Jogador> Jogadores { get; set; } =
        new List<Jogador>();

        //1:1 (1 Selecao pode ter 1 técnico ou não)
        public Tecnico Tecnico { get; set; } //? = pode ficar nulo

        //N:N (N selecoes podem jogar N jogos e N jogos podem ter N selecoes)
        public List<JogoSelecao> jogoSelecoes { get; set; } =
          new List<JogoSelecao>();
    }
}