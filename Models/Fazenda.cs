using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intergado.Models
{
    public class Fazenda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

    }
}
