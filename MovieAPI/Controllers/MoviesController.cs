using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI_Context.Model;

namespace MovieAPI.Controllers
{
    //Please Configure route in API
    //Updated by krishna 1.0


    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblMovie>>> GetTblMovies()
        {
          if (_context.TblMovies == null)
          {
              return NotFound();
          }
            return await _context.TblMovies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblMovie>> GetTblMovie(int id)
        {
          if (_context.TblMovies == null)
          {
              return NotFound();
          }
            var tblMovie = await _context.TblMovies.FindAsync(id);

            if (tblMovie == null)
            {
                return NotFound();
            }

            return tblMovie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblMovie(int id, TblMovie tblMovie)
        {
            if (id != tblMovie.MovieId)
            {
                return BadRequest();
            }

            _context.Entry(tblMovie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblMovieExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblMovie>> PostTblMovie(TblMovie tblMovie)
        {
          if (_context.TblMovies == null)
          {
              return Problem("Entity set 'MovieContext.TblMovies'  is null.");
          }
            _context.TblMovies.Add(tblMovie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblMovie", new { id = tblMovie.MovieId }, tblMovie);
        }
        
        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblMovie(int id)
        {
            if (_context.TblMovies == null)
            {
                return NotFound();
            }
            var tblMovie = await _context.TblMovies.FindAsync(id);
            if (tblMovie == null)
            {
                return NotFound();
            }

            _context.TblMovies.Remove(tblMovie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblMovieExists(int id)
        {
            return (_context.TblMovies?.Any(e => e.MovieId == id)).GetValueOrDefault();
        }
    }
}
