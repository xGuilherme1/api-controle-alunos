using Microsoft.AspNetCore.Mvc;
using ApiControleAlunos.Models;
using ApiControleAlunos.Models.Dtos;
using ApiControleAlunos.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace ApiControleAlunos.Controllers
{
    [Route("/cursos")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CursoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos([FromQuery] string? search)
        {
            var query = _context.Cursos.AsQueryable();

            if (search is not null)
                query = query.Where(c => c.Nome.Contains(search));

            var cursos = await query.ToListAsync();
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == id);
            if (curso == null) return NotFound();
            return Ok(curso);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CursoDto novoCurso)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var curso = new Curso
            {
                Nome = novoCurso.Nome,
                CargaHoraria = novoCurso.CargaHoraria
            };

            await _context.Cursos.AddAsync(curso);
            await _context.SaveChangesAsync();

            return Created("", curso);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] CursoDto atualizacao)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == id);
            if (curso == null) return NotFound();

            curso.Nome = atualizacao.Nome;
            curso.CargaHoraria = atualizacao.CargaHoraria;

            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();

            return Ok(curso);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == id);
            if (curso == null) return NotFound();

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
