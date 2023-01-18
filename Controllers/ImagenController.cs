using B3serverREST.Models;
using B3serverREST.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI.Models;

namespace B3serverREST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagenesController : ControllerBase
    {
        private readonly ImagenService ImagensService;

        public ImagenesController(ImagenService ImagensService) =>
            this.ImagensService = ImagensService;

        [HttpGet]
        public async Task<List<Imagen>> Get() =>
            await ImagensService.GetImagenes();

        [HttpGet("{id}")]
        public async Task<ActionResult<Imagen>> Get(int id)
        {
            var Imagen = await ImagensService.GetImagenByID(id);

            if (Imagen is null)
            {
                return NotFound();
            }

            return Imagen;
        }

        [HttpGet("getByAparcamiento/{id}")]
        public async Task<ActionResult<Imagen>> GetByAparcamiento(int id)
        {
            var Imagen = await ImagensService.GetImagenByAparcamiento(id);

            if (Imagen is null)
            {
                return NotFound();
            }

            return Imagen;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Imagen newImagen)
        {
            await ImagensService.CreateImagen(newImagen);

            return CreatedAtAction(nameof(Get), new { _id = newImagen._id }, newImagen);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Imagen updatedImagen)
        {
            var Imagen = await ImagensService.GetImagenByID(id);

            if (Imagen is null)
            {
                return NotFound();
            }

            updatedImagen._id = Imagen._id;

            await ImagensService.UpdateImagen(id, updatedImagen);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Imagen = await ImagensService.GetImagenByID(id);

            if (Imagen is null)
            {
                return NotFound();
            }

            await ImagensService.RemoveImagen(id);

            return NoContent();
        }
    }
}
