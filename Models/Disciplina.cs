using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiControleAlunos.Models
{
    [Table("Disciplina")]
    public class Disciplina
    {
        [Key]
        [Column("id_dis")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nome_dis")]
        public string Nome { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("professor_dis")]
        public string Professor { get; set; }

        public ICollection<AlunoDisciplina> AlunoDisciplinas { get; set; } = new List<AlunoDisciplina>();
    }
}
