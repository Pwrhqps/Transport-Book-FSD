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

    public class VehiclesController : ControllerBase

    {

        private readonly AppDbContext _context;

        public VehiclesController(AppDbContext context)

        {

            _context = context;

        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()

        {

            return await _context.Vehicles.ToListAsync();

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Vehicle>> GetVehicle(int id)

        {

            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null) return NotFound();

            return vehicle;

        }

        [HttpPost]

        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)

        {

            _context.Vehicles.Add(vehicle);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.VehicleId }, vehicle);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)

        {

            if (id != vehicle.VehicleId) return BadRequest();

            _context.Entry(vehicle).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }

            catch (DbUpdateConcurrencyException)

            {

                if (!_context.Vehicles.Any(e => e.VehicleId == id)) return NotFound();

                throw;

            }

            return NoContent();

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteVehicle(int id)

        {

            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null) return NotFound();

            _context.Vehicles.Remove(vehicle);

            await _context.SaveChangesAsync();

            return NoContent();

        }

    }

}
