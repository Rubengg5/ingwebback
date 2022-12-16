using Microsoft.AspNetCore.Mvc;
using Parcial3.Models;
using Parcial3.Services;

namespace Parcial3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class xController : ControllerBase
    {
        private readonly xService xService;

        public xController(xService xService) =>
            this.xService = xService;

        [HttpGet]
        public async Task<List<x>> Get() =>
            await xService.Getx();

        [HttpGet("{id}")]
        public async Task<ActionResult<x>> Get(Guid id)
        {
            var x = await xService.GetxById(id);

            if (x is null)
            {
                return NotFound();
            }

            return x;
        }

        [HttpGet("getByName/{name}")]
        public async Task<ActionResult<x>> GetByName(string name)
        {
            var x = await xService.GetByName(name);

            if (x is null)
            {
                return NotFound();
            }

            return x;
        }

        [HttpGet("getByUsuario/{usuario}")]
        public async Task<List<x>> GetByUsuario(Guid usuario)
        {
            return await xService.GetByUsuario(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Post(x newx)
        {
            await xService.Createx(newx);

            return CreatedAtAction(nameof(Get), new { id = newx.Id }, newx);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, x updatedx)
        {
            var x = await xService.GetxById(id);

            if (x is null)
            {
                return NotFound();
            }

            updatedx.Id = x.Id;

            await xService.Updatex(id, updatedx);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var x = await xService.GetxById(id);

            if (x is null)
            {
                return NotFound();
            }

            await xService.Removex(id);

            return NoContent();
        }

        [HttpGet("getByInquilino/{inquilinoId}")]
        public async Task<ActionResult<List<x>>> GetByInquilino(Guid inquilinoId)
        {
            var x = await xService.Getx();

            if (x is null)
            {
                return NotFound();
            }

            return x;
        }

        [HttpGet("getByVivienda/{viviendaId}")]
        public async Task<ActionResult<List<x>>> GetByVivienda(Guid viviendaId)
        {
            var xs = await xService.Getx();

            if (xs is null)
            {
                return NotFound();
            }

            return xs;
        }

        [HttpGet("getByFecha")]
        public async Task<ActionResult<List<x>>> GetByVivienda(string fechaEntrada, string fechaSalida)
        {
            var xs = await xService.Getx();

            if (xs is null)
            {
                return NotFound();
            }

            return xs;
        }


    }
}
