using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CopaHAS.Models;
using CopaHAS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CopaHAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JogosSelecoesController : ControllerBase
    {
        private readonly DataContext _context;

        public JogosSelecoesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{jogoId}/{selecaoId}")]
        public async Task<IActionResult> GetSingle(int jogoId, int selecaoId)
        {
            try
            {
                JogoSelecao jogoSelecao = await _context.TB_JOGO_SELECOES
                    .Include(js => js.JogoIdNavegacao)
                    .Include(js => js.SelecaoIdNavegacao)
                    .FirstOrDefaultAsync(eBusca => eBusca.JogoId == jogoId && eBusca.SelecaoId == selecaoId);

                return Ok(jogoSelecao);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<JogoSelecao> lista = await _context.TB_JOGO_SELECOES
                    .Include(js => js.JogoIdNavegacao)
                    .Include(js => js.SelecaoIdNavegacao)
                    .ToListAsync();

                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(JogoSelecao jogoSelecao)
        {
            try
            {
                await _context.TB_JOGO_SELECOES.AddAsync(jogoSelecao);
                await _context.SaveChangesAsync();

                return Ok(jogoSelecao);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }
    }
}