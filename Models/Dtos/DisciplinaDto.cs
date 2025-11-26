using System.ComponentModel.DataAnnotations;

namespace ApiControleAlunos.Models.Dtos
{
    public class DisciplinaDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(150)]
        public string Professor { get; set; }
    }
}
