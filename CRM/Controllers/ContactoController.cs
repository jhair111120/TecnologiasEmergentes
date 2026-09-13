using CRM.DTO;
using CRM.Entidades;
using CRM.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/contacto
        // Búsqueda opcional: api/contacto?busqueda=garcia
        [HttpGet]
        public async Task<ActionResult<List<ContactoDTO>>> Get([FromQuery] string? busqueda)
        {
            var query = _context.Contactos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                var termino = busqueda.Trim().ToLower();
                query = query.Where(c =>
                    c.Nombre.ToLower().Contains(termino) ||
                    c.Apellido.ToLower().Contains(termino));
            }

            var contactos = await query.ToListAsync();
            return ContactoMapper.ToDTOList(contactos);
        }

        // GET: api/contacto/5
        [HttpGet("{id:int}", Name = "ObtenerContacto")]
        public async Task<ActionResult<ContactoDTO>> GetById(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);

            if (contacto == null)
                return NotFound($"No se encontró el contacto con id {id}.");

            return ContactoMapper.ToDTO(contacto);
        }

        // POST: api/contacto
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ContactoCreaDTO contactoCreaDTO)
        {
            var contacto = ContactoMapper.ToEntity(contactoCreaDTO);

            _context.Contactos.Add(contacto);
            await _context.SaveChangesAsync();

            var contactoDTO = ContactoMapper.ToDTO(contacto);
            return CreatedAtRoute("ObtenerContacto", new { id = contacto.Id }, contactoDTO);
        }

        // PUT: api/contacto/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ContactoCreaDTO contactoCreaDTO)
        {
            var contacto = await _context.Contactos.FindAsync(id);

            if (contacto == null)
                return NotFound($"No se encontró el contacto con id {id}.");

            ContactoMapper.ApplyUpdate(contactoCreaDTO, contacto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/contacto/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);

            if (contacto == null)
                return NotFound($"No se encontró el contacto con id {id}.");

            _context.Contactos.Remove(contacto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
