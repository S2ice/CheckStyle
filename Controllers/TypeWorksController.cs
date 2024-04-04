using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfferAndFindAPI.Models;

namespace OfferAndFindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeWorksController : ControllerBase
    {
        private readonly Offer_And_FindContext _context;

        public TypeWorksController(Offer_And_FindContext context)
        {
            _context = context;
        }

        // GET: api/TypeWorks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeWork>>> GetTypeWorks()
        {
          if (_context.TypeWorks == null)
          {
              return NotFound();
          }
            return await _context.TypeWorks.ToListAsync();
        }

        // GET: api/TypeWorks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeWork>> GetTypeWork(int? id)
        {
          if (_context.TypeWorks == null)
          {
              return NotFound();
          }
            var typeWork = await _context.TypeWorks.FindAsync(id);

            if (typeWork == null)
            {
                return NotFound();
            }

            return typeWork;
        }

        // PUT: api/TypeWorks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeWork(int? id, TypeWork typeWork)
        {
            if (id != typeWork.IdType)
            {
                return BadRequest();
            }

            _context.Entry(typeWork).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeWorkExists(id))
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

        // POST: api/TypeWorks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeWork>> PostTypeWork(TypeWork typeWork)
        {
          if (_context.TypeWorks == null)
          {
              return Problem("Entity set 'Offer_And_FindContext.TypeWorks'  is null.");
          }
            _context.TypeWorks.Add(typeWork);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeWork", new { id = typeWork.IdType }, typeWork);
        }

        // DELETE: api/TypeWorks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeWork(int? id)
        {
            if (_context.TypeWorks == null)
            {
                return NotFound();
            }
            var typeWork = await _context.TypeWorks.FindAsync(id);
            if (typeWork == null)
            {
                return NotFound();
            }

            _context.TypeWorks.Remove(typeWork);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeWorkExists(int? id)
        {
            return (_context.TypeWorks?.Any(e => e.IdType == id)).GetValueOrDefault();
        }
    }
}
