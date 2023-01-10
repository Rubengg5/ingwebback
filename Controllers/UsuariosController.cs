using WebAPI.Models;
using WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosService usuariosService;

        public UsuariosController(UsuariosService usuariosService) =>
        this.usuariosService = usuariosService;

        [HttpGet]
        public async Task<List<Usuario>> Get() =>
        await usuariosService.GetUsuarios();

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(Guid id)
        {
            var usuario = await usuariosService.GetUsuarioById(id);

            if (usuario is null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Usuario newUsuario)
        {
            await usuariosService.CreateUsuario(newUsuario);

            return CreatedAtAction(nameof(Get), new { id = newUsuario.Id }, newUsuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Usuario updatedUsuario)
        {
            var reserva = await usuariosService.GetUsuarioById(id);

            if (reserva is null)
            {
                return NotFound();
            }

            updatedUsuario.Id = reserva.Id;

            await usuariosService.UpdateUsuario(id, updatedUsuario);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var reserva = await usuariosService.GetUsuarioById(id);

            if (reserva is null)
            {
                return NotFound();
            }

            await usuariosService.RemoveUsuario(id);

            return NoContent();
        }
    }
}
