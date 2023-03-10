using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intergado.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Somente números são permitidos.")]
        public string Tag { get; set; }

        [Required]
        [ForeignKey("Fazenda")]
        public int FazendaId { get; set; }

        public virtual Fazenda? Fazenda { get; set; }
    }


}