using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CopaHAS.Models;
using CopaHAS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CopaHAS.DTOs;

namespace CopaHAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly DataContext _context;

        public JogosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Jogo jogo = await _context.TB_JOGOS
                    .Include(j => j.EstadioIdNavegacao)
                    .Include(j => j.JogoSelecoes)
                    .FirstOrDefaultAsync(eBusca => eBusca.Id == id);

                return Ok(jogo);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get2()
        {
            try
            {
                List<Jogo> lista = await _context.TB_JOGOS
                    .Include(j => j.EstadioIdNavegacao)
                    .ToListAsync();

                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpGet("ObterTabela")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var sql = @"SELECT
J.id AS IdJogo,
J.DataHora,
E.Nome AS Estadio,
E.Cidade,
S1.Nome AS SelecaoMandante,
JS1.Gols AS GolsMandante,
JS1.GolsDecisaoPenaltis AS GolsDecisaoPenaltisMandante,
T1.Nome AS TecnicoMandante,
S2.Nome AS SelecaoVisitante,
JS2.Gols AS GolsVisitante,
JS2.GolsDecisaoPenaltis AS GolsDecisaoPenaltisVisitante,
T2.Nome AS TecnicoVisitante
FROM TB_JOGOS J
INNER JOIN TB_ESTADIOS E ON E.Id = J.EstadioId
INNER JOIN TB_JOGOS_SELECOES JS1 ON JS1.JogoId = J.Id
INNER JOIN TB_SELECOES S1 ON S1.Id = JS1.SelecaoId
LEFT JOIN TB_TECNICOS T1 ON T1.SelecaoId = S1.Id
INNER JOIN TB_JOGOS_SELECOES JS2 ON JS2.JogoId = J.Id AND JS2.SelecaoId <> JS1.SelecaoId
INNER JOIN TB_SELECOES S2 ON S2.Id = JS2.SelecaoId
LEFT JOIN TB_TECNICOS T2 ON T2.SelecaoId = S2.Id
WHERE S1.Id < S2.Id
ORDER BY J.Id
        ";

                var resultado = await _context.Database.SqlQueryRaw<JogoDTO>(sql)
                    .ToListAsync();

                return Ok(resultado);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Jogo jogo)
        {
            try
            {
                await _context.TB_JOGOS.AddAsync(jogo);
                await _context.SaveChangesAsync();

                return Ok(jogo);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Jogo jogo)
        {
            try
            {
                _context.TB_JOGOS.Update(jogo);
                await _context.SaveChangesAsync();

                return Ok(jogo);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // O Include carrega as dependências da tabela associativa
                Jogo jogo = await _context.TB_JOGOS
                    .Include(j => j.JogoSelecoes)
                    .FirstOrDefaultAsync(eBusca => eBusca.Id == id);

                if (jogo == null)
                {
                    return NotFound("Jogo não encontrado.");
                }

                // 1º passo: Remover as associações na tabela TB_JOGO_SELECOES
                if (jogo.JogoSelecoes != null && jogo.JogoSelecoes.Any())
                {
                    _context.TB_JOGO_SELECOES.RemoveRange(jogo.JogoSelecoes);
                }

                // 2º passo: Remover o jogo em si na TB_JOGOS
                _context.TB_JOGOS.Remove(jogo);

                await _context.SaveChangesAsync();

                return Ok(jogo);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }
    }
}