using System.ComponentModel.DataAnnotations;

namespace ApiControleAlunos.Models.Dtos
{
    public class AlunoDisciplinaDto
    {
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }

        public DisciplinaDto Disciplina { get; set; }

        public decimal Nota { get; set; }
    }
}
