using System.ComponentModel.DataAnnotations;

namespace ApiControleAlunos.Models.Dtos
{
    public class EnderecoDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Logradouro { get; set; }

        [MaxLength(20)]
        public string Numero { get; set; }

        [Required]
        [MaxLength(100)]
        public string Cidade { get; set; }

        [Required]
        [MaxLength(50)]
        public string Estado { get; set; }
    }
}
