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

    public class DriverRatingsController : ControllerBase

    {

        private readonly AppDbContext _context;
 
        public DriverRatingsController(AppDbContext context)

        {

            _context = context;

        }

        // GET: api/DriverRatings

        [HttpGet]

        public async Task<ActionResult<IEnumerable<DriverRatingViewModel>>> GetDriverRatings()

        {

            return await _context.DriverRatingViewModel.ToListAsync();

        }

        // GET: api/DriverRatings/5

        [HttpGet("{id}")]

        public async Task<ActionResult<DriverRatingViewModel>> GetDriverRating(int id)

        {

            var rating = await _context.DriverRatingViewModel.FindAsync(id);

            if (rating == null) return NotFound();

            return rating;

        }

    }

}
