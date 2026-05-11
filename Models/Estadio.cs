using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaHAS.Models;

namespace CopaApi.Models
{
    public class Estadio
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Cidade { get; set; } = string.Empty;
        public int Capacidade { get; set; }

        //1:N
        public List<Jogo> Jogos { get; set; }
            = new List<Jogo>();
    }
}