using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiControleAlunos.Models
{
    [Table("Endereco")]
    public class Endereco
    {
        [Key]
        [Column("id_end")]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("logradouro_end")]
        public string Logradouro { get; set; }

        [MaxLength(20)]
        [Column("numero_end")]
        public string Numero { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("cidade_end")]
        public string Cidade { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("estado_end")]
        public string Estado { get; set; }

        public Aluno Aluno { get; set; } = null!;
    }
}
