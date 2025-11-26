using Microsoft.AspNetCore.Mvc;
using ApiControleAlunos.Models;
using ApiControleAlunos.Models.Dtos;
using ApiControleAlunos.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace ApiControleAlunos.Controllers
{
    [Route("/enderecos")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnderecoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos([FromQuery] string? cidade)
        {
            var query = _context.Enderecos.AsQueryable();

            if (cidade is not null)
                query = query.Where(e => e.Cidade.Contains(cidade));

            var enderecos = await query.ToListAsync();
            return Ok(enderecos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
            if (endereco == null) return NotFound();
            return Ok(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] EnderecoDto novoEndereco)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var endereco = new Endereco
            {
                Logradouro = novoEndereco.Logradouro,
                Numero = novoEndereco.Numero,
                Cidade = novoEndereco.Cidade,
                Estado = novoEndereco.Estado
            };

            await _context.Enderecos.AddAsync(endereco);
            await _context.SaveChangesAsync();

            return Created("", endereco);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] EnderecoDto atualizacao)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
            if (endereco == null) return NotFound();

            endereco.Logradouro = atualizacao.Logradouro;
            endereco.Numero = atualizacao.Numero;
            endereco.Cidade = atualizacao.Cidade;
            endereco.Estado = atualizacao.Estado;

            _context.Enderecos.Update(endereco);
            await _context.SaveChangesAsync();

            return Ok(endereco);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
            if (endereco == null) return NotFound();

            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
