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

    public class StaffsController : ControllerBase

    {

        private readonly AppDbContext _context;

        public StaffsController(AppDbContext context)

        {

            _context = context;

        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Staff>>> GetStaffs()

        {

            return await _context.Staffs.ToListAsync();

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Staff>> GetStaff(int id)

        {

            var staff = await _context.Staffs.FindAsync(id);

            if (staff == null) return NotFound();

            return staff;

        }

        [HttpPost]

        public async Task<ActionResult<Staff>> PostStaff(Staff staff)

        {

            _context.Staffs.Add(staff);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStaff), new { id = staff.StaffId }, staff);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutStaff(int id, Staff staff)

        {

            if (id != staff.StaffId) return BadRequest();

            _context.Entry(staff).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }

            catch (DbUpdateConcurrencyException)

            {

                if (!_context.Staffs.Any(e => e.StaffId == id)) return NotFound();

                throw;

            }

            return NoContent();

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteStaff(int id)

        {

            var staff = await _context.Staffs.FindAsync(id);

            if (staff == null) return NotFound();

            _context.Staffs.Remove(staff);

            await _context.SaveChangesAsync();

            return NoContent();

        }

    }

}
