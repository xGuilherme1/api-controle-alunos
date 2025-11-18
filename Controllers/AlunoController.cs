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
    }
}
