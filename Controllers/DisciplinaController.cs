using Microsoft.AspNetCore.Mvc;
using ApiControleAlunos.Models;
using ApiControleAlunos.Models.Dtos;
using ApiControleAlunos.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace ApiControleAlunos.Controllers
{
    [Route("/disciplinas")]
    [ApiController]
    public class DisciplinaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DisciplinaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos([FromQuery] string? search)
        {
            var query = _context.Disciplinas.AsQueryable();

            if (search is not null)
                query = query.Where(d => d.Nome.Contains(search));

            var disciplinas = await query.ToListAsync();
            return Ok(disciplinas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var disciplina = await _context.Disciplinas.FirstOrDefaultAsync(d => d.Id == id);
            if (disciplina == null) return NotFound();
            return Ok(disciplina);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] DisciplinaDto nova)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var disciplina = new Disciplina
            {
                Nome = nova.Nome,
                Professor = nova.Professor
            };

            await _context.Disciplinas.AddAsync(disciplina);
            await _context.SaveChangesAsync();

            return Created("", disciplina);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] DisciplinaDto atualizacao)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var disciplina = await _context.Disciplinas.FirstOrDefaultAsync(d => d.Id == id);
            if (disciplina == null) return NotFound();

            disciplina.Nome = atualizacao.Nome;
            disciplina.Professor = atualizacao.Professor;

            _context.Disciplinas.Update(disciplina);
            await _context.SaveChangesAsync();

            return Ok(disciplina);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var disciplina = await _context.Disciplinas.FirstOrDefaultAsync(d => d.Id == id);
            if (disciplina == null) return NotFound();

            _context.Disciplinas.Remove(disciplina);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
