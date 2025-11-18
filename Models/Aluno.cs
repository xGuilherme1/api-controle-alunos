using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiControleAlunos.Models
{
    [Table("Aluno")]
    public class Aluno
    {
        [Key]
        [Column("id_alu")]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("nome_alu")]
        public string Nome { get; set; }

        [Required]
        [Column("data_nascimento_alu")]
        public DateTime DataNascimento { get; set; }

        // Chaves estrangeiras para relacionamentos N:1
        [Column("id_curso_fk")]
        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        [Column("id_turma_fk")]
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }

        [Column("id_endereco_fk")]
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
    }
}
