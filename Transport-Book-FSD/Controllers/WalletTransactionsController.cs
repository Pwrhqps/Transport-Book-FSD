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

    public class WalletTransactionsController : ControllerBase

    {

        private readonly AppDbContext _context;

        public WalletTransactionsController(AppDbContext context)

        {

            _context = context;

        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<WalletTransaction>>> GetWalletTransactions()

        {

            return await _context.WalletTransactions.ToListAsync();

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<WalletTransaction>> GetWalletTransaction(int id)

        {

            var tx = await _context.WalletTransactions.FindAsync(id);

            if (tx == null) return NotFound();

            return tx;

        }

        [HttpPost]

        public async Task<ActionResult<WalletTransaction>> PostWalletTransaction(WalletTransaction tx)

        {

            _context.WalletTransactions.Add(tx);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWalletTransaction), new { id = tx.WalletTransactionId }, tx);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutWalletTransaction(int id, WalletTransaction tx)

        {

            if (id != tx.WalletTransactionId) return BadRequest();

            _context.Entry(tx).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }

            catch (DbUpdateConcurrencyException)

            {

                if (!_context.WalletTransactions.Any(e => e.WalletTransactionId == id)) return NotFound();

                throw;

            }

            return NoContent();

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteWalletTransaction(int id)

        {

            var tx = await _context.WalletTransactions.FindAsync(id);

            if (tx == null) return NotFound();

            _context.WalletTransactions.Remove(tx);

            await _context.SaveChangesAsync();

            return NoContent();

        }

    }

}
