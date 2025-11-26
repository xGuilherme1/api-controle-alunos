using Microsoft.AspNetCore.Mvc;
using ApiControleAlunos.Models;
using ApiControleAlunos.Models.Dtos;
using ApiControleAlunos.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace ApiControleAlunos.Controllers
{
    [Route("/turmas")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TurmaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos([FromQuery] string? search)
        {
            var query = _context.Turmas.AsQueryable();

            if (search is not null)
                query = query.Where(t => t.Nome.Contains(search));

            var turmas = await query.ToListAsync();
            return Ok(turmas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var turma = await _context.Turmas.FirstOrDefaultAsync(t => t.Id == id);
            if (turma == null) return NotFound();
            return Ok(turma);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] TurmaDto novaTurma)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var turma = new Turma
            {
                Nome = novaTurma.Nome,
                Ano = novaTurma.Ano
            };

            await _context.Turmas.AddAsync(turma);
            await _context.SaveChangesAsync();

            return Created("", turma);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] TurmaDto atualizacao)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var turma = await _context.Turmas.FirstOrDefaultAsync(t => t.Id == id);
            if (turma == null) return NotFound();

            turma.Nome = atualizacao.Nome;
            turma.Ano = atualizacao.Ano;

            _context.Turmas.Update(turma);
            await _context.SaveChangesAsync();

            return Ok(turma);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var turma = await _context.Turmas.FirstOrDefaultAsync(t => t.Id == id);
            if (turma == null) return NotFound();

            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
