using B3serverREST.Models;
using B3serverREST.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI.Models;

namespace B3serverREST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AparcamientosController : ControllerBase
    {
        private readonly AparcamientosService AparcamientosService;

        public AparcamientosController(AparcamientosService AparcamientosService) =>
            this.AparcamientosService = AparcamientosService;

        [HttpGet]
        public async Task<List<Aparcamiento>> Get() =>
            await AparcamientosService.GetAparcamientos();

        [HttpGet("{id}")]
        public async Task<ActionResult<Aparcamiento>> Get(int id)
        {
            var Aparcamiento = await AparcamientosService.GetAparcamientoBypoiID(id);

            if (Aparcamiento is null)
            {
                return NotFound();
            }

            return Aparcamiento;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Aparcamiento newAparcamiento)
        {
            await AparcamientosService.CreateAparcamiento(newAparcamiento);

            return CreatedAtAction(nameof(Get), new { _id = newAparcamiento._id }, newAparcamiento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Aparcamiento updatedAparcamiento)
        {
            var Aparcamiento = await AparcamientosService.GetAparcamientoBypoiID(id);

            if (Aparcamiento is null)
            {
                return NotFound();
            }

            updatedAparcamiento._id = Aparcamiento._id;

            await AparcamientosService.UpdateAparcamiento(id, updatedAparcamiento);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Aparcamiento = await AparcamientosService.GetAparcamientoBypoiID(id);

            if (Aparcamiento is null)
            {
                return NotFound();
            }

            await AparcamientosService.RemoveAparcamiento(id);

            return NoContent();
        }
    }
}
