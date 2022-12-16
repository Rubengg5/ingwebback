using Microsoft.AspNetCore.Mvc;
using Parcial3.Models;
using Parcial3.Services;

namespace Parcial3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParadaController : ControllerBase
    {
        private readonly ParadaService paradaService;

        public ParadaController(ParadaService paradaService) =>
            this.paradaService = paradaService;

        [HttpGet]
        public async Task<List<Parada>> Get() =>
            await paradaService.GetParadas();

        [HttpGet("{codParada}")]
        public async Task<ActionResult<Parada>> Get(int codParada)
        {
            var x = await paradaService.GetParadaByCodParada(codParada);

            if (x is null)
            {
                return NotFound();
            }

            return x;
        }

        [HttpGet("{linea}/{sentido}")]
        public async Task<List<Parada>> GetByLineaYSentido(int linea, int sentido) =>
            await paradaService.GetParadasByLineaYSentido(linea, sentido);
        

        //[HttpGet("getByName/{name}")]
        //public async Task<ActionResult<x>> GetByName(string name)
        //{
        //    var x = await xService.GetByName(name);

        //    if (x is null)
        //    {
        //        return NotFound();
        //    }

        //    return x;
        //}

        //[HttpGet("getByUsuario/{usuario}")]
        //public async Task<List<x>> GetByUsuario(Guid usuario)
        //{
        //    return await xService.GetByUsuario(usuario);
        //}

        [HttpPost]
        public async Task<IActionResult> Post(Parada newx)
        {
            await paradaService.CreateParada(newx);

            return CreatedAtAction(nameof(Get), new { codParada = newx.codParada }, newx);
        }

        //[HttpPost]
        //public async Task<IActionResult> PostParadas(List<Parada> listx)
        //{
        //    await paradaService.CreateParadas(listx);

        //    return CreatedAtAction(nameof(Get), listx);
        //}

        [HttpPut("{codParada}")]
        public async Task<IActionResult> Update(int codParada, Parada updatedx)
        {
            var x = await paradaService.GetParadaByCodParada(codParada);

            if (x is null)
            {
                return NotFound();
            }

            updatedx.codParada = x.codParada;

            await paradaService.UpdateParada(codParada, updatedx);

            return NoContent();
        }

        [HttpDelete("{codParada}")]
        public async Task<IActionResult> Delete(int codParada)
        {
            var x = await paradaService.GetParadaByCodParada(codParada);

            if (x is null)
            {
                return NotFound();
            }

            await paradaService.RemoveParada(codParada);

            return NoContent();
        }

        //[HttpGet("getByInquilino/{inquilinoId}")]
        //public async Task<ActionResult<List<x>>> GetByInquilino(Guid inquilinoId)
        //{
        //    var x = await xService.Getx();

        //    if (x is null)
        //    {
        //        return NotFound();
        //    }

        //    return x;
        //}

        //[HttpGet("getByVivienda/{viviendaId}")]
        //public async Task<ActionResult<List<x>>> GetByVivienda(Guid viviendaId)
        //{
        //    var xs = await xService.Getx();

        //    if (xs is null)
        //    {
        //        return NotFound();
        //    }

        //    return xs;
        //}

        //[HttpGet("getByFecha")]
        //public async Task<ActionResult<List<x>>> GetByVivienda(string fechaEntrada, string fechaSalida)
        //{
        //    var xs = await xService.Getx();

        //    if (xs is null)
        //    {
        //        return NotFound();
        //    }

        //    return xs;
        //}


    }
}
