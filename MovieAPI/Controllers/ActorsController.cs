using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieAPI_Context.Model;

namespace MovieAPI.Controllers
{
    public class ActorsController : Controller
    {
        private readonly MovieContext _context;

        public ActorsController(MovieContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
              return _context.TblActors != null ? 
                          View(await _context.TblActors.ToListAsync()) :
                          Problem("Entity set 'MovieContext.TblActors'  is null.");
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblActors == null)
            {
                return NotFound();
            }

            var tblActor = await _context.TblActors
                .FirstOrDefaultAsync(m => m.ActorId == id);
            if (tblActor == null)
            {
                return NotFound();
            }

            return View(tblActor);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActorId,ActorName,Gender,Dob")] TblActor tblActor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblActor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblActor);
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblActors == null)
            {
                return NotFound();
            }

            var tblActor = await _context.TblActors.FindAsync(id);
            if (tblActor == null)
            {
                return NotFound();
            }
            return View(tblActor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActorId,ActorName,Gender,Dob")] TblActor tblActor)
        {
            if (id != tblActor.ActorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblActor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblActorExists(tblActor.ActorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblActor);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblActors == null)
            {
                return NotFound();
            }

            var tblActor = await _context.TblActors
                .FirstOrDefaultAsync(m => m.ActorId == id);
            if (tblActor == null)
            {
                return NotFound();
            }

            return View(tblActor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblActors == null)
            {
                return Problem("Entity set 'MovieContext.TblActors'  is null.");
            }
            var tblActor = await _context.TblActors.FindAsync(id);
            if (tblActor != null)
            {
                _context.TblActors.Remove(tblActor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblActorExists(int id)
        {
          return (_context.TblActors?.Any(e => e.ActorId == id)).GetValueOrDefault();
        }
    }
}
