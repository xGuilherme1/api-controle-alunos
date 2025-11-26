using System.ComponentModel.DataAnnotations;

namespace ApiControleAlunos.Models.Dtos
{
    public class TurmaDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        public int Ano { get; set; }
    }
}
