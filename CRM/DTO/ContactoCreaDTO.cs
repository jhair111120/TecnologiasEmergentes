using System.ComponentModel.DataAnnotations;

namespace CRM.DTO
{
    public class ContactoCreaDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100)]
        public string Apellido { get; set; } = null!;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(20)]
        public string Telefono { get; set; } = null!;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [StringLength(150)]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        public string CorreoElectronico { get; set; } = null!;

        [StringLength(150)]
        public string? Empresa { get; set; }
    }
}
