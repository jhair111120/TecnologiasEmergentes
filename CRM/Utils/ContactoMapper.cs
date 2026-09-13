using CRM.DTO;
using CRM.Entidades;

namespace CRM.Utils
{
    /// <summary>
    /// Mapeo manual entre la entidad Contacto y sus DTOs.
    /// Evita dependencias externas de librerías de mapeo.
    /// </summary>
    public static class ContactoMapper
    {
        public static ContactoDTO ToDTO(Contacto contacto) => new()
        {
            Id = contacto.Id,
            Nombre = contacto.Nombre,
            Apellido = contacto.Apellido,
            Telefono = contacto.Telefono,
            CorreoElectronico = contacto.CorreoElectronico,
            Empresa = contacto.Empresa
        };

        public static List<ContactoDTO> ToDTOList(IEnumerable<Contacto> contactos) =>
            contactos.Select(ToDTO).ToList();

        public static Contacto ToEntity(ContactoCreaDTO dto) => new()
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Telefono = dto.Telefono,
            CorreoElectronico = dto.CorreoElectronico,
            Empresa = dto.Empresa
        };

        public static void ApplyUpdate(ContactoCreaDTO dto, Contacto contacto)
        {
            contacto.Nombre = dto.Nombre;
            contacto.Apellido = dto.Apellido;
            contacto.Telefono = dto.Telefono;
            contacto.CorreoElectronico = dto.CorreoElectronico;
            contacto.Empresa = dto.Empresa;
        }
    }
}
