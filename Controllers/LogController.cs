using B3serverREST.Models;
using B3serverREST.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI.Models;

namespace B3serverREST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly LogService LogService;

        public LogController(LogService LogService) =>
            this.LogService = LogService;

        [HttpGet]
        public async Task<List<Log>> Get() =>
            await LogService.GetLog();

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Log>> Get(Guid id)
        //{
        //    var Log = await LogService.GetLogById(id);

        //    if (Log is null)
        //    {
        //        return NotFound();
        //    }

        //    return Log;
        //}

        [HttpPost]
        public async Task<IActionResult> Post(Log newLog)
        {
            await LogService.CreateLog(newLog);

            return CreatedAtAction(nameof(Get), newLog);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(Guid id, Log updatedLog)
        //{
        //    var Log = await LogService.GetLogById(id);

        //    if (Log is null)
        //    {
        //        return NotFound();
        //    }

        //    updatedLog.id = Log.id;

        //    await LogService.UpdateLog(id, updatedLog);

        //    return NoContent();
        //}


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var Log = await LogService.GetLogById(id);

        //    if (Log is null)
        //    {
        //        return NotFound();
        //    }

        //    await LogService.RemoveLog(id);

        //    return NoContent();
        //}
    }
}
