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
    public class DriverRatingViewModelsController : ControllerBase
    {
        private readonly TransportBookFSDContext _context;

        public DriverRatingViewModelsController(TransportBookFSDContext context)
        {
            _context = context;
        }

        // GET: api/DriverRatingViewModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverRatingViewModel>>> GetDriverRatingViewModel()
        {
            return await _context.DriverRatingViewModel.ToListAsync();
        }

        // GET: api/DriverRatingViewModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverRatingViewModel>> GetDriverRatingViewModel(int id)
        {
            var driverRatingViewModel = await _context.DriverRatingViewModel.FindAsync(id);

            if (driverRatingViewModel == null)
            {
                return NotFound();
            }

            return driverRatingViewModel;
        }

        // PUT: api/DriverRatingViewModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriverRatingViewModel(int id, DriverRatingViewModel driverRatingViewModel)
        {
            if (id != driverRatingViewModel.DriverId)
            {
                return BadRequest();
            }

            _context.Entry(driverRatingViewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverRatingViewModelExists(id))
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

        // POST: api/DriverRatingViewModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DriverRatingViewModel>> PostDriverRatingViewModel(DriverRatingViewModel driverRatingViewModel)
        {
            _context.DriverRatingViewModel.Add(driverRatingViewModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriverRatingViewModel", new { id = driverRatingViewModel.DriverId }, driverRatingViewModel);
        }

        // DELETE: api/DriverRatingViewModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriverRatingViewModel(int id)
        {
            var driverRatingViewModel = await _context.DriverRatingViewModel.FindAsync(id);
            if (driverRatingViewModel == null)
            {
                return NotFound();
            }

            _context.DriverRatingViewModel.Remove(driverRatingViewModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DriverRatingViewModelExists(int id)
        {
            return _context.DriverRatingViewModel.Any(e => e.DriverId == id);
        }
    }
}
