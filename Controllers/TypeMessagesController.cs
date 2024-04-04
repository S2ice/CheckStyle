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
    public class TypeMessagesController : ControllerBase
    {
        private readonly Offer_And_FindContext _context;

        public TypeMessagesController(Offer_And_FindContext context)
        {
            _context = context;
        }

        // GET: api/TypeMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeMessage>>> GetTypeMessages()
        {
          if (_context.TypeMessages == null)
          {
              return NotFound();
          }
            return await _context.TypeMessages.ToListAsync();
        }

        // GET: api/TypeMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeMessage>> GetTypeMessage(int? id)
        {
          if (_context.TypeMessages == null)
          {
              return NotFound();
          }
            var typeMessage = await _context.TypeMessages.FindAsync(id);

            if (typeMessage == null)
            {
                return NotFound();
            }

            return typeMessage;
        }

        // PUT: api/TypeMessages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeMessage(int? id, TypeMessage typeMessage)
        {
            if (id != typeMessage.IdType)
            {
                return BadRequest();
            }

            _context.Entry(typeMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeMessageExists(id))
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

        // POST: api/TypeMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeMessage>> PostTypeMessage(TypeMessage typeMessage)
        {
          if (_context.TypeMessages == null)
          {
              return Problem("Entity set 'Offer_And_FindContext.TypeMessages'  is null.");
          }
            _context.TypeMessages.Add(typeMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeMessage", new { id = typeMessage.IdType }, typeMessage);
        }

        // DELETE: api/TypeMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeMessage(int? id)
        {
            if (_context.TypeMessages == null)
            {
                return NotFound();
            }
            var typeMessage = await _context.TypeMessages.FindAsync(id);
            if (typeMessage == null)
            {
                return NotFound();
            }

            _context.TypeMessages.Remove(typeMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeMessageExists(int? id)
        {
            return (_context.TypeMessages?.Any(e => e.IdType == id)).GetValueOrDefault();
        }
    }
}
