using B3serverREST.Models;
using B3serverREST.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI.Models;

namespace B3serverREST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly MessagesService MessagesService;

        public MessagesController(MessagesService MessagesService) =>
            this.MessagesService = MessagesService;

        [HttpGet]
        public async Task<List<Message>> Get() =>
            await MessagesService.GetMessages();

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> Get(Guid id)
        {
            var Message = await MessagesService.GetMessageById(id);

            if (Message is null)
            {
                return NotFound();
            }

            return Message;
        }

        [HttpGet("getConversacion/{idRemitente}/{idDestinatario}")]
        public async Task<List<Message>> GetConversacion(Guid idRemitente, Guid idDestinatario) =>
            await MessagesService.GetConversacionByRemitenteAndDestinatario(idRemitente, idDestinatario);



        [HttpGet("getCabecerasByUserDesc/{id}")]
        public async Task<ActionResult<List<MessageDTO>>> GetCabecerasByUserDesc(Guid id)
        {
            var Messages = await MessagesService.GetByUserDesc(id);

            List<MessageDTO> messagesDTO = new List<MessageDTO>();

            foreach (var m in Messages)
            {
                MessageDTO cabecera = new MessageDTO { id = m.id, de = m.de, para = m.para, asunto = m.asunto, stamp = m.stamp };
                messagesDTO.Add(cabecera);
            }

            if (Messages is null)
            {
                return NotFound();
            }

            return messagesDTO;
        }


        [HttpGet("getContactos/{id}")]
        public async Task<ActionResult<List<Message>>> GetContactos(Guid id)
        {
            var Messages = await MessagesService.GetByUserDesc(id);

            if (Messages is null)
            {
                return NotFound();
            }

            return Messages.OrderBy(m => m.stamp).ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Message newMessage)
        {
            await MessagesService.CreateMessage(newMessage);

            return CreatedAtAction(nameof(Get), new { id = newMessage.id }, newMessage);
        }

        [HttpPost("/postWithContent")]
        public async Task<IActionResult> PostWithContent(Guid de, Guid para, string asunto, string contenido)
        {
            Message newMessage = new Message{ de = de, para = para, asunto = asunto, contenido = contenido };

            await MessagesService.CreateMessage(newMessage);

            return CreatedAtAction(nameof(Get), new { id = newMessage.id }, newMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Message updatedMessage)
        {
            var Message = await MessagesService.GetMessageById(id);

            if (Message is null)
            {
                return NotFound();
            }

            updatedMessage.id = Message.id;

            await MessagesService.UpdateMessage(id, updatedMessage);

            return NoContent();
        }

        [HttpPut("updateAsunto/{id}")]
        public async Task<IActionResult> UpdateAsunto(Guid id, string updatedAsunto)
        {
            var Message = await MessagesService.GetMessageById(id);

            if (Message is null)
            {
                return NotFound();
            }

            Message.asunto = updatedAsunto;

            await MessagesService.UpdateMessage(id, Message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var Message = await MessagesService.GetMessageById(id);

            if (Message is null)
            {
                return NotFound();
            }

            await MessagesService.RemoveMessage(id);

            return NoContent();
        }

        //[HttpGet("getSinRespuesta/{id1}")]
        //public async Task<ActionResult<List<Cabecera>>> GetSinRespuesta(string id)
        //{
        //    var mensajes = await MensajeService.GetPendientes(id);
        //    var respuestas = new List<Mensaje>();

        //    foreach (Mensaje msg in mensajes)
        //    {
        //        respuestas = await MensajeService.IsLast(msg.cabecera.para, msg.cabecera.stamp);
        //        if (respuestas.Count() > 0) mensajes.Remove(msg);
        //    }

        //    var cabeceras = new List<Cabecera>();

        //    if (mensajes is null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        foreach (Mensaje msg in mensajes)
        //        {
        //            cabeceras.Add(msg.cabecera);
        //        }
        //    }

        //    return cabeceras.OrderBy(m => m.stamp).ToList();
        //}

        //[HttpGet("getContactos/{id}")]
        //public async Task<ActionResult<List<string>>> GetContactos(string id)
        //{
        //    var contactos = await MensajeService.GetContactos(id);


        //    if (contactos is null)
        //    {
        //        return NotFound();
        //    }

        //    return contactos;
        //}

    }
}
