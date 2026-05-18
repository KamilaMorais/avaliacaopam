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
        public DbSet<Selecao> TB_SELECOES { get; set; }
        public DbSet<Tecnico> TB_TECNICOS { get; set; }
        public DbSet<Jogo> TB_JOGOS { get; set; }
        public DbSet<JogoSelecao> TB_JOGO_SELECOES { get; set; }      

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

            modelBuilder.Entity<Selecao>().ToTable("TB_SELECOES");
            modelBuilder.Entity<Tecnico>().ToTable("TB_TECNICOS");
            modelBuilder.Entity<Jogo>().ToTable("TB_JOGOS");
            modelBuilder.Entity<JogoSelecao>().ToTable("TB_JOGO_SELECOES");

            modelBuilder.Entity<Selecao>(entity =>
            {   //mapeamento simples
                entity.HasKey(e => e.Id); //definindo chave primária

                entity.Property(e => e.Pais)
                    .IsRequired() //= not null
                    .HasMaxLength(100);//= varchar
            });

            //JOGADOR (1:N com Seleção)
            modelBuilder.Entity<Jogador>(entity =>
                {
                    entity.HasKey(e => e.Id);

                    entity.Property(e => e.Nome)
                        //.HasColumnName("Nome_diferente_da_classe_no_banco")
                        .IsRequired() //= not null
                        .HasMaxLength(100); //= varchar

                    entity.Property(e => e.Posicao)
                        .HasMaxLength(50);

                    entity.HasOne(d => d.SelecaoIdNavegacao)
                        .WithMany(p => p.Jogadores)
                        .HasForeignKey(d => d.SelecaoId)
                        .OnDelete(DeleteBehavior.Cascade);

            });

            //TECNICO (1:1 com Selecao)
            modelBuilder.Entity<Tecnico>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .IsRequired() //= not null
                    .HasMaxLength(100);//= varchar

                entity.HasOne(d => d.SelecaoIdNavegacao)
                    .WithOne(p => p.Tecnico)
                    .HasForeignKey<Tecnico>(d => d.SelecaoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ESTADIO            
            modelBuilder.Entity<Estadio>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome)
                      .IsRequired()
                      .HasMaxLength(150);
                entity.Property(e => e.Cidade)
                      .HasMaxLength(100);
            });
            
            // JOGO (1:N com Estadio)            
            modelBuilder.Entity<Jogo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DataHora)                      
                      .IsRequired();
                //entity.Property(e => e.DataHora)
                      //.HasColumnName("Nome_outra_coluna_diferente_da_classe_no_banco");
                entity.HasOne(d => d.EstadioIdNavegacao)
                      .WithMany(p => p.Jogos)
                      .HasForeignKey(d => d.EstadioId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            //JOGO-SELECOES (N:N) -> entidade associativa
            modelBuilder.Entity<JogoSelecao>(entity =>
            {
                entity.HasKey(e => new { e.JogoId, e.SelecaoIdNavegacao });
                entity.HasOne(d => d.JogoIdNavegacao)
                    .WithMany(p => p.JogoSelecoes)
                    .HasForeignKey(d => d.JogoId);

                entity.HasOne(d => d.SelecaoIdNavegacao)
                    .WithMany(p => p.jogoSelecoes)
                    .HasForeignKey(d => d.SelecaoId);
            });
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
