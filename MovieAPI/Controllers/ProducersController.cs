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
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly MovieContext _context;

        public ProducersController(MovieContext context)
        {
            _context = context;
        }
         

        // GET: api/Producers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblProducer>>> GetTblProducers()
        {
          if (_context.TblProducers == null)
          {
              return NotFound();
          }
            return await _context.TblProducers.ToListAsync();
        }

        // GET: api/Producers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblProducer>> GetTblProducer(int id)
        {
          if (_context.TblProducers == null)
          {
              return NotFound();
          }
            var tblProducer = await _context.TblProducers.FindAsync(id);

            if (tblProducer == null)
            {
                return NotFound();
            }

            return tblProducer;
        }

        // PUT: api/Producers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblProducer(int id, TblProducer tblProducer)
        {
            if (id != tblProducer.ProducerId)
            {
                return BadRequest();
            }

            _context.Entry(tblProducer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblProducerExists(id))
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

        // POST: api/Producers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblProducer>> PostTblProducer(TblProducer tblProducer)
        {
          if (_context.TblProducers == null)
          {
              return Problem("Entity set 'MovieContext.TblProducers'  is null.");
          }
            _context.TblProducers.Add(tblProducer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblProducerExists(tblProducer.ProducerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblProducer", new { id = tblProducer.ProducerId }, tblProducer);
        }

        // DELETE: api/Producers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblProducer(int id)
        {
            if (_context.TblProducers == null)
            {
                return NotFound();
            }
            var tblProducer = await _context.TblProducers.FindAsync(id);
            if (tblProducer == null)
            {
                return NotFound();
            }

            _context.TblProducers.Remove(tblProducer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblProducerExists(int id)
        {
            return (_context.TblProducers?.Any(e => e.ProducerId == id)).GetValueOrDefault();
        }
    }
}
