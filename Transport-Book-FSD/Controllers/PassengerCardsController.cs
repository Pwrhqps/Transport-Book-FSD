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

    public class PassengerCardsController : ControllerBase

    {

        private readonly AppDbContext _context;

        public PassengerCardsController(AppDbContext context)

        {

            _context = context;

        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<PassengerCard>>> GetPassengerCards()

        {

            return await _context.PassengerCards.ToListAsync();

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<PassengerCard>> GetPassengerCard(int id)

        {

            var card = await _context.PassengerCards.FindAsync(id);

            if (card == null) return NotFound();

            return card;

        }

        [HttpPost]

        public async Task<ActionResult<PassengerCard>> PostPassengerCard(PassengerCard card)

        {

            _context.PassengerCards.Add(card);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPassengerCard), new { id = card.PassengerCardId }, card);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutPassengerCard(int id, PassengerCard card)

        {

            if (id != card.PassengerCardId) return BadRequest();

            _context.Entry(card).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }

            catch (DbUpdateConcurrencyException)

            {

                if (!_context.PassengerCards.Any(e => e.PassengerCardId == id)) return NotFound();

                throw;

            }

            return NoContent();

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletePassengerCard(int id)

        {

            var card = await _context.PassengerCards.FindAsync(id);

            if (card == null) return NotFound();

            _context.PassengerCards.Remove(card);

            await _context.SaveChangesAsync();

            return NoContent();

        }

    }

}
