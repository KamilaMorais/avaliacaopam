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
    public class EstadiosController : ControllerBase
    {
        private readonly DataContext _context;

        public EstadiosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Estadio estadio = await _context.TB_ESTADIOS
                    .Include(e => e.Jogos)
                    .FirstOrDefaultAsync(eBusca => eBusca.Id == id);

                return Ok(estadio);
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
                List<Estadio> lista = await _context.TB_ESTADIOS.ToListAsync();

                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Estadio estadio)
        {
            try
            {
                await _context.TB_ESTADIOS.AddAsync(estadio);
                await _context.SaveChangesAsync();

                return Ok(estadio);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Estadio estadio)
        {
            try
            {
                _context.TB_ESTADIOS.Update(estadio);
                await _context.SaveChangesAsync();

                return Ok(estadio);
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
                Estadio estadio = await _context.TB_ESTADIOS
                    .FirstOrDefaultAsync(eBusca => eBusca.Id == id);

                _context.TB_ESTADIOS.Remove(estadio);
                await _context.SaveChangesAsync();

                return Ok(estadio);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }
    }
}