using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIFireAlarm_221119.Models;

//TODO 06: Add > new scaffolded item & juiste opties kiezen
//TODO 07: launchsettings.json aanpassen
//TODO 09: bekijk alle methods in deze controller
//TODO 10: open Postman
//TODO 11: disable SSL certificate verification in postman
//TODO 12: API testen met Postman

namespace APIFireAlarm_221119.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FireAlarmsController : ControllerBase
    {
        private readonly FireAlarmContext _context;

        public FireAlarmsController(FireAlarmContext context)
        {
            _context = context;

            //TODO 08: maak minstens 1 firealarm
            if(context.FireAlarmItems.Count() == 0)
            {
                FireAlarm fireAlarm = new FireAlarm()
                {
                    //ID => niet invullen dat gebeurt automatisch door entity framework
                    Location = "IWT - Kortrijk - Maaklab",
                    Reason = "Smoke detected",
                    Active = false
                };
                
                context.FireAlarmItems.Add(fireAlarm);
                context.SaveChanges();
            }
        }

        // GET: api/FireAlarms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FireAlarm>>> GetFireAlarmItems()
        {
            return await _context.FireAlarmItems.ToListAsync();
        }

        // GET: api/FireAlarms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FireAlarm>> GetFireAlarm(ulong id)
        {
            var fireAlarm = await _context.FireAlarmItems.FindAsync(id);

            if (fireAlarm == null)
            {
                return NotFound();
            }

            return fireAlarm;
        }

        // PUT: api/FireAlarms/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFireAlarm(ulong id, FireAlarm fireAlarm)
        {
            if (id != fireAlarm.ID)
            {
                return BadRequest();
            }

            _context.Entry(fireAlarm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FireAlarmExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FireAlarms
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<FireAlarm>> PostFireAlarm(FireAlarm fireAlarm)
        {
            _context.FireAlarmItems.Add(fireAlarm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFireAlarm", new { id = fireAlarm.ID }, fireAlarm);
        }

        // DELETE: api/FireAlarms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FireAlarm>> DeleteFireAlarm(ulong id)
        {
            var fireAlarm = await _context.FireAlarmItems.FindAsync(id);
            if (fireAlarm == null)
            {
                return NotFound();
            }

            _context.FireAlarmItems.Remove(fireAlarm);
            await _context.SaveChangesAsync();

            return fireAlarm;
        }

        private bool FireAlarmExists(ulong id)
        {
            return _context.FireAlarmItems.Any(e => e.ID == id);
        }
    }
}
