using B3serverREST.Models;
using B3serverREST.Services;
using Microsoft.AspNetCore.Mvc;

namespace B3serverREST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MensajesController : ControllerBase
    {
        private readonly MensajesService MensajesService;

        public MensajesController(MensajesService MensajesService) =>
            this.MensajesService = MensajesService;

        [HttpGet]
        public async Task<List<Mensaje>> Get() =>
            await MensajesService.GetMensajes();

        [HttpGet("{id}")]
        public async Task<ActionResult<Mensaje>> Get(Guid id)
        {
            var Mensaje = await MensajesService.GetMensajeById(id);

            if (Mensaje is null)
            {
                return NotFound();
            }

            return Mensaje;
        }

        [HttpGet("getByRemitente/{id}")]
        public async Task<ActionResult<List<Mensaje>>> GetByRemitente(Guid id)
        {
            var Mensajes = await MensajesService.GetMensajesByRemitente(id);

            if (Mensajes is null)
            {
                return NotFound();
            }

            return Mensajes;
        }

        [HttpGet("getByDestinatario/{id}")]
        public async Task<ActionResult<List<Mensaje>>> GetByDestinatario(Guid id)
        {
            var Mensajes = await MensajesService.GetMensajesByDestinatario(id);

            if (Mensajes is null)
            {
                return NotFound();
            }

            Mensajes = (List<Mensaje>)(from msg in Mensajes
                       orderby msg.timestamp
                       select msg);

            return Mensajes;
        }

        [HttpGet("getByDestinatarioAndRemitente/{idRemitente}/{idDestinatario}")]
        public async Task<ActionResult<List<Mensaje>>> GetByDestinatarioAndRemitente(Guid idRemitente, Guid idDestinatario)
        {
            var Mensajes = await MensajesService.GetMensajesByRemitenteAndDestinatario(idRemitente, idDestinatario);

            if (Mensajes is null)
            {
                return NotFound();
            }
            
            return Mensajes.OrderBy(m => m.timestamp).ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Mensaje newMensaje)
        {
            await MensajesService.CreateMensaje(newMensaje);

            return CreatedAtAction(nameof(Get), new { id = newMensaje.Id }, newMensaje);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Mensaje updatedMensaje)
        {
            var Mensaje = await MensajesService.GetMensajeById(id);

            if (Mensaje is null)
            {
                return NotFound();
            }

            updatedMensaje.Id = Mensaje.Id;

            await MensajesService.UpdateMensaje(id, updatedMensaje);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var Mensaje = await MensajesService.GetMensajeById(id);

            if (Mensaje is null)
            {
                return NotFound();
            }

            await MensajesService.RemoveMensaje(id);

            return NoContent();
        }
    }
}
