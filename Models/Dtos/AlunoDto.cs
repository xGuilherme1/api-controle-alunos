using System.ComponentModel.DataAnnotations;

namespace ApiControleAlunos.Models.Dtos
{
    public class AlunoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [MaxLength(150, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int CursoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int TurmaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int EnderecoId { get; set; }

        public CursoDto? Curso { get; set; }
        public TurmaDto? Turma { get; set; }
        public EnderecoDto? Endereco { get; set; }

        public ICollection<AlunoDisciplinaDto> Disciplinas { get; set; } = new List<AlunoDisciplinaDto>();
    }
}
