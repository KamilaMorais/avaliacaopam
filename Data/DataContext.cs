using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaApi.Models;
using CopaHAS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Identity.Client;

namespace CopaHAS.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Jogador> TB_JOGADORES { get; set; }  
        public DbSet<Estadio> TB_ESTADIOS { get; set; }      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogador>().ToTable("TB_JOGADORES");

            modelBuilder.Entity<Jogador>().HasData
            (
                new Jogador(){ Id=1, Nome="Hugo Souza",NumeroCamisa=1,Posicao="Goleiro",Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=2, Nome="Yuri Alberto",NumeroCamisa=9,Posicao="Atacante",Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=3, Nome="Danilo", NumeroCamisa=2, Posicao="Lateral Direito", Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=4, Nome="Marquinhos", NumeroCamisa=4, Posicao="Zagueiro", Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=5, Nome="Casemiro", NumeroCamisa=5, Posicao="Volante", Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=6, Nome="Alex Sandro", NumeroCamisa=6, Posicao="Lateral Esquerdo", Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=7, Nome="Lucas Paquetá", NumeroCamisa=7, Posicao="Meio Campo", Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=8, Nome="Bruno Guimarães", NumeroCamisa=8, Posicao="Meio Campo", Status=Models.Enuns.StatusJogador.Reserva },
                new Jogador(){ Id=9, Nome="Richarlison", NumeroCamisa=10, Posicao="Atacante", Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=10, Nome="Vinicius Jr", NumeroCamisa=11, Posicao="Atacante", Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=11, Nome="Rodrygo", NumeroCamisa=19, Posicao="Atacante", Status=Models.Enuns.StatusJogador.DepartamentoMedico },
                new Jogador(){ Id=12, Nome="Alisson", NumeroCamisa=23, Posicao="Goleiro", Status=Models.Enuns.StatusJogador.NaoRelacionado }
            );

            modelBuilder.Entity<Estadio>().ToTable("TB_ESTADIOS");

            modelBuilder.Entity<Estadio>().HasData
            (
                new Estadio(){ Id = 1, Nome = "Maracanã", Cidade = "Rio de Janeiro", Capacidade = 78838 },
                new Estadio(){ Id = 2, Nome = "Morumbi", Cidade = "São Paulo", Capacidade = 66795 },
                new Estadio(){ Id = 3, Nome = "Allianz Parque", Cidade = "São Paulo", Capacidade = 43713 },
                new Estadio(){ Id = 4, Nome = "Arena Corinthians", Cidade = "São Paulo", Capacidade = 49205 },
                new Estadio(){ Id = 5, Nome = "Mineirão", Cidade = "Belo Horizonte", Capacidade = 61927 },
                new Estadio(){ Id = 6, Nome = "Arena do Grêmio", Cidade = "Porto Alegre", Capacidade = 55662 },
                new Estadio(){ Id = 7, Nome = "Beira-Rio", Cidade = "Porto Alegre", Capacidade = 50842 }
            );


        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>()
                .HaveColumnType("varchar").HaveMaxLength(200);

            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        optionsBuilder.ConfigureWarnings(warnings => warnings
        .Ignore(RelationalEventId.PendingModelChangesWarning));
        }

    }
}

