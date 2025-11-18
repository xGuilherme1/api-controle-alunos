using System.ComponentModel.DataAnnotations.Schema;

namespace ApiControleAlunos.Models
{
    [Table("AlunoDisciplina")]
    public class AlunoDisciplina
    {
        [Column("alunoId")]
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        [Column("disciplinaId")]
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }

        [Column("Nota")]
        public decimal Nota { get; set; }
    }
}
