using WebAPI.Models;
using WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly ReservasService reservasService;

    public ReservasController(ReservasService reservasService) =>
        this.reservasService = reservasService;

    [HttpGet]
    public async Task<List<Reserva>> Get() =>
        await reservasService.GetReservas();

    [HttpGet("{id}")]
    public async Task<ActionResult<Reserva>> Get(Guid id)
    {
        var reserva = await reservasService.GetReservaById(id);

        if (reserva is null)
        {
            return NotFound();
        }

        return reserva;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Reserva newReserva)
    {
        await reservasService.CreateReserva(newReserva);

        return CreatedAtAction(nameof(Get), new { id = newReserva.Id }, newReserva);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Reserva updatedReserva)
    {
        var reserva = await reservasService.GetReservaById(id);

        if (reserva is null)
        {
            return NotFound();
        }

        updatedReserva.Id = reserva.Id;

        await reservasService.UpdateReserva(id, updatedReserva);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var reserva = await reservasService.GetReservaById(id);

        if (reserva is null)
        {
            return NotFound();
        }

        await reservasService.RemoveReserva(id);

        return NoContent();
    }

    [HttpGet("getByInquilino/{inquilinoId}")]
    public async Task<ActionResult<List<Reserva>>> GetByInquilino(Guid inquilinoId)
    {
        var reserva = await reservasService.GetReservaByInquilinoId(inquilinoId);

        if (reserva is null)
        {
            return NotFound();
        }

        return reserva;
    }

    [HttpGet("getByVivienda/{viviendaId}")]
    public async Task<ActionResult<List<Reserva>>> GetByVivienda(Guid viviendaId)
    {
        var reservas = await reservasService.GetReservasByVivienda(viviendaId);

        if (reservas is null)
        {
            return NotFound();
        }

        return reservas;
    }

    [HttpGet("getByFecha")]
    public async Task<ActionResult<List<Reserva>>> GetByVivienda(string fechaEntrada, string fechaSalida)
    {
        var reservas = await reservasService.GetReservasByFecha(fechaEntrada, fechaSalida);

        if (reservas is null)
        {
            return NotFound();
        }

        return reservas;
    }
}