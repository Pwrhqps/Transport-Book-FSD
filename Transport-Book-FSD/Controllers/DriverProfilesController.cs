using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportBookFSD.Data;
using TransportBookFSD.Models;

namespace TransportBookFSD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverProfilesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DriverProfilesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverProfile>>> GetDriverProfiles()
        {
            return await _context.DriverProfiles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DriverProfile>> GetDriverProfile(int id)
        {
            var profile = await _context.DriverProfiles.FindAsync(id);
            if (profile == null) return NotFound();
            return profile;
        }

        [HttpPost]
        public async Task<ActionResult<DriverProfile>> PostDriverProfile(DriverProfile profile)
        {
            _context.DriverProfiles.Add(profile);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDriverProfile), new { id = profile.DriverProfileId }, profile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriverProfile(int id, DriverProfile profile)
        {
            if (id != profile.DriverProfileId) return BadRequest();
            _context.Entry(profile).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.DriverProfiles.Any(e => e.DriverProfileId == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriverProfile(int id)
        {
            var profile = await _context.DriverProfiles.FindAsync(id);
            if (profile == null) return NotFound();
            _context.DriverProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}