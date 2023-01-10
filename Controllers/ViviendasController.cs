using WebAPI.Models;
using WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ViviendasController : ControllerBase
{
    private readonly ViviendasService viviendasService;

    public ViviendasController(ViviendasService viviendasService) =>
        this.viviendasService = viviendasService;

    [HttpGet]
    public async Task<List<Vivienda>> Get() =>
        await viviendasService.GetViviendas();

    [HttpGet("{id}")]
    public async Task<ActionResult<Vivienda>> Get(Guid id)
    {
        var vivienda = await viviendasService.GetViviendaById(id);

        if (vivienda is null)
        {
            return NotFound();
        }

        return vivienda;
    }
    [HttpGet("getByPropietario/{id}")]
    public async Task<ActionResult<List<Vivienda>>> GetByPropietario(Guid id)
    {
        var viviendas = await viviendasService.GetViviendasByPropietario(id);

        if (viviendas is null)
        {
            return NotFound();
        }

        return viviendas;
    }

    [HttpGet("getByLocalidad/")]
    public async Task<ActionResult<List<Vivienda>>> GetByLocalidad(string localidad)
    {
        var viviendas = await viviendasService.GetViviendasByLocalidad(localidad);

        if (viviendas is null)
        {
            return NotFound();
        }

        return viviendas;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Vivienda newVivienda)
    {
        await viviendasService.CreateVivienda(newVivienda);

        return CreatedAtAction(nameof(Get), new { id = newVivienda.Id }, newVivienda);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Vivienda updatedVivienda)
    {
        var vivienda = await viviendasService.GetViviendaById(id);

        if (vivienda is null)
        {
            return NotFound();
        }

        updatedVivienda.Id = vivienda.Id;

        await viviendasService.UpdateVivienda(id, updatedVivienda);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var vivienda = await viviendasService.GetViviendaById(id);

        if (vivienda is null)
        {
            return NotFound();
        }

        await viviendasService.RemoveVivienda(id);

        return NoContent();
    }
}