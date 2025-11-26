using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiControleAlunos.Models
{
    [Table("Turma")]
    public class Turma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_tur")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("nome_tur")]
        public string Nome { get; set; }

        [Column("ano_tur")]
        public int Ano { get; set; }

        public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
    }
}
