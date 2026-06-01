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
    public class SelecoesController : ControllerBase
    {
        private readonly DataContext _context;

        public SelecoesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Selecao selecao = await _context.TB_SELECOES
                    .Include(s => s.Tecnico)
                    .Include(s => s.Jogadores)
                    .FirstOrDefaultAsync(eBusca => eBusca.Id == id);

                return Ok(selecao);
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
                List<Selecao> lista = await _context.TB_SELECOES
                    .Include(s => s.Tecnico)
                    .ToListAsync();

                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Selecao selecao)
        {
            try
            {
                await _context.TB_SELECOES.AddAsync(selecao);
                await _context.SaveChangesAsync();

                return Ok(selecao);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Selecao selecao)
        {
            try
            {
                _context.TB_SELECOES.Update(selecao);
                await _context.SaveChangesAsync();

                return Ok(selecao);
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
                Selecao selecao = await _context.TB_SELECOES
                    .FirstOrDefaultAsync(eBusca => eBusca.Id == id);

                _context.TB_SELECOES.Remove(selecao);
                await _context.SaveChangesAsync();

                return Ok(selecao);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }
    }
}