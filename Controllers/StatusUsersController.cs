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
    public class StatusUsersController : ControllerBase
    {
        private readonly Offer_And_FindContext _context;

        public StatusUsersController(Offer_And_FindContext context)
        {
            _context = context;
        }

        // GET: api/StatusUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusUser>>> GetStatusUsers()
        {
          if (_context.StatusUsers == null)
          {
              return NotFound();
          }
            return await _context.StatusUsers.ToListAsync();
        }

        // GET: api/StatusUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusUser>> GetStatusUser(int? id)
        {
          if (_context.StatusUsers == null)
          {
              return NotFound();
          }
            var statusUser = await _context.StatusUsers.FindAsync(id);

            if (statusUser == null)
            {
                return NotFound();
            }

            return statusUser;
        }

        // PUT: api/StatusUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusUser(int? id, StatusUser statusUser)
        {
            if (id != statusUser.IdStatus)
            {
                return BadRequest();
            }

            _context.Entry(statusUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusUserExists(id))
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

        // POST: api/StatusUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatusUser>> PostStatusUser(StatusUser statusUser)
        {
          if (_context.StatusUsers == null)
          {
              return Problem("Entity set 'Offer_And_FindContext.StatusUsers'  is null.");
          }
            _context.StatusUsers.Add(statusUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatusUser", new { id = statusUser.IdStatus }, statusUser);
        }

        // DELETE: api/StatusUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusUser(int? id)
        {
            if (_context.StatusUsers == null)
            {
                return NotFound();
            }
            var statusUser = await _context.StatusUsers.FindAsync(id);
            if (statusUser == null)
            {
                return NotFound();
            }

            _context.StatusUsers.Remove(statusUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusUserExists(int? id)
        {
            return (_context.StatusUsers?.Any(e => e.IdStatus == id)).GetValueOrDefault();
        }
    }
}
