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
    public class PassengerProfilesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PassengerProfilesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassengerProfile>>> GetPassengerProfiles()
        {
            return await _context.PassengerProfiles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PassengerProfile>> GetPassengerProfile(int id)
        {
            var profile = await _context.PassengerProfiles.FindAsync(id);
            if (profile == null) return NotFound();
            return profile;
        }

        [HttpPost]
        public async Task<ActionResult<PassengerProfile>> PostPassengerProfile(PassengerProfile profile)
        {
            _context.PassengerProfiles.Add(profile);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPassengerProfile), new { id = profile.PassengerProfileId }, profile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassengerProfile(int id, PassengerProfile profile)
        {
            if (id != profile.PassengerProfileId) return BadRequest();
            _context.Entry(profile).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PassengerProfiles.Any(e => e.PassengerProfileId == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassengerProfile(int id)
        {
            var profile = await _context.PassengerProfiles.FindAsync(id);
            if (profile == null) return NotFound();
            _context.PassengerProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}