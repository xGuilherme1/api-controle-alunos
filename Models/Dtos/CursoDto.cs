using System.ComponentModel.DataAnnotations;

namespace ApiControleAlunos.Models.Dtos
{
    public class CursoDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        public int CargaHoraria { get; set; }
    }
}
