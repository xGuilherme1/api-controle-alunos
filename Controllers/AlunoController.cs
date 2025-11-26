using Microsoft.AspNetCore.Mvc;
using ApiControleAlunos.Models.Dtos;
using ApiControleAlunos.Models;
using ApiControleAlunos.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace ApiControleAlunos.Controllers
{
    [Route("/alunos")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos(
                [FromQuery] string? search,
                [FromQuery] int? cursoId,
                [FromQuery] int? turmaId
            )
        {
            var query = _context.Alunos.AsQueryable();

            if (search is not null)
            {
                query = query.Where(x => x.Nome.Contains(search));
            }

            if (cursoId is not null)
            {
                query = query.Where(x => x.CursoId == cursoId.Value);
            }

            if (turmaId is not null)
            {
                query = query.Where(x => x.TurmaId == turmaId.Value);
            }

            var alunos = await query
                .Include(a => a.Curso)
                .Include(a => a.Turma)
                .Include(a => a.Endereco)
                .Include(a => a.AlunoDisciplinas).ThenInclude(ad => ad.Disciplina)
                .Select(a => new AlunoDto
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    DataNascimento = a.DataNascimento,
                    CursoId = a.CursoId,
                    TurmaId = a.TurmaId,
                    EnderecoId = a.EnderecoId,
                    Curso = a.Curso == null ? null : new CursoDto { Id = a.Curso.Id, Nome = a.Curso.Nome, CargaHoraria = a.Curso.CargaHoraria },
                    Turma = a.Turma == null ? null : new TurmaDto { Id = a.Turma.Id, Nome = a.Turma.Nome, Ano = a.Turma.Ano },
                    Endereco = a.Endereco == null ? null : new EnderecoDto { Id = a.Endereco.Id, Logradouro = a.Endereco.Logradouro, Numero = a.Endereco.Numero, Cidade = a.Endereco.Cidade, Estado = a.Endereco.Estado },
                    Disciplinas = a.AlunoDisciplinas.Select(ad => new AlunoDisciplinaDto {
                        AlunoId = ad.AlunoId,
                        DisciplinaId = ad.DisciplinaId,
                        Nota = ad.Nota,
                        Disciplina = ad.Disciplina == null ? null : new DisciplinaDto { Id = ad.Disciplina.Id, Nome = ad.Disciplina.Nome, Professor = ad.Disciplina.Professor }
                    }).ToList()
                })
                .ToListAsync();

            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] AlunoDto novoAluno)
        {
            var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == novoAluno.CursoId);
            if (curso is null) return NotFound("Curso informado não encontrado");

            var turma = await _context.Turmas.FirstOrDefaultAsync(t => t.Id == novoAluno.TurmaId);
            if (turma is null) return NotFound("Turma informada não encontrada");

            var endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.Id == novoAluno.EnderecoId);
            if (endereco is null) return NotFound("Endereço informado não encontrado");

            var aluno = new Aluno
            {
                Nome = novoAluno.Nome,
                DataNascimento = novoAluno.DataNascimento,
                CursoId = novoAluno.CursoId,
                TurmaId = novoAluno.TurmaId,
                EnderecoId = novoAluno.EnderecoId,

                Curso = curso,
                Turma = turma,
                Endereco = endereco
            };

            if (novoAluno.Disciplinas != null)
            {
                foreach (var ad in novoAluno.Disciplinas)
                {
                    var disciplina = await _context.Disciplinas.FirstOrDefaultAsync(d => d.Id == ad.DisciplinaId);
                    if (disciplina is null) return NotFound($"Disciplina {ad.DisciplinaId} não encontrada");

                    aluno.AlunoDisciplinas.Add(new AlunoDisciplina
                    {
                        DisciplinaId = ad.DisciplinaId,
                        Nota = ad.Nota,
                        Disciplina = disciplina
                    });
                }
            }

            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();

            var created = await _context.Alunos
                .Where(a => a.Id == aluno.Id)
                .Include(a => a.Curso)
                .Include(a => a.Turma)
                .Include(a => a.Endereco)
                .Include(a => a.AlunoDisciplinas).ThenInclude(ad => ad.Disciplina)
                .FirstOrDefaultAsync();

            return Created("", created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] AlunoDto atualizacaoAluno)
        {
            var aluno = await _context.Alunos
                .Include(a => a.AlunoDisciplinas)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == atualizacaoAluno.CursoId);
            if (curso is null) return NotFound("Curso informado não encontrado");

            var turma = await _context.Turmas.FirstOrDefaultAsync(t => t.Id == atualizacaoAluno.TurmaId);
            if (turma is null) return NotFound("Turma informada não encontrada");

            var endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.Id == atualizacaoAluno.EnderecoId);
            if (endereco is null) return NotFound("Endereço informado não encontrado");

            aluno.Nome = atualizacaoAluno.Nome;
            aluno.DataNascimento = atualizacaoAluno.DataNascimento;
            aluno.CursoId = atualizacaoAluno.CursoId;
            aluno.TurmaId = atualizacaoAluno.TurmaId;
            aluno.EnderecoId = atualizacaoAluno.EnderecoId;

            aluno.AlunoDisciplinas.Clear();
            if (atualizacaoAluno.Disciplinas != null)
            {
                foreach (var ad in atualizacaoAluno.Disciplinas)
                {
                    var disciplina = await _context.Disciplinas.FirstOrDefaultAsync(d => d.Id == ad.DisciplinaId);
                    if (disciplina is null) return NotFound($"Disciplina {ad.DisciplinaId} não encontrada");

                    aluno.AlunoDisciplinas.Add(new AlunoDisciplina
                    {
                        AlunoId = id,
                        DisciplinaId = ad.DisciplinaId,
                        Nota = ad.Nota
                    });
                }
            }

            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();

            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var aluno = await _context.Alunos
                .Include(a => a.AlunoDisciplinas)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null)
            {
                return NotFound();
            }

            _context.AlunoDisciplinas.RemoveRange(aluno.AlunoDisciplinas);
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
