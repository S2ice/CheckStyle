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
    public class TypeAdsController : ControllerBase
    {
        private readonly Offer_And_FindContext _context;

        public TypeAdsController(Offer_And_FindContext context)
        {
            _context = context;
        }

        // GET: api/TypeAds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeAd>>> GetTypeAds()
        {
          if (_context.TypeAds == null)
          {
              return NotFound();
          }
            return await _context.TypeAds.ToListAsync();
        }

        // GET: api/TypeAds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeAd>> GetTypeAd(int? id)
        {
          if (_context.TypeAds == null)
          {
              return NotFound();
          }
            var typeAd = await _context.TypeAds.FindAsync(id);

            if (typeAd == null)
            {
                return NotFound();
            }

            return typeAd;
        }

        // PUT: api/TypeAds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeAd(int? id, TypeAd typeAd)
        {
            if (id != typeAd.IdType)
            {
                return BadRequest();
            }

            _context.Entry(typeAd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeAdExists(id))
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

        // POST: api/TypeAds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeAd>> PostTypeAd(TypeAd typeAd)
        {
          if (_context.TypeAds == null)
          {
              return Problem("Entity set 'Offer_And_FindContext.TypeAds'  is null.");
          }
            _context.TypeAds.Add(typeAd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeAd", new { id = typeAd.IdType }, typeAd);
        }

        // DELETE: api/TypeAds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeAd(int? id)
        {
            if (_context.TypeAds == null)
            {
                return NotFound();
            }
            var typeAd = await _context.TypeAds.FindAsync(id);
            if (typeAd == null)
            {
                return NotFound();
            }

            _context.TypeAds.Remove(typeAd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeAdExists(int? id)
        {
            return (_context.TypeAds?.Any(e => e.IdType == id)).GetValueOrDefault();
        }
    }
}
