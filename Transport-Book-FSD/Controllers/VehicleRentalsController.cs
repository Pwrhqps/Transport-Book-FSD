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

    public class VehicleRentalsController : ControllerBase

    {

        private readonly AppDbContext _context;

        public VehicleRentalsController(AppDbContext context)

        {

            _context = context;

        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<VehicleRental>>> GetVehicleRentals()

        {

            return await _context.VehicleRentals.ToListAsync();

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<VehicleRental>> GetVehicleRental(int id)

        {

            var rental = await _context.VehicleRentals.FindAsync(id);

            if (rental == null) return NotFound();

            return rental;

        }

        [HttpPost]

        public async Task<ActionResult<VehicleRental>> PostVehicleRental(VehicleRental rental)

        {

            _context.VehicleRentals.Add(rental);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVehicleRental), new { id = rental.VehicleRentalId }, rental);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutVehicleRental(int id, VehicleRental rental)

        {

            if (id != rental.VehicleRentalId) return BadRequest();

            _context.Entry(rental).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }

            catch (DbUpdateConcurrencyException)

            {

                if (!_context.VehicleRentals.Any(e => e.VehicleRentalId == id)) return NotFound();

                throw;

            }

            return NoContent();

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteVehicleRental(int id)

        {

            var rental = await _context.VehicleRentals.FindAsync(id);

            if (rental == null) return NotFound();

            _context.VehicleRentals.Remove(rental);

            await _context.SaveChangesAsync();

            return NoContent();

        }

    }

}
