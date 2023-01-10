using WebAPI.Models;
using WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValoracionesController : ControllerBase
{
    private readonly ValoracionesService valoracionesService;

    public ValoracionesController(ValoracionesService valoracionesService) =>
        this.valoracionesService = valoracionesService;

    [HttpGet]
    public async Task<List<Valoracion>> Get() =>
        await valoracionesService.GetValoraciones();

    [HttpGet("{id}")]
    public async Task<ActionResult<Valoracion>> Get(Guid id)
    {
        var valoracion = await valoracionesService.GetValoracionById(id);

        if (valoracion is null)
        {
            return NotFound();
        }

        return valoracion;
    }

    [HttpGet("getByVivienda/{id}")]
    public async Task<ActionResult<List<Valoracion>>> GetByVivienda(Guid id)
    {
        var valoraciones = await valoracionesService.GetValoracionesByVivienda(id);

        if (valoraciones is null)
        {
            return NotFound();
        }

        return valoraciones;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Valoracion newValoracion)
    {
        await valoracionesService.CreateValoracion(newValoracion);

        return CreatedAtAction(nameof(Get), new { id = newValoracion.Id }, newValoracion);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Valoracion updatedValoracion)
    {
        var valoracion = await valoracionesService.GetValoracionById(id);

        if (valoracion is null)
        {
            return NotFound();
        }

        updatedValoracion.Id = valoracion.Id;

        await valoracionesService.UpdateValoracion(id, updatedValoracion);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var valoracion = await valoracionesService.GetValoracionById(id);

        if (valoracion is null)
        {
            return NotFound();
        }

        await valoracionesService.RemoveValoracion(id);

        return NoContent();
    }
}