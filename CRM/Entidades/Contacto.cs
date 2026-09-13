using System.ComponentModel.DataAnnotations;

namespace CRM.Entidades
{
    public class Contacto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; } = null!;

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string CorreoElectronico { get; set; } = null!;

        [StringLength(150)]
        public string? Empresa { get; set; }
    }
}
