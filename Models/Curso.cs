using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiControleAlunos.Models
{
    [Table("Curso")]
    public class Curso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_cur")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nome_cur")]
        public string Nome { get; set; }

        [Column("cargaHoraria_cur")]
        public int CargaHoraria { get; set; }

        public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
    }
}
