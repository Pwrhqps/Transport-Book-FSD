using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportBookFSD.Data;
using TransportBookFSD.Models;

namespace TransportBookFSD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerProfilesController : ControllerBase
    {
        private readonly TransportBookFSDContext _context;

        public PassengerProfilesController(TransportBookFSDContext context)
        {
            _context = context;
        }

        // GET: api/PassengerProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassengerProfile>>> GetPassengerProfile()
        {
            return await _context.PassengerProfile.ToListAsync();
        }

        // GET: api/PassengerProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PassengerProfile>> GetPassengerProfile(int id)
        {
            var passengerProfile = await _context.PassengerProfile.FindAsync(id);

            if (passengerProfile == null)
            {
                return NotFound();
            }

            return passengerProfile;
        }

        // PUT: api/PassengerProfiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassengerProfile(int id, PassengerProfile passengerProfile)
        {
            if (id != passengerProfile.PassengerProfileId)
            {
                return BadRequest();
            }

            _context.Entry(passengerProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassengerProfileExists(id))
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

        // POST: api/PassengerProfiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PassengerProfile>> PostPassengerProfile(PassengerProfile passengerProfile)
        {
            _context.PassengerProfile.Add(passengerProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassengerProfile", new { id = passengerProfile.PassengerProfileId }, passengerProfile);
        }

        // DELETE: api/PassengerProfiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassengerProfile(int id)
        {
            var passengerProfile = await _context.PassengerProfile.FindAsync(id);
            if (passengerProfile == null)
            {
                return NotFound();
            }

            _context.PassengerProfile.Remove(passengerProfile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PassengerProfileExists(int id)
        {
            return _context.PassengerProfile.Any(e => e.PassengerProfileId == id);
        }
    }
}
