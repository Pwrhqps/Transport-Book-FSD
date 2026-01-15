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
    public class DriverProfilesController : ControllerBase
    {
        private readonly TransportBookFSDContext _context;

        public DriverProfilesController(TransportBookFSDContext context)
        {
            _context = context;
        }

        // GET: api/DriverProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverProfile>>> GetDriverProfile()
        {
            return await _context.DriverProfile.ToListAsync();
        }

        // GET: api/DriverProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverProfile>> GetDriverProfile(int id)
        {
            var driverProfile = await _context.DriverProfile.FindAsync(id);

            if (driverProfile == null)
            {
                return NotFound();
            }

            return driverProfile;
        }

        // PUT: api/DriverProfiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriverProfile(int id, DriverProfile driverProfile)
        {
            if (id != driverProfile.DriverProfileId)
            {
                return BadRequest();
            }

            _context.Entry(driverProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverProfileExists(id))
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

        // POST: api/DriverProfiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DriverProfile>> PostDriverProfile(DriverProfile driverProfile)
        {
            _context.DriverProfile.Add(driverProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriverProfile", new { id = driverProfile.DriverProfileId }, driverProfile);
        }

        // DELETE: api/DriverProfiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriverProfile(int id)
        {
            var driverProfile = await _context.DriverProfile.FindAsync(id);
            if (driverProfile == null)
            {
                return NotFound();
            }

            _context.DriverProfile.Remove(driverProfile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DriverProfileExists(int id)
        {
            return _context.DriverProfile.Any(e => e.DriverProfileId == id);
        }
    }
}
