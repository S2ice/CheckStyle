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
    public class StatusAdsController : ControllerBase
    {
        private readonly Offer_And_FindContext _context;

        public StatusAdsController(Offer_And_FindContext context)
        {
            _context = context;
        }

        // GET: api/StatusAds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusAd>>> GetStatusAds()
        {
          if (_context.StatusAds == null)
          {
              return NotFound();
          }
            return await _context.StatusAds.ToListAsync();
        }

        // GET: api/StatusAds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusAd>> GetStatusAd(int? id)
        {
          if (_context.StatusAds == null)
          {
              return NotFound();
          }
            var statusAd = await _context.StatusAds.FindAsync(id);

            if (statusAd == null)
            {
                return NotFound();
            }

            return statusAd;
        }

        // PUT: api/StatusAds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusAd(int? id, StatusAd statusAd)
        {
            if (id != statusAd.IdStatus)
            {
                return BadRequest();
            }

            _context.Entry(statusAd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusAdExists(id))
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

        // POST: api/StatusAds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatusAd>> PostStatusAd(StatusAd statusAd)
        {
          if (_context.StatusAds == null)
          {
              return Problem("Entity set 'Offer_And_FindContext.StatusAds'  is null.");
          }
            _context.StatusAds.Add(statusAd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatusAd", new { id = statusAd.IdStatus }, statusAd);
        }

        // DELETE: api/StatusAds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusAd(int? id)
        {
            if (_context.StatusAds == null)
            {
                return NotFound();
            }
            var statusAd = await _context.StatusAds.FindAsync(id);
            if (statusAd == null)
            {
                return NotFound();
            }

            _context.StatusAds.Remove(statusAd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusAdExists(int? id)
        {
            return (_context.StatusAds?.Any(e => e.IdStatus == id)).GetValueOrDefault();
        }
    }
}
